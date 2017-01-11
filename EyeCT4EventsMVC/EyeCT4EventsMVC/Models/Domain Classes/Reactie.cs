// <copyright file="Reactie.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class Reactie
    {
        private Repositories.RepositoryGebruiker rg;
        private RepositoryReactie repoReactie;

        public DateTime DatumTijd { get; set; }

        public int Flagged { get; set; }

        public int GeplaatstDoor { get; set; }

        public string Inhoud { get; set; }

        public int Likes { get; set; }

        public int MediaID { get; set; }

        public int ReactieID { get; set; }

        public int GebruikerID { get; private set; }

        public Reactie()
        {
            rg = new Repositories.RepositoryGebruiker(new MSSQLGebruiker());
        }

        public Reactie(int reactieID, int gebruikerID, int mediaID, int likes, string inhoud, DateTime datumTijd)
        {
            rg = new Repositories.RepositoryGebruiker(new MSSQLGebruiker());
            ReactieID = reactieID;
            GebruikerID = gebruikerID;
            MediaID = mediaID;
            Likes = likes;
            Inhoud = inhoud;
            DatumTijd = datumTijd;
        }

        public override string ToString()
        {
            return "Geplaatst Door: " + rg.GetGebruikerByID(GeplaatstDoor).ToString() + " | Reactie: " + Inhoud;
        }

        public string GeplaatstDoorGebruiker()
        {
            return rg.GetGebruikerByID(GeplaatstDoor).ToString();
        }

        public List<Reactie> ReactieBijMedia(int mediaID)
        {
            repoReactie = new RepositoryReactie(new MSSQLReactie());
            return repoReactie.ReactieBijMedia(mediaID);
        }
    }
}