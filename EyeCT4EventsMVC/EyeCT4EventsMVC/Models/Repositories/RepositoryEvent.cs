// <copyright file="RepositoryEvent.cs" company="Unitech">
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
    public class RepositoryEvent
    {
        private IEvent context;

        public RepositoryEvent(IEvent context)
        {
            this.context = context;
        }

        public void EventAanmaken(Event events)
        {
            context.EventAanmaken(events);
        }

        public List<Event> AlleEvents()
        {
            return context.AlleEvents();
        }

        public void EventVerwijderen(int eventID)
        {
            context.EventVerwijderen(eventID);
        }
    }
}