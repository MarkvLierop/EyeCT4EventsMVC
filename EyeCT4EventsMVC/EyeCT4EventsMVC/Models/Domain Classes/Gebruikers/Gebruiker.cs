﻿// <copyright file="Gebruiker.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
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
        public string Email { get; set; }

        public string Achternaam { get; set; }

        public int ID { get; set; }

        public int RFID { get; set; }

        public string Gebruikersnaam { get; set; }

        public string Tussenvoegsel { get; set; }

        public string Voornaam { get; set; }

        public string Wachtwoord { get; set; }

        public bool Aanwezig { get; set; }
        public bool Betaald { get; set; }

        private List<Media> mediaList;
        private List<Reactie> reactieList;
        private List<UitgeleendMateriaal> uitgeleendMateriaal;

        private RepositoryActiveDirectory rad;
        private RepositoryGebruiker repoGebruiker;

        public Gebruiker(string gebruikersnaam, string voornaam, string tussenvoegsel, string achternaam, string wachtwoord, string email)
        {
            Voornaam = voornaam;
            Tussenvoegsel = tussenvoegsel;
            Achternaam = achternaam;
            Wachtwoord = wachtwoord;
            Email = email;
        }

        protected Gebruiker()
        {
            rad = new RepositoryActiveDirectory(new ActiveDirectory());
        }

        public bool Inloggen(string gebruikersnaam, string wachtwoord)
        {
            return rad.Inloggen(gebruikersnaam, wachtwoord);
        }

        public void GebruikerRegistreren(Gebruiker gebruiker)
        {
            repoGebruiker = new RepositoryGebruiker(new MSSQLGebruiker());
            repoGebruiker.GebruikerRegistreren(gebruiker);
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

        public Gebruiker GebruikerBijBarcode(string barcode)
        {
            repoGebruiker = new RepositoryGebruiker(new MSSQLGebruiker());
            return repoGebruiker.GetGebruikerByBarcode(barcode);
        }

        public void AfwezigAanwezig(Gebruiker gebruiker)
        {
            repoGebruiker = new RepositoryGebruiker(new MSSQLGebruiker());
            if(gebruiker.Aanwezig == true)
            {
                repoGebruiker.ZetGebruikerOpAfwezig(gebruiker.ID);
            }
            else
            {
                repoGebruiker.ZetGebruikerOpAanwezig(gebruiker.ID);
            }
        }
    }
}