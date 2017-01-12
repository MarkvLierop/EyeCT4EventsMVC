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
        public MSSQLGebruiker mssqlg = new MSSQLGebruiker();
        public MSSQLReserveren mssqlr = new MSSQLReserveren();

        // GET: Reservering
        public ActionResult Reservering()
        {
            ViewBag.AlleKampeerplaatsen = Rkp.AlleKampeerplaatsenOpvragen();           
            return View();
        }
              public ActionResult ReserverenSubmit(int AantalPersonen, string inputName, string InputEmail, string DatumVan, string DatumTot, string nummer)
        {
            string[] bestandsnaam = nummer.Split(',');
            int Nummer = Convert.ToInt32(bestandsnaam[0]);

            ViewBag.AantalPersonen = res.GebruikerList;
            string naam = inputName;
            string email = InputEmail;
            string datumv = DatumVan;
            string datumt = DatumTot;
            return View(new { Nummer = Nummer });
        }
        
        public ActionResult ReserveringOpslaan(int AantalPersonen, string nummer, string datumv, string datumt)
        {
            string[] bestandsnaam = Request.QueryString["nummer"].Split(',');
            int Nummer = Convert.ToInt32(bestandsnaam[0]);
            
            for (int i = 0; i<=AantalPersonen; i++)
            {
               Bezoeker g = new Bezoeker();
                g.Gebruikersnaam = Request.QueryString["Naam " + i.ToString()];
                g.Email = Request.QueryString["Email " + i.ToString()];

                Convert.ToDateTime(datumv);
                Convert.ToDateTime(datumt);
                if (!mssqlg.CheckOfGebruikerBestaat(g.Gebruikersnaam))
                {
                    mssqlg.GebruikerAanmaken(g);
                    //mssqlr.ReserveringPlaatsen();
                }
            }
  
            
            
            return View();
        }
    }
}