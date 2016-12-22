using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class Locatie
    {
        public int ID { get; private set; }
        public string  Naam { get; private set; }
        public string Straat { get; private set; }
        public int Huisnummer { get; private set; }
        public string Postcode { get; private set; }
        public string Woonplaats { get; private set; }
        private RepositoryLocatie RepoLocatie;

        public Locatie(int id,string naam, string straat, int huisnummer,string postcode, string woonplaats)
        {
            ID = id;
            Naam = naam;
            Straat = straat;
            Huisnummer = huisnummer;
            Postcode = postcode;
            Woonplaats = woonplaats;
        }

        public Locatie()
        {

        }

        public List<Locatie> AlleLocaties()
        {
            RepoLocatie = new RepositoryLocatie(new MSSQLLocatie());
            return RepoLocatie.AlleLocaties();
        }

        public int LocatieBijNaam(string naam)
        {
            RepoLocatie = new RepositoryLocatie(new MSSQLLocatie());
            return RepoLocatie.LocatieBijNaam(naam);
        }
    }
}