using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeCT4EventsMVC.Controllers
{
    public class ToegangsController : Controller
    {
        // GET: Toegangs
        public ActionResult Index()
        {
            return View(new Bezoeker());
        }

        public ActionResult GebruikerBijBarcode(string barcode)
        {
            Bezoeker bezoeker = new Bezoeker();
            bezoeker = (Bezoeker)bezoeker.GebruikerBijBarcode(barcode);
            Session["Toegang"] = bezoeker;
            bezoeker.AfwezigAanwezig(bezoeker);
            return RedirectToAction("Index", "Toegangs");
        }
    }
}