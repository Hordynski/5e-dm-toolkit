using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamAlpha.GoldenOracle.Models;

namespace TeamAlpha.GoldenOracle.Controllers
{
    public class AddController : Controller
    {
        // GET: Add
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public ActionResult SaveCreature(EncounterCreature encounterCreature)
        {

            HomeController.encounterCreatures.Add(encounterCreature);

            if (HomeController.creaturesQueue.Count != 0)
            {
                int i = HomeController.creaturesQueue.Count;
                while (HomeController.creaturesQueue.Peek().Initiative > encounterCreature.Initiative && i > 0)
                {
                    HomeController.creaturesQueue.Enqueue(HomeController.creaturesQueue.Dequeue());
                    i--;
                }
                for (int j = HomeController.encounterCreatures.Count - i; j > 0; j--)
                {
                    HomeController.creaturesQueue.Enqueue(HomeController.creaturesQueue.Dequeue());
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}