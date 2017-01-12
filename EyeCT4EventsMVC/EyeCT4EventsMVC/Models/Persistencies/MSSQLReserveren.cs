// <copyright file="MSSQLReserveren.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Exceptions;
using EyeCT4EventsMVC.Models.Interfaces;

namespace EyeCT4EventsMVC.Models.Persistencies
{
    public class MSSQLReserveren : MSSQLServer, IKampeerplaats, IMateriaal
    {
        public List<Materiaal> HaalMaterialenOp()
        {
            List<Materiaal> materialen = new List<Materiaal>();

            Connect();
            try
            {
                string query = "SELECT * FROM Materiaal WHERE HuidigeVoorraad > 0";
                using (command = new SqlCommand(query, sQLcon))
                {
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Materiaal materiaal = new Materiaal();

                        materiaal.MateriaalID = Convert.ToInt32(reader["ID"]);
                        materiaal.Naam = reader["Naam"].ToString();
                        materiaal.Prijs = Convert.ToInt32(reader["Prijs"]);
                        materiaal.Voorraad = Convert.ToInt32(reader["HuidigeVoorraad"]);

                        materialen.Add(materiaal);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }

            Close();
            return materialen;
        }

        public List<Kampeerplaats> AlleKampeerplaatsenOpvragen()
        {
            List<Kampeerplaats> kampeerList = new List<Kampeerplaats>();

            Connect();
            try
            {
                string query = "SELECT * FROM Kampeerplaats where ID NOT IN (Select Kampeerplaats_ID from Reservering) ";
                using (command = new SqlCommand(query, sQLcon))
                {
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Kampeerplaats kampeerplaats = new Kampeerplaats();

                        kampeerplaats.Type = reader["KampeerPlaatsType"].ToString();
                        kampeerplaats.ID = Convert.ToInt32(reader["ID"]);
                        kampeerplaats.Nummer = Convert.ToInt32(reader["Nummer"]);
                        kampeerplaats.MaxPersonen = Convert.ToInt32(reader["Capaciteit"]);
                        kampeerplaats.Lawaai = Convert.ToInt32(reader["Lawaai"]);
                        kampeerplaats.Invalide = Convert.ToInt32(reader["Invalide"]);
                        kampeerplaats.Comfort = Convert.ToInt32(reader["Comfort"]);
                        kampeerList.Add(kampeerplaats);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }

            Close();
            return kampeerList;
        }

        public List<Kampeerplaats> KampeerplaatsenOpvragen(int comfort, int invalide, int lawaai, string eigentent, string bungalow, string bungalino, string blokhut, string stacaravan, string huurtent)
        {
            List<Kampeerplaats> kampeerList = new List<Kampeerplaats>();

            Connect();
            try
            {
                string query = "SELECT * FROM KampeerPlaats k WHERE k.Comfort = @comfort AND k.Invalide = @invalide AND k.Lawaai = @lawaai AND (k.KampeerPlaatsType = @eigentent OR k.KampeerPlaatsType = @bungalow OR k.KampeerPlaatsType = @bungalino OR k.KampeerPlaatsType = @blokhut OR k.KampeerPlaatsType = @stacaravan OR k.KampeerPlaatsType = @huurtent);";
                using (command = new SqlCommand(query, sQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@comfort", comfort));
                    command.Parameters.Add(new SqlParameter("@invalide", invalide));
                    command.Parameters.Add(new SqlParameter("@lawaai", lawaai));
                    command.Parameters.Add(new SqlParameter("@eigentent", eigentent));
                    command.Parameters.Add(new SqlParameter("@bungalow", bungalow));
                    command.Parameters.Add(new SqlParameter("@bungalino", bungalino));
                    command.Parameters.Add(new SqlParameter("@blokhut", blokhut));
                    command.Parameters.Add(new SqlParameter("@stacaravan", stacaravan));
                    command.Parameters.Add(new SqlParameter("@huurtent", huurtent));

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Kampeerplaats kampeerplaats = new Kampeerplaats();

                        kampeerplaats.Type = reader["KampeerPlaatsType"].ToString();
                        kampeerplaats.ID = Convert.ToInt32(reader["ID"]);
                        kampeerplaats.MaxPersonen = Convert.ToInt32(reader["MaxPersonen"]);
                        kampeerplaats.Lawaai = Convert.ToInt32(reader["Lawaai"]);
                        kampeerplaats.Invalide = Convert.ToInt32(reader["Invalide"]);
                        kampeerplaats.Comfort = Convert.ToInt32(reader["Comfort"]);
                        kampeerplaats.Locatie = Convert.ToInt32(reader["Locatie"]);
                        kampeerList.Add(kampeerplaats);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }

            Close();
            return kampeerList;
        }

        public void ZetBezoekerOpAfwezig(int rfid)
        {
            Connect();
            try
            {
                string query = "UPDATE Gebruiker SET Aanwezig = 0 WHERE RFID = @RFID ";
                using (command = new SqlCommand(query, sQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@RFID", rfid));

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }

            Close();
        }

        public void ZetBezoekerOpAanwezig(int rfid)
        {
            Connect();
            try
            {
                string query = "UPDATE Gebruiker SET Aanwezig = 1 WHERE RFID = @RFID ";
                using (command = new SqlCommand(query, sQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@RFID", rfid));

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }

            Close();
        }

        public void ToevoegenMateriaal(string naam, decimal prijs, decimal voorraad)
        {
            Connect();
            string query = "INSERT INTO Materiaal VALUES (@voorraad, @naam, @prijs)";
            using (command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.Add(new SqlParameter("@voorraad", voorraad));
                command.Parameters.Add(new SqlParameter("@naam", naam));
                command.Parameters.Add(new SqlParameter("@prijs", prijs));

                command.ExecuteNonQuery();
            }

            Close();
        }

        public void ReserveringPlaatsen(int inputName, int nummer, DateTime datumVan, DateTime datumTot)
        {
            int userid = inputName;
            int id = nummer;
            DateTime dvan = datumVan;
            DateTime dtot = datumTot;
            
            Connect();
            string query = "INSERT INTO Reservering VALUES (select ID from Kampeerplaats where Nummer = Nummer)@KampeerPlaats,(select ID from Gebruiker where emailadres = emailadres)@GebruikrID, @DatumVan, @DatumTot, @Betaling)";
            using (command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.Add(new SqlParameter("@KampeerPlaats", id));
                command.Parameters.Add(new SqlParameter("@GebruikrID", userid));
                command.Parameters.Add(new SqlParameter("@DatumVan", dvan));
                command.Parameters.Add(new SqlParameter("@DatumTot", dtot));
                command.Parameters.Add(new SqlParameter("@Betaling", false));

                command.ExecuteNonQuery();
            }

            Close();
        }

        public Reservering HaalReserveringOpNaAanmaken(int gebruikerid, int plaatsid, DateTime datumvan, DateTime datumtot)
        {
            Reservering reservering = new Reservering();

            int gebruikerId = gebruikerid;
            int plaatsId = plaatsid;
            DateTime datumVan = datumvan;
            DateTime datumTot = datumtot;
            bool betaald;

            Connect();
            string query = "SELECT * FROM Reservering WHERE GebruikerID = @Gebruikerid AND KampeerPlaats = @Plaatsid AND DatumVan = @DatumVan AND DatumTot = @DatumTot";
            using (command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.Add(new SqlParameter("@Gebruikerid", gebruikerId));
                command.Parameters.Add(new SqlParameter("@Plaatsid", plaatsId));
                command.Parameters.Add(new SqlParameter("@DatumVan", datumVan));
                command.Parameters.Add(new SqlParameter("@DatumTot", datumTot));

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int gebruikerID = Convert.ToInt32(reader["GebruikerID"]);
                    int plaatsID = Convert.ToInt32(reader["KampeerPlaats"]);
                    int iD = Convert.ToInt32(reader["ID"]);
                    DateTime datumTOT = Convert.ToDateTime(reader["DatumTot"]);
                    DateTime datumVAN = Convert.ToDateTime(reader["DatumVan"]);
                    if ((bool)reader["Betaald"] == false)
                    {
                        betaald = false;

                        reservering.ReserveringID = iD;
                        reservering.BezoekerID = gebruikerID;
                        reservering.KampeerplaatsID = plaatsID;
                        reservering.DatumVan = datumVAN;
                        reservering.DatumTot = datumTOT;
                        reservering.Betaald = betaald;
                    }
                    else
                    {
                        betaald = true;

                        reservering.ReserveringID = iD;
                        reservering.BezoekerID = gebruikerID;
                        reservering.KampeerplaatsID = plaatsID;
                        reservering.DatumVan = datumVAN;
                        reservering.DatumTot = datumTOT;
                        reservering.Betaald = betaald;
                    }
                }
            }

            Close();
            return reservering;
        }

        public void ReserveringgroepToevoegen(int verantwoordelijke, int gebruiker, int kampeerplaats, int reservering)
        {
            Connect();
            string query = "INSERT INTO ReserveringGroep VALUES (@ReserveringsVerantwoordelijke, @Gebruiker, @Kampeerplaats, @Reservering)";
            using (command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.Add(new SqlParameter("@ReserveringsVerantwoordelijke", verantwoordelijke));
                command.Parameters.Add(new SqlParameter("@Gebruiker", gebruiker));
                command.Parameters.Add(new SqlParameter("@Kampeerplaats", kampeerplaats));
                command.Parameters.Add(new SqlParameter("@Reservering", reservering));

                command.ExecuteNonQuery();
            }

            Close();
        }

        public void ReserveerMateriaal(int gebruikerid, int materiaalid, int aantal, DateTime datum)
        {
            Connect();
            string query = "INSERT INTO Uitgeleend_Materiaal VALUES (@Materiaalid, @Gebruikerid, @Datum, @Aantal)";
            using (command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.Add(new SqlParameter("@Materiaalid", materiaalid));
                command.Parameters.Add(new SqlParameter("@Gebruikerid", gebruikerid));
                command.Parameters.Add(new SqlParameter("@Datum", datum));
                command.Parameters.Add(new SqlParameter("@Aantal", aantal));

                command.ExecuteNonQuery();
            }

            Close();
        }

        public void WerkVoorraadBij(int voorraad, int id)
        {
            Connect();
            string query = "UPDATE Materiaal SET HuidigeVoorraad = @Voorraad WHERE ID = @ID";
            using (command = new SqlCommand(query, sQLcon))
            {
                command.Parameters.Add(new SqlParameter("@Voorraad", voorraad));
                command.Parameters.Add(new SqlParameter("@ID", id));

                command.ExecuteNonQuery();
            }

            Close();
        }
    }
}