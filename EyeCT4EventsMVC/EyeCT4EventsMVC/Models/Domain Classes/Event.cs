using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class Event
    {
        public string Beschrijving { get; set; }
        public DateTime DatumTot { get; set; }
        public DateTime DatumVan { get; set; }
        public int ID { get; set; }
        public string Locatie { get; set; }
        public int LocatieID { get; set; }
        public string Titel { get; set; }
        private RepositoryEvent RepoEvent;
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

        public Event EventAanmaken(Event events)
        {
            RepoEvent = new RepositoryEvent(new MSSQLEvent());
            return RepoEvent.EventAanmaken(events);
        }

        public string GetEventData()
        {
            return Titel + "\n Van: " + DatumVan + "\n Locatie: " + Locatie + "\n";
        }

        public string BeschrijvingInsertEnters()
        {
            string beschrijving = Beschrijving;

            if (beschrijving.Length > 35)
                beschrijving = beschrijving.Insert(35, "\n");
            if (beschrijving.Length > 70)
                beschrijving = beschrijving.Insert(70, "\n");
            if (beschrijving.Length > 105)
                beschrijving = beschrijving.Insert(105, "\n");
            if (beschrijving.Length > 140)
                beschrijving = beschrijving.Insert(140, "\n");

            return beschrijving;
        }
        public override string ToString()
        {
            return Titel + " " + DatumVan.Date + "  " + DatumVan.Hour + ":" + DatumVan.Minute;
        }
    }
}