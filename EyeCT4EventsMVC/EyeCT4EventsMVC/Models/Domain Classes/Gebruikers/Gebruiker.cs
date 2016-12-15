using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;

namespace EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers
{
    public abstract class Gebruiker
    {
        public string Achternaam { get; set; }
        public int ID { get; set; }
        public int RFID { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Voornaam { get; set; }
        public string Wachtwoord { get; set; }
        public bool Aanwezig { get; set; }

        private List<Media> MediaList;
        private List<Reactie> ReactieList;
        private List<UitgeleendMateriaal> UitgeleendMateriaal;

        private RepositoryActiveDirectory rad;

        public Gebruiker(string voornaam, string tussenvoegsel, string achternaam, string wachtwoord)
        {
            Voornaam = voornaam;
            Tussenvoegsel = tussenvoegsel;
            Achternaam = achternaam;
            Wachtwoord = wachtwoord;
        }
        protected Gebruiker()
        {
            rad = new RepositoryActiveDirectory(new ActiveDirectory());
        }

        public bool Inloggen(string gebruikersnaam, string wachtwoord)
        {
            return rad.Inloggen(gebruikersnaam, wachtwoord);
        }
        public string GetGebruikerType()
        {
            string[] type = this.GetType().ToString().Split('.');
            return type[type.Count() - 1];
        }
        public override string ToString()
        {
            return Voornaam + " " + Tussenvoegsel + " " + Achternaam;
        }
    }
}