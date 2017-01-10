// <copyright file="RepositoryGebruiker.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;
using EyeCT4EventsMVC.Models.Interfaces;

namespace EyeCT4EventsMVC.Models.Repositories
{
    public class RepositoryGebruiker
    {
        public IGebruikerAdministratie Context;

        public RepositoryGebruiker(IGebruikerAdministratie context)
        {
            Context = context;
        }

        public bool CheckOfGebruikerBestaat(string gebruikersnaam)
        {
            return Context.CheckOfGebruikerBestaat(gebruikersnaam);
        }

        public List<Gebruiker> GesorteerdeGeberuikers(string filter)
        {
            return Context.GesorteerdeGeberuikers(filter);
        }

        public List<Gebruiker> ZoekenGebruiker(string gezochteNaam)
        {
            return Context.ZoekenGebruiker(gezochteNaam);
        }

        public void Betaal(int rfid)
        {
            Context.Betaal(rfid);
        }

        public List<string> GetBetalingsGegevens(Gebruiker g)
        {
            return Context.GetBetalingsGegevens(g);
        }

        public Gebruiker GetGebruikerByRFID(int rfid)
        {
            return Context.GetGebruikerByRFID(rfid);
        }

        public List<Gebruiker> LijstAanwezigeBezoekers()
        {
            return Context.LijstAanwezigePersonen();
        }

        public void ZetGebruikerOpAanwezig(int rfid)
        {
            Context.ZetBezoekerOpAanwezig(rfid);
        }

        public void ZetGebruikerOpAfwezig(int gebruikerID)
        {
            Context.ZetBezoekerOpAfwezig(gebruikerID);
        }

        public Gebruiker GebruikerInloggen(string gebruikersnaam, string wachtwoord)
        {
            return Context.Inloggen(gebruikersnaam, wachtwoord);
        }

        public Gebruiker GetGebruikerByGebruikersnaam(string gebruikersnaam)
        {
            return Context.GetGebruikerByGebruikersnaam(gebruikersnaam);
        }

        public Gebruiker GetGebruikerByID(int id)
        {
            return Context.GetGebruikerByID(id);
        }

        public List<Gebruiker> GezochteBezoekersOphalen(string zoekopdracht)
        {
            return Context.GezochteBezoekersOphalen(zoekopdracht);
        }

        public void GebruikerRegistreren(Gebruiker gebruiker)
        {
            Context.GebruikerRegistreren(gebruiker);
        }
    }
}