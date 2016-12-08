using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class Kampeerplaats
    {
        public List<Gebruiker> gebruikersOpLocatie { get; private set; }
        public int MaxPersonen { get; set; }
        public int ID { get; set; }
        public string Type { get; set; }
        public int Comfort { get; set; }
        public int Invalide { get; set; }
        public int Lawaai { get; set; }
        public int GebruikerID { get; set; }
        public int Locatie { get; set; }

        public Kampeerplaats()
        {
            gebruikersOpLocatie = new List<Gebruiker>();
        }

        public bool CheckOfBezoekerOpKampeerplaatsIs(Gebruiker g)
        {
            if (gebruikersOpLocatie.Contains(g))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            if (Comfort == 1)
            {
                return "Locatie: " + Locatie + " Max aantal Personen: " + MaxPersonen + " Comfortabel";
            }

            if (Lawaai == 1)
            {
                return "Locatie: " + Locatie + " Max aantal Personen: " + MaxPersonen + " Lawaaierig";
            }

            else
            {
                return "Locatie: " + Locatie + " Max aantal Personen: " + MaxPersonen + " Voor minder valide bezoekers";
            }

        }
    }
}