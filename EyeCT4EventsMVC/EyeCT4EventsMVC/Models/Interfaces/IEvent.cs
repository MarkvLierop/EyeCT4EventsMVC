// <copyright file="IEvent.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeCT4EventsMVC.Models.Domain_Classes;

namespace EyeCT4EventsMVC.Models.Interfaces
{
    public interface IEvent
    {
        void EventAanmaken(Event events);

        List<Event> AlleEvents();

        void EventVerwijderen(int eventID);
    }
}
