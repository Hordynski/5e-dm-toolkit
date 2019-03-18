using TeamAlpha.GoldenOracle.DAL;
using TeamAlpha.GoldenOracle.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TeamAlpha.GoldenOracle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //using (DungeonContext dungeon = new DungeonContext())
            //{
            //    List<Monsters> model = dungeon.Monsters.ToList();
            //}

            List<EncounterCreature> encounter = encounterCreatures;

            // Note: Add feature to add a new creature mid-encounter without messing up current order.
            if (encounter.Count > 1)
                encounter.Sort((x, y) => y.Initiative.CompareTo(x.Initiative));
            creaturesQueue.Clear();
            foreach (var x in encounter)
            {
                creaturesQueue.Enqueue(x);
            }

            return View(creaturesQueue);
        }

        public ActionResult GetView()
        {
            return PartialView();
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