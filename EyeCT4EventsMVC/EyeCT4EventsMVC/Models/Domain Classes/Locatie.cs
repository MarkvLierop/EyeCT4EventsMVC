// <copyright file="Locatie.cs" company="Unitech">
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
    public class Locatie
    {
        public int ID { get; private set; }

        public string Naam { get; private set; }

        public string Straat { get; private set; }

        public int Huisnummer { get; private set; }

        public string Postcode { get; private set; }

        public string Woonplaats { get; private set; }

        private RepositoryLocatie repoLocatie;

        public Locatie(int id, string naam, string straat, int huisnummer, string postcode, string woonplaats)
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
            repoLocatie = new RepositoryLocatie(new MSSQLLocatie());
            return repoLocatie.AlleLocaties();
        }

        public int LocatieBijNaam(string naam)
        {
            repoLocatie = new RepositoryLocatie(new MSSQLLocatie());
            return repoLocatie.LocatieBijNaam(naam);
        }
    }
}