using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;

namespace EyeCT4EventsMVC.Controllers
{
    public class ReserveringController : Controller
    {
        RepositoryKampeerPlaatsen rkp = new RepositoryKampeerPlaatsen(new MSSQLReserveren());

        // GET: Reservering
        public ActionResult Reservering()
        {
            ViewBag.AlleKampeerplaatsen = rkp.AlleKampeerplaatsenOpvragen();
            return View();
        }
    }
}