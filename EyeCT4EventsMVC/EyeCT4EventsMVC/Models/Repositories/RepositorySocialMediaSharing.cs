// <copyright file="RepositorySocialMediaSharing.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;
using EyeCT4EventsMVC.Models.Interfaces;

namespace EyeCT4EventsMVC.Models.Repositories
{
    public class RepositorySocialMediaSharing
    {
        public ISocialMediaSharing ISMS;

        public RepositorySocialMediaSharing(ISocialMediaSharing isms)
        {
            ISMS = isms;
        }

        public void VerwijderMedia(Media media)
        {
            ISMS.VerwijderMedia(media);
        }
        public bool CheckOfLikeBestaat(Gebruiker gebruiker, int mediaID, int reactieID)
        {
            return ISMS.CheckOfLikeBestaat(gebruiker, mediaID, reactieID);
        }
        public void SchoolAbusievelijkTaalgebruikOp()
        {
            ISMS.SchoolAbusievelijkTaalgebruikOp();
        }

        public List<Event> AlleEventsOpvragen()
        {
            return ISMS.AlleEventsOpvragen();
        }

        public void VerwijderReactie(Reactie reactie)
        {
            ISMS.VerwijderReactie(reactie);
        }

        public void ToevoegenEvent(Event ev)
        {
            ISMS.ToevoegenEvent(ev);
        }

        public Categorie[] AlleCategorienOpvragen()
        {
            return ISMS.AlleCategorienOpvragen();
        }

        public void ToevoegenCategorie(Categorie cat)
        {
            ISMS.ToevoegenCategorie(cat);
        }
        public Categorie GetParentCategorie(int c)
        {
            return ISMS.GetParentCategorie(c);
        }
        public List<Categorie> GetSubCategorien(Categorie cat)
        {
            List<Categorie> catlist = new List<Categorie>();

            return ISMS.GetSubCategorien(cat, catlist);
        }
        public Categorie GetCategorieMetID(int ID)
        {
            return ISMS.GetCategorieMetID(ID);
        }
        // media.BestandOpslaan();
        public void ToevoegenMedia(Media media)  // AANPASSEN
        {
            ISMS.ToevoegenMedia(media);
        }

        public List<Reactie> AlleReactiesOpvragen(Media media)
        {
            return ISMS.AlleReactiesOpvragen(media);
        }
        public void ZetAantalKerenGerapporteerdOp0(Media media)
        {
            ISMS.ZetAantalKerenGerapporteerdOp0(media);
        }

        public void ToevoegenLikeInMediaOfReactie(Gebruiker gebruiker, int mediaID, int reactieID)
        {
            ISMS.ToevoegenLikeInMediaOfReactie(gebruiker, mediaID, reactieID);
        }

        public void ToevoegenRapporterenMediaReactie(int mediaID, int reactieID)
        {
            ISMS.ToevoegenRapporterenMediaReactie(mediaID, reactieID);
        }

        public Media GetMediaByID(int id)
        {
            return ISMS.GetMediaByID(id);
        }

        public Categorie GetCategorieMetNaam(string naam)
        {
            return ISMS.GetCategorieMetNaam(naam);
        }

        public List<Categorie> ZoekenCategorie(string naam)
        {
            return ISMS.ZoekenCategorie(naam);
        }

        public List<Media> ZoekenMedia(string zoekterm, int id)
        {
            return ISMS.ZoekenMedia(zoekterm, id);
        }

        public Media SelectHoogsteMediaID()
        {
            return ISMS.SelectLaatstIngevoerdeMedia();
        }

        public List<Media> AlleMediaOpvragen()
        {
            return ISMS.AlleMediaOpvragen();
        }

        public List<Media> AlleGerapporteerdeMediaOpvragen()
        {
            return ISMS.AlleGerapporteerdeMediaOpvragen();
        }

        public List<Reactie> AlleReactiesOpvragen()
        {
            return ISMS.AlleReactiesOpvragen();
        }

        public void ToevoegenReactie(Reactie reactie)
        {
            ISMS.ToevoegenReactie(reactie);
        }
    }
}