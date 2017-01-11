// <copyright file="ReserveringController.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;

namespace EyeCT4EventsMVC.Controllers
{
    public class ReserveringController : Controller
    {
        public RepositoryKampeerPlaatsen Rkp = new RepositoryKampeerPlaatsen(new MSSQLReserveren());
        public Reservering res = new Reservering();
        // GET: Reservering
        public ActionResult Reservering()
        {
            ViewBag.AlleKampeerplaatsen = Rkp.AlleKampeerplaatsenOpvragen();           
            return View();
        }

        public ActionResult ReserveringSubmit(int AantalPersonen, string inputName, string InputEmail, DateTime DatumVan, DateTime DatumTot, string Kampeerplek)
        {
            ViewBag.AantalPersonen = res.GebruikerList;
            return View();
        }
        
        public ActionResult ReserveringOpslaan(int AantalPersonen)
        {
            List < Gebruiker > gebruikerlijst = new List<Gebruiker>();
            for (int i = 0; i < AantalPersonen; i++)
            {
                Bezoeker g = new Bezoeker();
                g.Gebruikersnaam = "Naam" + i.ToString();
                g.Email = "Email" + i.ToString();
                gebruikerlijst.Add(g);
            }
            return View();
        }
    }
}