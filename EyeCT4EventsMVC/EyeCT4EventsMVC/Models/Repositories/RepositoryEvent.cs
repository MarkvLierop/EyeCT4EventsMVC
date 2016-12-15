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

        public void EventAanmaken(Event events)
        {
            Context.EventAanmaken(events);
        }

        public List<Event> AlleEvents()
        {
            return Context.AlleEvents();
        }

        public void EventVerwijderen(int EventID)
        {
            Context.EventVerwijderen(EventID);
        }
    }
}