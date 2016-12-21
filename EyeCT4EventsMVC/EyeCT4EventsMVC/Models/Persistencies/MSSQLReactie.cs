using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Persistencies
{
    public class MSSQLReactie:MSSQLServer, IReactie 
    {
        public List<Reactie> ReactieBijMedia(int MediaID)
        {
            List<Reactie> reacties = new List<Reactie>();
            Connect();
            string query = "Select * From Reactie where Media_ID = @MediaID";
            using (command = new SqlCommand(query, SQLcon))
            {
                command.Parameters.AddWithValue("@MediaID", MediaID);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    reacties.Add(CreateReactieFromReader(reader));
                }
            }
            return reacties;
        }

        public Reactie CreateReactieFromReader(SqlDataReader reader)
        {
            return new Reactie(
                Convert.ToInt32(reader["ID"]),
                Convert.ToInt32(reader["Gebruiker_ID"]),
                Convert.ToInt32(reader["Media_ID"]),
                Convert.ToInt32(reader["Likes"]),
                Convert.ToString(reader["Inhoud"]),
                Convert.ToDateTime(reader["DatumTijd"]));
        }
    }
}