using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class UitgeleendMateriaal
    {
        public int Gebruiker { get; set; }
        public int MateriaalID { get; set; }
        public string MateriaalNaam { get; set; }
        public DateTime UitleenDatum { get; set; }
        public int Aantal { get; set; }
        public int UitleenID { get; set; }
        
        public override string ToString()
        {
            return "Item: " + MateriaalNaam + "Aantal: " + Aantal;
        }
    }
}