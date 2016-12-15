<<<<<<< HEAD
﻿// <copyright file="Event.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
=======
﻿using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;
>>>>>>> f127336da0b3b241a4f71e24e5bcb7003423822c
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class Event
    {
        public string LocatieNaam { get; set; }
        public string Beschrijving { get; set; }

        public DateTime DatumTot { get; set; }

        public DateTime DatumVan { get; set; }

        public int ID { get; set; }

        public string Locatie { get; set; }
<<<<<<< HEAD

        public string Titel { get; set; }

=======
        public int LocatieID { get; set; }
        public string Titel { get; set; }
        private RepositoryEvent RepoEvent;
>>>>>>> f127336da0b3b241a4f71e24e5bcb7003423822c
        public Event()
        {
        }

        public Event(int locatieID, DateTime datumVan, DateTime datumTot, string titel, string beschrijving)
        {
            LocatieID = locatieID;
            DatumVan = datumVan;
            DatumTot = datumTot;
            Titel = titel;
            Beschrijving = beschrijving;
        }

        public Event(int id,string locatieNaam, DateTime datumVan, DateTime datumTot, string titel, string beschrijving)
        {
            ID = id;
            LocatieNaam = locatieNaam;
            DatumVan = datumVan;
            DatumTot = datumTot;
            Titel = titel;
            Beschrijving = beschrijving;
        }

        public void EventAanmaken(Event events)
        {
            RepoEvent = new RepositoryEvent(new MSSQLEvent());
            RepoEvent.EventAanmaken(events);
        }

        public List<Event> AlleEvents()
        {
            RepoEvent = new RepositoryEvent(new MSSQLEvent());
            return RepoEvent.AlleEvents();
        }

        public void EventVerwijderen(int EventID)
        {
            RepoEvent = new RepositoryEvent(new MSSQLEvent());
            RepoEvent.EventVerwijderen(EventID);
        }

        public string GetEventData()
        {
            return Titel + "\n Van: " + DatumVan + "\n Locatie: " + Locatie + "\n";
        }

        public string BeschrijvingInsertEnters()
        {
            string beschrijving = Beschrijving;

            if (beschrijving.Length > 35)
            {
                beschrijving = beschrijving.Insert(35, "\n");
            }

            if (beschrijving.Length > 70)
            {
                beschrijving = beschrijving.Insert(70, "\n");
            }

            if (beschrijving.Length > 105)
            {
                beschrijving = beschrijving.Insert(105, "\n");
            }

            if (beschrijving.Length > 140)
            {
                beschrijving = beschrijving.Insert(140, "\n");
            }

            return beschrijving;
        }

        public override string ToString()
        {
            return Titel + " " + DatumVan.Date + "  " + DatumVan.Hour + ":" + DatumVan.Minute;
        }
    }
}