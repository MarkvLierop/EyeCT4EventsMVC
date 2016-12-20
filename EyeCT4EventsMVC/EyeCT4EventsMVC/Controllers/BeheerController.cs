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

        public ActionResult Aanmaken(int locatieID, DateTime datumVan, DateTime datumTot,string titel, string beschrijving)
        {
            Event events = new Event(locatieID,datumVan,datumTot,titel,beschrijving);
            events.EventAanmaken(events);
            return RedirectToAction("AlleEvents", "Beheer");
        }

        public ActionResult AlleEvents()
        {
            return View();
        }

        public ActionResult BeheerderAanmaken()
        {
            return View();
        }

        public ActionResult GerapporteerdeMedia()
        {
            return View();
        }
    }
}