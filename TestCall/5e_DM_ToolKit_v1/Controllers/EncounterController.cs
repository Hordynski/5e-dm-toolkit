using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using TeamAlpha.GoldenOracle.DAL;
using TeamAlpha.GoldenOracle.Models;

namespace TeamAlpha.GoldenOracle.Controllers
{
    public class EncounterController : Controller
    {
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromHours(1) };
        private DungeonContext db = new DungeonContext();

        public ActionResult Index()
        {
            return View(db.Encounters.ToList());
        }

        public ActionResult Details(int id)
        {
            return View(db.Encounters.Find(id));
        }

        public ActionResult BuildEncounter()
        {
            

            return RedirectToAction("AddingMonster");
        }

        public ActionResult SaveMonsterToList(Monsters monster)
        {
            List<Monsters> monsterList = new List<Monsters>();

            _cache.Set("MonsterList", monsterList, _policy);

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
            List<Monsters> monsterList = _cache.Get("MonsterList") as List<Monsters>;

            return View(monsterList);
        }

        [HttpPost]
        public ActionResult FinishBuildingEncounter(Encounter encounter)
        {
            List<Monsters> monsterList = _cache.Get("MonsterList") as List<Monsters>;

            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<Monsters>));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, monsterList);
                    xml = sww.ToString();
                }
            }
            encounter.MonsterList = xml;

            db.Encounters.Add(encounter);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}