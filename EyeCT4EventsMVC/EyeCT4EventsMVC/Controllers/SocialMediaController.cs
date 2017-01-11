// <copyright file="SocialMediaController.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;
using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;
using System.IO;

namespace EyeCT4EventsMVC.Controllers
{
    public class SocialMediaController : Controller
    {
        private RepositorySocialMediaSharing rsms = new RepositorySocialMediaSharing(new MSSQLSocialMediaSharing());

        List<Categorie> breadcrumbs = new List<Categorie>();
        private void getParent(int c)
        {
            Categorie cat = rsms.GetParentCategorie(c);
            breadcrumbs.Add(cat);
            if (cat.Parent != 0)
            {
                getParent(cat.Parent);
            }
        }
        public ActionResult SocialMedia()
        {
            Gebruiker g = new Bezoeker();
            g.ID = 1;
            Session["Gebruiker"] = g;
            try
            {
                rsms.SchoolAbusievelijkTaalgebruikOp();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            if (Url.RequestContext.RouteData.Values["id"] == null)
            {
                try
                {
                    ViewBag.AlleMedia = rsms.AlleMediaOpvragen();
                    ViewBag.AlleReacties = rsms.AlleReactiesOpvragen();
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
                    ViewBag.AlleMedia = rsms.ZoekenMedia(" ", Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]));
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                }
            }

            getParent(Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]));
            breadcrumbs.Reverse();
            ViewBag.BreadCrumbs = breadcrumbs;

            if (Url.RequestContext.RouteData.Values["id"] == null)
            {
                List<Categorie> catlist = rsms.AlleCategorienOpvragen().ToList();
                List<Categorie> finalList = new List<Categorie>();
                foreach (Categorie c in catlist)
                {
                    if (c.Parent == 0)
                    {
                        finalList.Add(c);
                    }
                }
                ViewBag.Categorien = finalList.ToArray();
            }
            else
            {
                Categorie c = rsms.GetCategorieMetID(Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]));

                ViewBag.Categorien = rsms.GetSubCategorien(c).ToArray();
            }

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

        [HttpPost]
        public ActionResult InsertMedia(Media media, HttpPostedFileBase file)
        {
            try
            {
                media.GeplaatstDoor = ((Gebruiker)Session["Gebruiker"]).ID;
                #region bestandopslaan
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    int hoogsteID;
                    if (rsms.SelectHoogsteMediaID().ID == 1)
                    {
                        hoogsteID = 1;
                    }
                    else
                    {
                        hoogsteID = rsms.SelectHoogsteMediaID().ID + 1;
                    }
                    string directory = @"C:\AdwCleaner\" + hoogsteID.ToString() + @"\";  // MAP DIRECTORY AANPASSEN NAAR SERVER

                    // Map aanmapen
                    System.IO.Directory.CreateDirectory(@"C:\AdwCleaner\" + hoogsteID.ToString() + @"\");

                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(directory, fileName);
                    file.SaveAs(path);

                    media.Pad = path;
                }
                #endregion
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
            if (!rsms.CheckOfLikeBestaat((Gebruiker)Session["Gebruiker"], mediaID, int.MinValue))
            {
                try
                {
                    rsms.ToevoegenLikeInMediaOfReactie((Gebruiker)Session["Gebruiker"], mediaID, int.MinValue);
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                }
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
                rsms.ToevoegenRapporterenMediaReactie(int.MinValue, reactieID);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return RedirectToAction("SocialMedia");
        }

        public ActionResult LikeReactie(int reactieID, int mediaID)
        {

            if (!rsms.CheckOfLikeBestaat((Gebruiker)Session["Gebruiker"], mediaID, reactieID))
            {
                try
                {
                    rsms.ToevoegenLikeInMediaOfReactie((Gebruiker)Session["Gebruiker"], mediaID, reactieID);
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                }
            }
            return RedirectToAction("SocialMedia");
        }

        public ActionResult ZoekenCategorie(string categorie)
        {
            rsms.ZoekenCategorie(categorie);
            return RedirectToAction("SocialMedia", "SocialMedia");
        }

        public ActionResult ReactieToevoegen(string inhoud, int id)
        {
            Gebruiker g = new Bezoeker();
            g.ID = 44;
            Session["Gebruiker"] = g;
            Reactie reactie = new Reactie();
            reactie.MediaID = id;
            reactie.DatumTijd = DateTime.Now;
            reactie.GeplaatstDoor = ((Gebruiker)Session["Gebruiker"]).ID;
            reactie.Inhoud = inhoud;
            rsms.ToevoegenReactie(reactie);
            return RedirectToAction("SocialMedia");
        }

        public ActionResult InsertCategorie(int id, string naam)
        {
            try
            {
                if (id != int.MinValue)
                {
                    Categorie cat = new Categorie();
                    cat.Parent = id;
                    cat.Naam = naam;
                    rsms.ToevoegenCategorie(cat);
                    return RedirectToAction("SocialMedia");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return RedirectToAction("SocialMedia");
        }
        public ActionResult LoadResource(string pad, string ext, string type)
        {
            if (type == "Afbeelding")
            {
                return File(pad, "image/jpg");
            }
            else if (type == "Video")
            {
                return File(pad, "video/mp4");
            }
            else if (type == "Audio")
            {
                return File(pad, "audio/mpeg");
            }
            return null;
        }
    }
}