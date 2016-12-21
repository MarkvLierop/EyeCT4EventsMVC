using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Repositories
{
    public class RepositoryReactie
    {
        private IReactie Context;

        public RepositoryReactie(IReactie context)
        {
            Context = context;
        }

        public List<Reactie> ReactieBijMedia(int MediaID)
        {
            return Context.ReactieBijMedia(MediaID);
        }
    }
}