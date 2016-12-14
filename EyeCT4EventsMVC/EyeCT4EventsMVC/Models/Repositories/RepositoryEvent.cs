using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Repositories
{
    public class RepositoryEvent
    {
        private IEvent Context;

        public RepositoryEvent(IEvent context)
        {
            Context = context;
        }

        public Event EventAanmaken(Event events)
        {
            return Context.EventAanmaken(events);
        }
    }
}