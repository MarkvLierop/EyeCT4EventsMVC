<<<<<<< HEAD
﻿using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;
=======
﻿// <copyright file="Beheerder.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
>>>>>>> 860eb2f58f5a9749f318e963d5e6edae2bed8134
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