using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;
using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;

namespace EyeCT4EventsMVC.Controllers
{
    public class GebruikerController : Controller
    {
        // GET: Gebruiker
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string gebruikersnaam, string wachtwoord)
        {
            RepositoryActiveDirectory rad = new RepositoryActiveDirectory(new ActiveDirectory());
            RepositoryGebruiker rg = new RepositoryGebruiker(new MSSQLGebruiker());

            try
            {
                if (rad.Inloggen(gebruikersnaam, wachtwoord))
                {
                    Gebruiker g = rg.GetGebruikerByGebruikersnaam(gebruikersnaam);

                    // if g heeft polsbandje, naar social media. Anders naar registreer pagina

                    return RedirectToAction("Index", "SocialMedia");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return View();
        }

    }
}