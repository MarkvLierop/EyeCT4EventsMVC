// <copyright file="IGebruikerAdministratie.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;

namespace EyeCT4EventsMVC.Models.Interfaces
{
    public interface IGebruikerAdministratie
    {
        List<Gebruiker> LijstAanwezigePersonen();

        void ZetBezoekerOpAfwezig(int gebruikerID);

        void ZetBezoekerOpAanwezig(int gebruikerID);

        List<Gebruiker> ZoekenGebruiker(string gezochteNaam);

        bool CheckOfGebruikerBestaat(string gebruikersnaam);

        List<Gebruiker> GesorteerdeGeberuikers(string filter);

        List<Gebruiker> GezochteBezoekersOphalen(string zoekopdracht);

        Gebruiker Inloggen(string gebruikersnaam, string wachtwoord);

        Gebruiker GetGebruikerByGebruikersnaam(string gebruikersnaam);

        Gebruiker GetGebruikerByID(int id);

        Gebruiker GetGebruikerByRFID(int rfid);

        void GebruikerRegistreren(Gebruiker gebruiker);

        List<string> GetBetalingsGegevens(Gebruiker gebruiker);

        void Betaal(int rfid);
    }
}