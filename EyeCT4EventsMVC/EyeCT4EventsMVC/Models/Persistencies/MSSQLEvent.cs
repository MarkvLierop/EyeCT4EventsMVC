// <copyright file="MSSQLEvent.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Interfaces;

namespace EyeCT4EventsMVC.Models.Persistencies
{
    public class MSSQLEvent : MSSQLServer, IEvent 
    {
        public void EventAanmaken(Event events)
        {
            Connect();
            string query = "INSERT INTO EventInfo(Locatie_ID,DatumVan,DatumTot,Titel,Beschrijving)Values(@LocatieID,@DatumVan,@DatumTot,@Titel,@Beschrijving)";
            using (command = new SqlCommand(query, sQLcon))
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
            List<Event> alleEvents = new List<Event>();
            Connect();
            string query = "SELECT l.Naam, e.* FROM EventInfo e, Locatie l WHERE l.ID = e.Locatie_ID AND e.DatumTot >= @Nu";
            using (command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.AddWithValue("@Nu", DateTime.Now);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    alleEvents.Add(CreateEventFromReader(reader));
                }
            }

            return alleEvents;
        }

        public void EventVerwijderen(int eventID)
        {
            Connect();
            string query = "DELETE FROM EventInfo WHERE ID = @EventID";
            using (command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.AddWithValue("@EventID", eventID);
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