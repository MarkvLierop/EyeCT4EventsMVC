// <copyright file="BeheerController.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;
using System.Data.SqlClient;

namespace EyeCT4EventsMVC.Controllers
{
    public class BeheerController : Controller
    {
        public Locatie Locatie { get; set; }

        private Event Event; // Event with capital Letter because there is a event keyword in visual studio.

        private Media media;

        private Gebruiker gebruiker;

        // GET: Beheer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EventAanmaken()
        {
            Locatie = new Locatie();
            ViewBag.Locatie = Locatie.AlleLocaties();
            return View();
        }

        public ActionResult Aanmaken(string locatie, DateTime datumVan, DateTime datumTot, string titel, string beschrijving)
        {
            Locatie = new Locatie();
            Event events = new Event(Locatie.LocatieBijNaam(locatie), datumVan, datumTot, titel, beschrijving);
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

        public ActionResult MaakBeheerderAan(string voornaam, string tussenvoegsel, string achternaam, string email, string gebruikersnaam, string wachtwoord, 
            string WachtwoordBevestigen, string type)
        {
            if (wachtwoord == WachtwoordBevestigen)
            {
                try
                {
                    if (type == "Beheerder")
                    {
                        gebruiker = new Beheerder(gebruiker);
                    }

                    if (type == "Medewerker")
                    {
                        gebruiker = new Medewerker(gebruiker);
                    }

                    gebruiker.Voornaam = voornaam;
                    gebruiker.Tussenvoegsel = tussenvoegsel;
                    gebruiker.Achternaam = achternaam;
                    gebruiker.Email = email;
                    gebruiker.Gebruikersnaam = gebruikersnaam;
                    gebruiker.Wachtwoord = wachtwoord;
                    gebruiker.GebruikerRegistreren(gebruiker);
                    return RedirectToAction("Index", "Beheer");
                }
                catch(SqlException)
                {
                    Session["Message"] = "Emailadres is al in gebruik";
                    return RedirectToAction("BeheerderAanmaken", "Beheer");
                }
            }

            else
            {
                Session["Message"] = "Wachtwoordvelden komen niet overeen";
                return RedirectToAction("BeheerderAanmaken", "Beheer");
            }
        }

        public ActionResult GerapporteerdeMedia()
        {
            media = new Media();
            ViewBag.GerapporteerdeMedia = media.GerapporteerdeMedia();
            return View();
        }

        public ActionResult VerwijderenEvent(int eventID)
        {
            Event = new Event();
            Event.EventVerwijderen(eventID);
            return RedirectToAction("AlleEvents", "Beheer");
        }

        public ActionResult VerwijderenMedia(int mediaID)
        {
            media = new Media();
            foreach(Media media in media.GerapporteerdeMedia())
            {
                if(media.ID == mediaID)
                {
                    media.VerwijderMedia(media);
                }
            }
            return RedirectToAction("GerapporteerdeMedia", "Beheer");
        }
    }
}