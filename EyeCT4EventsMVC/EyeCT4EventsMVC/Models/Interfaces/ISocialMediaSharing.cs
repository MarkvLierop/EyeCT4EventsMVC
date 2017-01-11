// <copyright file="ISocialMediaSharing.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;

namespace EyeCT4EventsMVC.Models.Interfaces
{
    public interface ISocialMediaSharing
    {
        Categorie[] AlleCategorienOpvragen();
        Categorie GetParentCategorie(int c);
        List<Categorie> GetSubCategorien(Categorie cat, List<Categorie> catlist);
        Categorie GetCategorieMetID(int ID);
        List<Media> AlleMediaOpvragen();

        List<Media> AlleGerapporteerdeMediaOpvragen();

        List<Reactie> AlleReactiesOpvragen();
        List<Reactie> AlleReactiesOpvragen(Media media);
        List<Event> AlleEventsOpvragen();
        bool CheckOfLikeBestaat(Gebruiker gebruiker, int mediaID, int reactieID);
        void ToevoegenCategorie(Categorie cat);

        void ToevoegenEvent(Event ev);

        void SchoolAbusievelijkTaalgebruikOp();

        void ToevoegenMedia(Media media);

        void ToevoegenLikeInMediaOfReactie(Gebruiker gebruiker, int mediaID, int reactieID);

        void ToevoegenReactie(Reactie reactie);

        void ToevoegenRapporterenMediaReactie(int mediaID, int reactieID);
        void VerwijderMedia(Media media);

        Categorie GetCategorieMetNaam(string naam);

        Media GetMediaByID(int id);

        List<Categorie> ZoekenCategorie(string naam);

        List<Media> ZoekenMedia(string zoekterm, int id);

        Media SelectLaatstIngevoerdeMedia();

        void ZetAantalKerenGerapporteerdOp0(Media media);

        void VerwijderReactie(Reactie reactie);
    }
}