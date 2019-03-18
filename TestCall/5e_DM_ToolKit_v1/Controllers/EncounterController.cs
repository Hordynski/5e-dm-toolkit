using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TeamAlpha.GoldenOracle.DAL;
using TeamAlpha.GoldenOracle.Models;
using TeamAlpha.GoldenOracle.Services;

namespace TeamAlpha.GoldenOracle.Controllers
{
    public class EncounterController : Controller
    {
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromHours(1) };
        private DungeonContext db = new DungeonContext();
        public List<Monsters> monsterList = new List<Monsters>();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View(db.Encounters.ToList());
        }

        public ActionResult Details(int id)
        {
            var encounter = db.Encounters.Find(id);
            var decompressedJSON = StringCompression.Decompress(encounter.MonsterList);
            var jsonSerializer = new JavaScriptSerializer();
            List<Monsters> finalMonsterList = jsonSerializer.Deserialize<List<Monsters>>(decompressedJSON);
            ViewBag.ETitle = encounter.Title;
            ViewBag.EDescription = encounter.Description;
            ViewBag.ECR = encounter.ChallengeRating;

            return View(finalMonsterList);
        }

        public ActionResult BuildEncounter()
        {
            return RedirectToAction("AddingMonster");
        }

        public ActionResult SaveMonsterToList(Monsters monster)
        {
            if (monsterList == null)
            { 
                _cache.Set("MonsterList", monsterList, _policy);
            }

            monsterList.Add(monster);

            return RedirectToAction("AddingMonster");
        }

        public ActionResult AddingMonster()
        {
            return View();
        }

        public ActionResult RedirectingToMonsters()
        {
            return RedirectToAction("Index", "Monsters");
        }

        public ActionResult FinishBuildingEncounter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FinishBuildingEncounter(Encounter encounter)
        {
            List<Monsters> finalMonsterList = _cache.Get("MonsterList") as List<Monsters>;

            double crSum = 0;
            foreach (var item in finalMonsterList)
            {
                crSum += Convert.ToDouble(item.Challenge_Rating);
            }

            encounter.ChallengeRating = crSum;
            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(finalMonsterList);
            var compressedJSON = StringCompression.Compress(json);

            encounter.MonsterList = compressedJSON;

            db.Encounters.Add(encounter);
            db.SaveChanges();

            foreach (System.Collections.DictionaryEntry entry in HttpContext.Cache)
            {
                HttpContext.Cache.Remove("MonsterList");
                HttpContext.Cache.Remove("Monster");
            }

            return RedirectToAction("Index");
        }


    }
}