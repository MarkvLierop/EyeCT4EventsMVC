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
        private Event Event;
        // GET: Event
        public ActionResult Aanmaken()
        {
            locatie = new Locatie();
            ViewBag.Locaties = locatie.AlleLocaties();
            return View();
        }


        public ActionResult AlleEvents()
        {
            Event = new Event();
            ViewBag.Events = Event.AlleEvents();
            return View();
        }

        public ActionResult Verwijderen(int EventID)
        {
            Event = new Event();
            Event.EventVerwijderen(EventID);
            return RedirectToAction("AlleEvents", "Event");
        }

        public ActionResult EventAanmaken(string Locatie, DateTime datumVan,DateTime datumTot,string titel, string beschrijving)
        {
            locatie = new Locatie();
            Event events = new Event(locatie.LocatieBijNaam(Locatie).ID, datumVan, datumTot, titel, beschrijving);
            events.EventAanmaken(events);
            return RedirectToAction("AlleEvents", "Event");
        }
    }
}