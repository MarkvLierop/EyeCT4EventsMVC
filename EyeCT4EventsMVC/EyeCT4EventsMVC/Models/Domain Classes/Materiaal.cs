using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class Materiaal
    {
        public int MateriaalID { get; set; }
        public string Naam { get; set; }
        public int Prijs { get; set; }
        public int Voorraad { get; set; }

        public Materiaal()
        {

        }

        public override string ToString()
        {
            return "Item: " + Naam + " Prijs: " + Prijs + " Aantal beschikbaar: " + Voorraad;
        }
    }
}