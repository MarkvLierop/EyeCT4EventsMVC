// <copyright file="MSSQLReactie.cs" company="Unitech">
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
    public class MSSQLReactie : MSSQLServer, IReactie 
    {
        public List<Reactie> ReactieBijMedia(int mediaID)
        {
            List<Reactie> reacties = new List<Reactie>();
            Connect();
            string query = "Select * From Reactie where Media_ID = @MediaID";
            using (command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.AddWithValue("@MediaID", mediaID);
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