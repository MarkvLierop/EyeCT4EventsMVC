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
            //RepositoryActiveDirectory rad = new RepositoryActiveDirectory(new ActiveDirectory());
            RepositoryGebruiker rg = new RepositoryGebruiker(new MSSQLGebruiker());

            try
            {
                Gebruiker gebruiker = rg.GebruikerInloggen(gebruikersnaam, wachtwoord);
                if (gebruiker.ID != 0)
                {
                    Session["Gebruiker"] = rg.GetGebruikerByGebruikersnaam(gebruikersnaam);
                    if (gebruiker.GetGebruikerType() == "Bezoeker")
                    {
                        return RedirectToAction("SocialMedia", "SocialMedia", new { login = true });
                    }
                    else if(gebruiker.GetGebruikerType() == "Beheerder")
                    {
                        return RedirectToAction("Index", "Beheer");
                    }

                    else if(gebruiker.GetGebruikerType() == "Medewerker")
                    {
                        return RedirectToAction("Index", "Toegangs");
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Email en/of wachtwoord komen niet overeen";
            }
            return View();
        }

        public ActionResult Uitloggen()
        {
            return RedirectToAction("Login", "Gebruiker");
        }
    }
}