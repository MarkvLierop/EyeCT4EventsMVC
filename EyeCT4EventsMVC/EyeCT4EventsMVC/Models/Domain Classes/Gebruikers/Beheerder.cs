using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers
{
    public class Beheerder : Gebruiker
    {
        public string Gebruikerstype { get; private set; }
        public string Email { get; private set; }
        private RepositoryGebruiker RepoGebruiker;

        public Beheerder()
        {

        }
   
        public void BeheerderAanmaken(Gebruiker gebruiker)
        {
            RepoGebruiker = new RepositoryGebruiker(new MSSQLGebruiker());
            RepoGebruiker.GebruikerRegistreren(gebruiker);
        }
    }
}