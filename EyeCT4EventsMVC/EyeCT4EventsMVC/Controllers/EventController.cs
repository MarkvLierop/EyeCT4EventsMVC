using EyeCT4EventsMVC.Models.Domain_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeCT4EventsMVC.Controllers
{
    public class EventController : Controller
    {
        private Locatie locatie;
        // GET: Event
        public ActionResult Index()
        {
            locatie = new Locatie();
            ViewBag.Locaties = locatie.AlleLocaties();
            return View();
        }

        public ActionResult EventAanmaken(string Locatie, DateTime datumVan,DateTime datumTot,string titel, string beschrijving)
        {
            locatie = new Locatie();
            Event events = new Event(locatie.LocatieBijNaam(Locatie).ID, datumVan, datumTot, titel, beschrijving);
            events.EventAanmaken(events);
            return RedirectToAction("Index", "Event");
        }
    }
}