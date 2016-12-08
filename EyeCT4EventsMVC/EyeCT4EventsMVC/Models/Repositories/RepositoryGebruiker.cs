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
        IGebruikerAdministratie context;

        public RepositoryGebruiker(IGebruikerAdministratie context)
        {
            this.context = context;
        }
        public bool CheckOfGebruikerBestaat(string gebruikersnaam)
        {
            return context.CheckOfGebruikerBestaat(gebruikersnaam);
        }
        public List<Gebruiker> GesorteerdeGeberuikers(string filter)
        {
            return context.GesorteerdeGeberuikers(filter);
        }
        public List<Gebruiker> ZoekenGebruiker(string GezochtenNaam)
        {
            return context.ZoekenGebruiker(GezochtenNaam);
        }
        public void Betaal(int RFID)
        {
            context.Betaal(RFID);
        }
        public List<string> GetBetalingsGegevens(Gebruiker g)
        {
            return context.GetBetalingsGegevens(g);
        }
        public Gebruiker GetGebruikerByRFID(int RFID)
        {
            return context.GetGebruikerByRFID(RFID);
        }
        public List<Gebruiker> LijstAanwezigeBezoekers()
        {
            return context.LijstAanwezigePersonen();
        }
        public void ZetGebruikerOpAanwezig(int RFID)
        {
            context.ZetBezoekerOpAanwezig(RFID);
        }
        public void ZetGebruikerOpAfwezig(int gebruikerID)
        {
            context.ZetBezoekerOpAfwezig(gebruikerID);
        }
        public Gebruiker GebruikerInloggen(string gebruikersnaam, string wachtwoord)
        {
            return context.Inloggen(gebruikersnaam, wachtwoord);
        }
        public Gebruiker GetGebruikerByGebruikersnaam(string gebruikersnaam)
        {
            return context.GetGebruikerByGebruikersnaam(gebruikersnaam);
        }
        public Gebruiker GetGebruikerByID(int ID)
        {
            return context.GetGebruikerByID(ID);
        }

        public List<Gebruiker> GezochteBezoekersOphalen(string zoekopdracht)
        {
            return context.GezochteBezoekersOphalen(zoekopdracht);
        }

        public void GebruikerRegistreren(Gebruiker gebruiker)
        {
            context.GebruikerRegistreren(gebruiker);
        }
    }
}