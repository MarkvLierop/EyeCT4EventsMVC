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
        private Locatie Locatie;
        private Event Event;
        private Media Media;
        // GET: Beheer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EventAanmaken()
        {
            Locatie = new Locatie();
            ViewBag.Locaties = Locatie.AlleLocaties();
            return View();
        }

        public ActionResult Aanmaken(string locatie, DateTime datumVan, DateTime datumTot,string titel, string beschrijving)
        {
            Locatie = new Locatie();
            Event events = new Event(Locatie.LocatieBijNaam(locatie),datumVan,datumTot,titel,beschrijving);
            events.EventAanmaken(events);
            return RedirectToAction("AlleEvents", "Beheer");
        }

        public ActionResult AlleEvents()
        {
            Event = new Event();
            ViewBag.Events = Event.AlleEvents();
            return View();
        }

        public ActionResult BeheerderAanmaken()
        {
            return View();
        }

        public ActionResult GerapporteerdeMedia()
        {
            Media = new Media();
            ViewBag.GerapporteerdeMedia = Media.GerapporteerdeMedia();
            return View();
        }

        public ActionResult VerwijderenEvent(int EventID)
        {
            Event = new Event();
            Event.EventVerwijderen(EventID);
            return RedirectToAction("AlleEvents", "Beheer");
        }

        public ActionResult VerwijderenMedia(int MediaID)
        {
            Media = new Media();
            Media.MediaVerwijderen(MediaID);
            return RedirectToAction("GerapporteerdeMedia", "Beheer");
        }

        public ActionResult BekijkReacties(int MediaID)
        {
            Reactie reactie = new Reactie();
            ViewBag.Reacties = reactie.ReactieBijMedia(MediaID);
            return RedirectToAction("GerapporteerdeMedia", "Beheer");
        }
    }
}