using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Persistencies
{
    public class MSSQLEvent:MSSQLServer, IEvent 
    {
        private Event eventen;
        public Event EventAanmaken(Event events)
        {
            Connect();
            string query = "INSERT INTO EventInfo(Locatie_ID,DatumVan,DatumTot,Titel,Beschrijving)Values(@LocatieID,@DatumVan,@DatumTot,@Titel,@Beschrijving)";
            using(command = new SqlCommand(query, SQLcon))
            {
                command.Parameters.AddWithValue("@LocatieID", events.LocatieID);
                command.Parameters.AddWithValue("@DatumVan", events.DatumVan);
                command.Parameters.AddWithValue("@DatumTot", events.DatumTot);
                command.Parameters.AddWithValue("@Titel", events.Titel);
                command.Parameters.AddWithValue("@Beschrijving", events.Beschrijving);
                command.ExecuteNonQuery();
            }
            return eventen;
        }
    }
}