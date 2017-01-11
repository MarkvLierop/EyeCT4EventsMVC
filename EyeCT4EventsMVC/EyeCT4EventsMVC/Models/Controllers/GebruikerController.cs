// <copyright file="GebruikerController.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
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

        public ActionResult Beheersysteem()
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
                    //// if gebruiker heeft polsbandje, naar social media. Anders naar registreer pagina
                    Session["Gebruiker"] = rg.GetGebruikerByGebruikersnaam(gebruikersnaam);

                    return RedirectToAction("SocialMedia", "SocialMedia");
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