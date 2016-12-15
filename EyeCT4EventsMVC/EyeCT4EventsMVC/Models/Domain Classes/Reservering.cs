// <copyright file="Reservering.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class Reservering
    {
        public List<Gebruiker> GebruikerList { get; private set; }

        public DateTime DatumTot { get; set; }

        public DateTime DatumVan { get; set; }

        public bool BetalingsStatus { get; set; }

        public int ReserveringID { get; set; }

        public bool Betaald { get; set; }

        public int KampeerplaatsID { get; set; }

        public int BezoekerID { get; set; }

        public Reservering()
        {
        }
    }
}