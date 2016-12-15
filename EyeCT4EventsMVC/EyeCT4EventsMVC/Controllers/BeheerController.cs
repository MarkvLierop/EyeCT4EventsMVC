using EyeCT4EventsMVC.Models.Domain_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeCT4EventsMVC.Controllers
{
    public class BeheerController : Controller
    {
        private Locatie locatie;
        private Event Event;
        // GET: Beheer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EventAanmaken()
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
            return RedirectToAction("AlleEvents", "Beheer");
        }

        public ActionResult Aanmaken(string Locatie, DateTime datumVan, DateTime datumTot, string titel, string beschrijving)
        {
            locatie = new Locatie();
            Event events = new Event(locatie.LocatieBijNaam(Locatie).ID, datumVan, datumTot, titel, beschrijving);
            events.EventAanmaken(events);
            return RedirectToAction("AlleEvents", "Beheer");
        }

        public ActionResult GerapporteerdeMedia()
        {
            Media media = new Media();
            ViewBag.GerapporteerdeMedia = media.GerapporteerdeMedia();
            return View();
        }
    }
}