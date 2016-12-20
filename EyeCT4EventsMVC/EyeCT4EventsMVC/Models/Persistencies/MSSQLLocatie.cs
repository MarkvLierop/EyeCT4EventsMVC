using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Persistencies
{
    public class MSSQLLocatie:MSSQLServer, ILocatie 
    {
        private Locatie locatie;

        public List<Locatie> AlleLocaties()
        {
            List<Locatie> AlleLocaties = new List<Locatie>();
            Connect();
                string query = "SELECT * FROM Locatie";
                using (command = new SqlCommand(query, SQLcon))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        AlleLocaties.Add(CreateLocatieFromReader(reader));
                    }
                }
            return AlleLocaties;
        }

        public Locatie LocatieBijNaam(string naam)
        {
            Connect();
            string query = "SELECT * From Locatie WHERE Naam LIKE @Naam";
            using(command = new SqlCommand(query, SQLcon))
            {
                command.Parameters.AddWithValue("@Naam", naam);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    locatie = CreateLocatieFromReader(reader);
                }
            }
            return locatie;
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
