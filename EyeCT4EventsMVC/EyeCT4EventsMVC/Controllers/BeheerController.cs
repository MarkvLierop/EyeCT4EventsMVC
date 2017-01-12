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
using EyeCT4EventsMVC.Models.Repositories;
using EyeCT4EventsMVC.Models.Persistencies;

namespace EyeCT4EventsMVC.Controllers
{
    public class BeheerController : Controller
    {
        public Locatie Locatie { get; set; }

        private Event Event; // Event with capital Letter because there is a event keyword in visual studio.

        private Media media;
        private Reactie reactie;

        private Gebruiker gebruiker;

        // GET: Beheer
        public ActionResult Index()
        {
            Event = new Event();
            ViewBag.Events = Event.AlleEvents();
            if (Event.AlleEvents().Count == 0)
            {
                Session["GeenEvents"] = "Er zijn geen events om weer te geven";
            }
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
            return RedirectToAction("Index", "Beheer");
        }

        
        public ActionResult BeheerderAanmaken()
        {
            return View();
        }

        public ActionResult MaakBeheerderAan(string voornaam, string tussenvoegsel, string achternaam, string email, string gebruikersnaam, string wachtwoord, 
            string WachtwoordBevestigen, string type)
        {
            //RepositoryActiveDirectory RepoAD = new RepositoryActiveDirectory(new ActiveDirectory());
            if (wachtwoord == WachtwoordBevestigen)
            {
                try
                {
                    //RepoAD.GebruikerAanmaken(gebruikersnaam, wachtwoord);
                    if (type == "Beheerder")
                    {
                        gebruiker = new Beheerder(gebruiker);
                    }

                    else if (type == "Medewerker")
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
                    if (type == "Beheerder")
                    {
                        return RedirectToAction("Index", "Beheer");
                    }

                    else if(type == "Medewerker")
                    {
                        return RedirectToAction("Index", "Toegangs");
                    }
                    return View();
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
            if (media.GerapporteerdeMedia().Count == 0)
            {
                Session["ErrorMedia"] = "Er is geen media om weer te geven";
            }
            reactie = new Reactie();
            ViewBag.Reacties = reactie.ReactieBijGerapporteerdeMedia();
            Session["ErrorReactie"] = "Er zijn geen reacties bij deze media";
            return View();
        }

        public ActionResult GerapporteerdeReactie()
        {
            reactie = new Reactie();
            ViewBag.Reactie = reactie.GerapporteerdeReactie();
            if(reactie.GerapporteerdeReactie().Count == 0)
            {
                Session["ErrorReactie"] = "Er zijn geen reacties om weer te geven"; 
            }

            media = new Media();
            ViewBag.Media = media.AlleMedia();
            return View();
        }

        public ActionResult VerwijderenEvent(int eventID)
        {
            Event = new Event();
            Event.EventVerwijderen(eventID);
            return RedirectToAction("Index", "Beheer");
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
        public ActionResult VerwijderReactie(int reactieID)
        {
            reactie = new Reactie();
            media = new Media();
            foreach(Reactie reactie in reactie.ReactieBijGerapporteerdeMedia())
            {
                if(reactie.ReactieID == reactieID)
                {
                    media.VerwijderReactie(reactie);
                }
            }
            return RedirectToAction("GerapporteerdeMedia", "Beheer");
        }

    }
}