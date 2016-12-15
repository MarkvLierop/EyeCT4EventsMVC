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
        public void EventAanmaken(Event events)
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
        }

        public List<Event> AlleEvents()
        {
            List<Event> AlleEvents = new List<Event>();
            Connect();
            string query = "SELECT l.Naam, e.* FROM EventInfo e, Locatie l WHERE l.ID = e.Locatie_ID";
            using (command = new SqlCommand(query, SQLcon))
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AlleEvents.Add(CreateEventFromReader(reader));
                }
            }
            return AlleEvents;
        }

        public void EventVerwijderen(int EventID)
        {
            Connect();
            string query = "DELETE FROM EventInfo WHERE ID = @EventID";
            using(command = new SqlCommand(query, SQLcon))
            {
                command.Parameters.AddWithValue("@EventID", EventID);
                command.ExecuteNonQuery();
            }
        }

        private Event CreateEventFromReader(SqlDataReader reader)
        {
            return new Event(
                Convert.ToInt32(reader["ID"]),
                Convert.ToString(reader["Naam"]),
                Convert.ToDateTime(reader["DatumVan"]),
                Convert.ToDateTime(reader["DatumTot"]),
                Convert.ToString(reader["Titel"]),
                Convert.ToString(reader["Beschrijving"]));
        }
    }
}