using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;

namespace EyeCT4EventsMVC.Controllers
{
    public class SocialMediaController : Controller
    {
        private RepositorySocialMediaSharing rsms = new RepositorySocialMediaSharing(new MSSQLSocialMediaSharing());

        public ActionResult Index()
        {
            try
            {
                ViewBag.AlleMedia = rsms.AlleMediaOpvragen();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return View();
        }

        public ActionResult PlaatsReactie()
        {
            return View();
        }
    }
}