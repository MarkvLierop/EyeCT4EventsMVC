// <copyright file="RepositoryReactie.cs" company="Unitech">
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
    public class RepositoryReactie
    {
        public IReactie Context;

        public RepositoryReactie(IReactie context)
        {
            Context = context;
        }

        public List<Reactie> ReactieBijGerapporteerdeMedia()
        {
            return Context.ReactieBijGerapporteerdeMedia();
        }

        public List<Reactie> GerapporteerdeReactie()
        {
            return Context.GerapporteerdeReactie();
        }
    }
}