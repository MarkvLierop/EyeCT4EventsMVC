using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4EventsMVC.Models.Domain_Classes;
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
            Gebruiker g = new Bezoeker();
            g.ID = 5;
            Session["Gebruiker"] = g;
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

            if (Request.QueryString["categorie"] != null)
            {
                try
                {
                    ViewBag.Categorien = rsms.ZoekenCategorie(Request.QueryString["categorie"]).ToArray();
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                }
            }
            return View();
        }

        public ActionResult PlaatsReactie()
        {
            return View();
        }

        public ActionResult MediaToevoegen()
        {
            Media media = new Media();
            return View(media);
        }

        public ActionResult InsertMedia(Media media)
        {
            try
            {
                rsms.ToevoegenMedia(media);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("SocialMedia");
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
            if (Url.RequestContext.RouteData.Values["id"] == null)
            {
                ViewBag.Error = "Navigeer eerst naar een categorie.";
            }
            else
            {
                Categorie cat = new Categorie();
                cat.Parent = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);
                return View(cat);
            }
            return RedirectToAction("SocialMedia");
        }

        public ActionResult ZoekenCategorie(string categorie)
        {

            return RedirectToAction("SocialMedia", "SocialMedia");
        }

        public ActionResult ReactieToevoegen(string inhoud, int id)
        {
            Gebruiker g = new Bezoeker();
            g.ID = 44;
            Session["Gebruiker"] = g;
            Reactie reactie = new Reactie();
            reactie.Media = id;
            reactie.DatumTijd = DateTime.Now;
            reactie.GeplaatstDoor = ((Gebruiker) Session["Gebruiker"]).ID;
            reactie.Inhoud = inhoud;
            rsms.ToevoegenReactie(reactie);
            return RedirectToAction("SocialMedia");
        }
        public ActionResult Categorien()
        {
            return View();
        }
        public ActionResult InsertCategorie(Categorie cat)
        {
            try
            {
                rsms.ToevoegenCategorie(cat);
                return RedirectToAction("SocialMedia");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("CategorieToevoegen");
        }
    }
}