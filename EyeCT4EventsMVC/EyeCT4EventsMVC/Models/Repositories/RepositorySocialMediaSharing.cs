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
        private ISocialMediaSharing ISMS;

        public RepositorySocialMediaSharing(ISocialMediaSharing ISM)
        {
            this.ISMS = ISM;
        }
        public void VerwijderMedia(Media media)
        {
            ISMS.VerwijderMedia(media);
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
        public void ToevoegenMedia(Media media)  // AANPASSEN
        {
            //media.BestandOpslaan();
            ISMS.ToevoegenMedia(media);
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
        public List<Media> ZoekenMedia(string zoekterm, int ID)
        {
            return ISMS.ZoekenMedia(zoekterm, ID);
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
        public List<Reactie> AlleReactiesOpvragen(Media media)
        {
            return ISMS.AlleReactiesOpvragen(media);
        }
        public void ToevoegenReactie(Reactie reactie)
        {
            ISMS.ToevoegenReactie(reactie);
        }
    }
}