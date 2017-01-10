// <copyright file="MSSQLLocatie.cs" company="Unitech">
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
    public class MSSQLLocatie : MSSQLServer, ILocatie 
    {
        private int locatieID;

        public List<Locatie> AlleLocaties()
        {
            List<Locatie> alleLocaties = new List<Locatie>();
            Connect();
                string query = "SELECT * FROM Locatie";
                using (command = new SqlCommand(query, sQLcon))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        alleLocaties.Add(CreateLocatieFromReader(reader));
                    }
                }

            return alleLocaties;
        }

        public int LocatieBijNaam(string naam)
        {
            Connect();
            string query = "SELECT ID From Locatie WHERE Naam LIKE @Naam";
            using (command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.AddWithValue("@Naam", naam);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    locatieID = reader.GetInt32(0);
                }
            }

            return locatieID;
        }

        private Locatie CreateLocatieFromReader(SqlDataReader reader)
        {
            return new Locatie(
                Convert.ToInt32(reader["ID"]),
                Convert.ToString(reader["Naam"]),
                Convert.ToString(reader["Straat"]),
                Convert.ToInt32(reader["Huisnummer"]),
                Convert.ToString(reader["Postcode"]),
                Convert.ToString(reader["Woonplaats"]));
        }
    }
}
