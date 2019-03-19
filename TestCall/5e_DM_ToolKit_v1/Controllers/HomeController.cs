using TeamAlpha.GoldenOracle.DAL;
using TeamAlpha.GoldenOracle.Models;
using TeamAlpha.GoldenOracle.Models.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Net;

namespace TeamAlpha.GoldenOracle.Controllers
{
    public class HomeController : Controller
    {
        private DungeonContext db = new DungeonContext();

        public ActionResult Index()
        {
            EncounterViewModel encounterView = new EncounterViewModel();
            encounterView.EncounterCreatures = creaturesQueue;
            List<EncounterCreature> encounter = encounterCreatures;

            // Note: Add feature to add a new creature mid-encounter without messing up current order.
            if (encounter.Count > 1)
                encounter.Sort((x, y) => y.Initiative.CompareTo(x.Initiative));
            creaturesQueue.Clear();
            foreach (var x in encounter)
            {
                creaturesQueue.Enqueue(x);
            }

            return View(encounterView);
        }

        //public async task<actionresult> partialmonsterdetails(int index)
        //{
        //    var client = new httpclient();
        //    var urlextension = $"api/monsters/" + index;

        //    client.baseaddress = new uri("http://dnd5eapi.co/");
        //    var result = await client.getasync(urlextension);
        //    var monster = await result.content.readasasync<monsters>();

        //    return partialView("PartialMonsterDetails", monster);
        //}

        //[ChildActionOnly]
        //public ActionResult PartialCharacterDetails(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Characters character = db.Characters.Find(id);
        //    if (character == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return PartialView("PartialCharacterDetails", character);
        //}
        public async Task<PartialViewResult> MonstersDetail(int? id, bool? value)
        {
            var encounter = new EncounterViewModel();

            if (value == true)
            {
                
                var client = new HttpClient();
                var urlExtension = $"api/monsters/" + id;

                client.BaseAddress = new Uri("http://dnd5eapi.co/");
                var result = await client.GetAsync(urlExtension);
                var monster = await result.Content.ReadAsAsync<Monsters>();
                
                encounter.Monsters = monster;

                //return PartialView("_MonstersDetail", encounter);
            }

            else if (value == false)
            {
                Characters character = db.Characters.Find(id);
                encounter.Characters = character;

                //return PartialView("_MonstersDetail", encounter);
            }

            return PartialView("MonstersDetail", encounter);
        }

        public ActionResult NextTurn()
        {
            if (creaturesQueue.Count > 0)
                creaturesQueue.Enqueue(creaturesQueue.Dequeue());

            return View("Index", creaturesQueue);
        }

        public static Queue<EncounterCreature> creaturesQueue = new Queue<EncounterCreature>();
        public static List<EncounterCreature> encounterCreatures = new List<EncounterCreature>();
    }
}