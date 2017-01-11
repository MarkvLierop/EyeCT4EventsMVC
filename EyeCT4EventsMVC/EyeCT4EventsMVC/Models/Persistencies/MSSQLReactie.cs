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
        List<Reactie> reacties;
        public List<Reactie> ReactieBijGerapporteerdeMedia()
        {
            reacties = new List<Reactie>();
            Connect();
            string query = "Select * From Reactie";
            using (command = new SqlCommand(query, sQLcon))
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    reacties.Add(CreateReactieFromReader(reader));
                }
            }

            return reacties;
        }

        public List<Reactie> GerapporteerdeReactie()
        {
            reacties = new List<Reactie>();
            Connect();
            string query = "Select * from Reactie where Flagged > @AantalGerapporteerd;";
            using(command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.AddWithValue("@AantalGerapporteerd", 0);
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