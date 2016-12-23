// <copyright file="RepositoryLocatie.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Interfaces;

namespace EyeCT4EventsMVC.Models.Repositories
{
    public class RepositoryLocatie
    {
        public ILocatie Context;

        public RepositoryLocatie(ILocatie context)
        {
            Context = context;
        }

        public List<Locatie> AlleLocaties()
        {
            return Context.AlleLocaties();
        }

        public int LocatieBijNaam(string naam)
        {
            return Context.LocatieBijNaam(naam);
        }
    }
}