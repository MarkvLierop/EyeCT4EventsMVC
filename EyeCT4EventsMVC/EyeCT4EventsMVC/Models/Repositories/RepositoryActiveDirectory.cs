// <copyright file="RepositoryActiveDirectory.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Interfaces;

namespace EyeCT4EventsMVC.Models.Repositories
{
    public class RepositoryActiveDirectory
    {
        private IActiveDirectory iad;

        public RepositoryActiveDirectory(IActiveDirectory iad)
        {
            this.iad = iad;
        }

        public bool Inloggen(string gebruikersnaam, string wachtwoord)
        {
            return iad.GebruikerAuthentiseren(gebruikersnaam, wachtwoord);
        }

        public void ToevoegenAanGroep(string gebruikersnaam, string groepnaam)
        {
            iad.ToevoegenAanGroep(gebruikersnaam, groepnaam);
        }

        public void GebruikerAanmaken(string gebruikersnaam, string wachtwoord)
        {
            iad.CreateUserAccount(gebruikersnaam, wachtwoord);
        }
    }
}