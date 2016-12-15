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
    public class SocialMediaController : Controller
    {
        private RepositorySocialMediaSharing rsms = new RepositorySocialMediaSharing(new MSSQLSocialMediaSharing());

        public ActionResult SocialMedia()
        {
            if (Url.RequestContext.RouteData.Values["id"] == null)
            {
                try
                {
                    ViewBag.AlleMedia = rsms.AlleMediaOpvragen();
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                }
            }
            else
            {
                try
                {
                    ViewBag.AlleMedia = rsms.ZoekenMedia("", Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]));
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                }
            }

            ViewBag.Categorien = rsms.AlleCategorienOpvragen();

            return View();
        }

        public ActionResult PlaatsReactie()
        {
            return View();
        }

        public ActionResult LikeMedia(int mediaID)
        {
            try
            {
                rsms.ToevoegenLikeInMediaOfReactie((Gebruiker)Session["Gebruiker"], mediaID, Int32.MinValue);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return RedirectToAction("SocialMedia");
        }
        public ActionResult RapporteerMedia(int mediaID)
        {
            try
            {
                rsms.ToevoegenRapporterenMediaReactie(mediaID, int.MinValue);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return RedirectToAction("SocialMedia");
        }

        public ActionResult RapporteerReactie(int reactieID)
        {
            try
            {
                rsms.ToevoegenRapporterenMediaReactie(reactieID, reactieID);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("SocialMedia");
        }
        public ActionResult LikeReactie(int reactieID)
        {
            try
            {
                rsms.ToevoegenLikeInMediaOfReactie((Gebruiker)Session["Gebruiker"], int.MinValue, reactieID);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("SocialMedia");
        }

        public ActionResult CategorieToevoegen()
        {
            return 
        }
    }
}