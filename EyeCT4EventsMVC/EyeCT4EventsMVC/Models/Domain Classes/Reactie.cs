﻿// <copyright file="Reactie.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Persistencies;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class Reactie
    {
        private Repositories.RepositoryGebruiker rg;

        public DateTime DatumTijd { get; set; }

        public int Flagged { get; set; }

        public int GeplaatstDoor { get; set; }

        public string Inhoud { get; set; }

        public int Likes { get; set; }

        public int Media { get; set; }

        public int ReactieID { get; set; }
        
        public Reactie()
        {
            rg = new Repositories.RepositoryGebruiker(new MSSQLGebruiker());
        }

        public override string ToString()
        {
            return "Geplaatst Door: " + rg.GetGebruikerByID(GeplaatstDoor).ToString() + " | Reactie: " + Inhoud;
        }
    }
}