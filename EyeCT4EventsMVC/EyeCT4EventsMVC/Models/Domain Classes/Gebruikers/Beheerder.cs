// <copyright file="Beheerder.cs" company="Unitech">
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
    public class Beheerder : Gebruiker
    {
        public string Gebruikerstype { get; private set; }

        public new string Email { get; private set; }

        private RepositoryGebruiker repoGebruiker;

        public Beheerder()
        {
        }

        public Beheerder(Gebruiker gebruiker)
        {
        }
   
        public void BeheerderAanmaken(Gebruiker gebruiker)
        {
            repoGebruiker = new RepositoryGebruiker(new MSSQLGebruiker());
            repoGebruiker.GebruikerRegistreren(gebruiker);
        }
    }
}