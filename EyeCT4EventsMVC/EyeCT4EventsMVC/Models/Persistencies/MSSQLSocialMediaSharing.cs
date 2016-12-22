using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;
using EyeCT4EventsMVC.Models.Exceptions;
using EyeCT4EventsMVC.Models.Interfaces;

namespace EyeCT4EventsMVC.Models.Persistencies
{
    public class MSSQLSocialMediaSharing : MSSQLServer, ISocialMediaSharing
    {
        private string BestandsTypeDefinieren(string type)
        {
            switch (type.ToLower())
            {
                case ".png":
                    type = "Afbeelding";
                    break;
                case ".jpg":
                    type = "Afbeelding";
                    break;
                case ".tiff":
                    type = "Afbeelding";
                    break;
                case ".jpeg":
                    type = "Afbeelding";
                    break;
                case ".gif":
                    type = "Afbeelding";
                    break;
                case ".bmp":
                    type = "Afbeelding";
                    break;
                case ".mp4":
                    type = "Video";
                    break;
                case ".avi":
                    type = "Video";
                    break;
                case ".wmv":
                    type = "Video";
                    break;
                case ".flv":
                    type = "Video";
                    break;
                case ".vob":
                    type = "Video";
                    break;
                case ".mpeg":
                    type = "Video";
                    break;
                case ".mpg":
                    type = "Video";
                    break;
                case ".mp3":
                    type = "Audio";
                    break;
                case ".wav":
                    type = "Audio";
                    break;
                case ".m4a":
                    type = "Audio";
                    break;
                case ".wma":
                    type = "Audio";
                    break;
                default:
                    type = "Bericht";
                    break;
            }
            return type;
        }

        private List<string> GetNietGeaccepteerdeWoorden()
        {
            List<string> nietGeaccepteerdeWoorden = new List<string>();

            using (StreamReader srVerbergThreshHold = new StreamReader("NietGeaccepteerdeWoorden.txt", false))
            {
                while (srVerbergThreshHold.ReadLine() != null)
                {
                    nietGeaccepteerdeWoorden.Add(srVerbergThreshHold.ReadLine().ToLower());
                }
            }

            return nietGeaccepteerdeWoorden;
        }
        private void CheckVoorSubCategorien(List<Categorie> catLijst, Categorie[] catArray, Categorie cat)
        {
            foreach (Categorie c in catLijst)
            {
                if (c.Parent == cat.ID)
                {
                    int x = 0;
                    Start:
                    if (catArray[x] == null)
                    {
                        catArray[x] = c;
                        CheckVoorSubCategorien(catLijst, catArray, c);
                    }
                    else
                    {
                        x++;
                        goto Start;
                    }
                }
            }
        }

        private decimal GetMediaVerbergThreshhold()
        {
            using (StreamReader srVerbergThreshHold = new StreamReader("Settings.txt", false))
            {
                string line = srVerbergThreshHold.ReadLine();
                string[] data = line.Split(':');
                return Convert.ToDecimal(data[data.Count() - 1]);
            }
        }

        public List<Media> AlleMediaOpvragen()
        {
            List<Media> mediaList = new List<Media>();

            Connect();
            try
            {
                string query = "SELECT * FROM Media WHERE Flagged < @VerbergThreshhold ORDER BY ID DESC";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@VerbergThreshhold", 10));   //AANPASSEN
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Media media = new Media();
                        media.ID = Convert.ToInt32(reader["ID"]);
                        media.Beschrijving = reader["Beschrijving"].ToString();
                        media.Pad = reader["BestandPad"].ToString();
                        media.Type = reader["MediaType"].ToString();
                        media.Categorie = Convert.ToInt32(reader["Categorie_ID"]);
                        media.Flagged = Convert.ToInt32(reader["Flagged"]);
                        media.Likes = Convert.ToInt32(reader["Likes"]);
                        media.GeplaatstDoor = Convert.ToInt32(reader["Gebruiker_ID"]);
                        mediaList.Add(media);
                    }
                }
            }
            catch (Exception e)
            {
                Close();
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
            return mediaList;
        }
        public void SchoolAbusievelijkTaalgebruikOp()
        {
            List<Reactie> reactielijst = AlleReactiesOpvragen();
            List<Media> mediaLijst = AlleMediaOpvragen();
            List<string> nietGeaccepteerdeWoorden = GetNietGeaccepteerdeWoorden();

            List<Media> teVerwijderenMedia = new List<Media>();
            List<Reactie> teVerwijderenReacties = new List<Reactie>();

            try
            {
                // Alle media selecteren waar niet geaccepteerde woorden in voorkomen
                foreach (string s in nietGeaccepteerdeWoorden)
                {
                    Connect();
                    string query = "SELECT * FROM Media WHERE Beschrijving LIKE @zoekterm";
                    using (command = new SqlCommand(query, SQLcon))
                    {
                        command.Parameters.Add(new SqlParameter("@zoekterm", "%" + s + "%"));
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Media media = new Media();
                            media.ID = Convert.ToInt32(reader["ID"]);
                            teVerwijderenMedia.Add(media);
                        }
                    }
                    Close();
                }
            }
            catch (Exception e)
            {
                Close();
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            try
            {
                // Alle reacties selecteren waarin niet geaccepteerde woorden in voorkomen
                foreach (string s in nietGeaccepteerdeWoorden)
                {
                    Connect();
                    string query = "SELECT * FROM Reactie WHERE Inhoud LIKE @zoekterm";
                    using (command = new SqlCommand(query, SQLcon))
                    {
                        command.Parameters.Add(new SqlParameter("@zoekterm", "%" + s + "%"));
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Reactie reactie = new Reactie();
                            reactie.ReactieID = Convert.ToInt32(reader["ID"]);
                            teVerwijderenReacties.Add(reactie);
                        }
                    }
                    Close();
                }
            }
            catch (Exception e)
            {
                Close();
                throw new FoutBijUitvoerenQueryException(e.Message);
            }

            foreach (Media m in teVerwijderenMedia)
            {
                VerwijderMedia(m);
            }
            foreach (Reactie r in teVerwijderenReacties)
            {
                VerwijderReactie(r);
            }
        }

        public List<Media> AlleGerapporteerdeMediaOpvragen()
        {
            List<Media> mediaList = new List<Media>();

            Connect();
            try
            {
                string query = "SELECT * FROM Media WHERE Flagged >= @VerbergThreshhold ORDER BY ID DESC";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.AddWithValue("@VerbergThreshhold", 0); //Moet nog aangepast worden
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        mediaList.Add(CreateMediaFromReader(reader));
                    }
                }
            }
            catch (Exception e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
            return mediaList;
        }
        public Media GetMediaByID(int ID)
        {
            Media media = null;
            Connect();
            try
            {
                string query = "SELECT * FROM Media WHERE ID = @ID";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@ID", ID));
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        media.ID = Convert.ToInt32(reader["ID"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();

            return media;
        }
        public void ToevoegenMedia(Media media)
        {
            Connect();
            try
            {
                string query = "INSERT INTO Media VALUES (@GeplaatstDoor, @Categorie, @Beschrijving, @Pad, @Type, 0, 0)";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@GeplaatstDoor", media.GeplaatstDoor));
                    command.Parameters.Add(new SqlParameter("@Categorie", media.Categorie));
                    command.Parameters.Add(new SqlParameter("@Pad", media.Pad));
                    command.Parameters.Add(new SqlParameter("@Type", BestandsTypeDefinieren(media.Type)));
                    command.Parameters.Add(new SqlParameter("@Beschrijving", media.Beschrijving));

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
        }
        public void ToevoegenLikeInMediaOfReactie(Gebruiker gebruiker, int mediaID, int reactieID)
        {
            // INSERT +1 Like INTO Media
            if (reactieID == int.MinValue)
            {
                Connect();
                try
                {
                    string query = "UPDATE Media SET Likes= (SELECT Likes FROM Media WHERE ID = @mediaID)+1 WHERE ID = @mediaID";
                    using (command = new SqlCommand(query, SQLcon))
                    {
                        command.Parameters.Add(new SqlParameter("@mediaID", mediaID));

                        command.ExecuteNonQuery();
                    }
                    //string insert = @"INSERT INTO [Like](GebruikerID,MediaID) VALUES(@gebruikerID, @mediaID)";
                    //using (command = new SqlCommand(insert, SQLcon))
                    //{
                    //    command.Parameters.Add(new SqlParameter("@gebruikerID", 1)); // AANPASSEN ALS LOGIN WERKT gebruiker.GebruikersID
                    //    command.Parameters.Add(new SqlParameter("@mediaID", mediaID));

                    //    command.ExecuteNonQuery();
                    //}
                }
                catch (SqlException e)
                {
                    throw new FoutBijUitvoerenQueryException(e.Message);
                }
                Close();
            }
            // INSERT +1 Like INTO Reactie
            else if (mediaID == int.MinValue)
            {
                Connect();
                try
                {
                    string query = "UPDATE Reactie SET Likes= (SELECT Likes FROM Reactie WHERE ID = @reactieID)+1 WHERE ID = @reactieID";
                    using (command = new SqlCommand(query, SQLcon))
                    {
                        command.Parameters.Add(new SqlParameter("@reactieID", reactieID));

                        command.ExecuteNonQuery();
                    }
                    //string insert = "INSERT INTO [Like](GebruikerID, ReactieID) VALUES(@gebruikerID, @reactieID)";
                    //using (command = new SqlCommand(insert, SQLcon))
                    //{
                    //    command.Parameters.Add(new SqlParameter("@gebruikerID", 1)); // AANPASSEN ALS LOGIN WERKT gebruiker.GebruikersID
                    //    command.Parameters.Add(new SqlParameter("@reactieID", reactieID));

                    //    command.ExecuteNonQuery();
                    //}
                }
                catch (SqlException e)
                {
                    throw new FoutBijUitvoerenQueryException(e.Message);
                }

                Close();
            }
        }
        public void ToevoegenRapporterenMediaReactie(int mediaID, int reactieID)
        {
            if (reactieID == int.MinValue)
            {
                Connect();
                try
                {
                    string query = "UPDATE Media SET Flagged= (SELECT Flagged FROM Media WHERE ID = @mediaID)+1 WHERE ID = @mediaID";
                    using (command = new SqlCommand(query, SQLcon))
                    {
                        command.Parameters.Add(new SqlParameter("@mediaID", mediaID));

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    throw new FoutBijUitvoerenQueryException(e.Message);
                }
                Close();
            }
            else if (mediaID == int.MinValue)
            {
                Connect();
                try
                {
                    string query = "UPDATE Reactie SET Flagged= (SELECT Flagged FROM Reactie WHERE ID = @reactieID)+1 WHERE ID = @reactieID";
                    using (command = new SqlCommand(query, SQLcon))
                    {
                        command.Parameters.Add(new SqlParameter("@reactieID", reactieID));

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    throw new FoutBijUitvoerenQueryException(e.Message);
                }
                Close();
            }
        }
        // Zoeken op CategorieID & MediaType
        public List<Media> ZoekenMedia(string zoekterm, int ID)
        {
            List<Media> medialist = new List<Media>();
            Connect();
            try
            {
                string query = "SELECT * FROM Media WHERE Flagged < @Threshhold AND (Categorie_ID = @ID) ORDER BY ID DESC";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@zoekterm", "%" + zoekterm + "%"));
                    command.Parameters.Add(new SqlParameter("@ID", ID));
                    command.Parameters.Add(new SqlParameter("@Threshhold", 9));  // NOG AANPASSEN
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Media media = new Media();
                        media.ID = Convert.ToInt32(reader["ID"]);
                        media.Beschrijving = reader["Beschrijving"].ToString();
                        media.Pad = reader["BestandPad"].ToString();
                        media.Type = reader["MediaType"].ToString();
                        media.Categorie = Convert.ToInt32(reader["Categorie_ID"]);
                        media.Flagged = Convert.ToInt32(reader["Flagged"]);
                        media.Likes = Convert.ToInt32(reader["Likes"]);
                        media.GeplaatstDoor = Convert.ToInt32(reader["Gebruiker_ID"]);
                        medialist.Add(media);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
            
            return medialist;
        }
        public void ZetAantalKerenGerapporteerdOp0(Media media)
        {
            Connect();
            try
            {
                string query = "UPDATE Media SET Flagged = 0 WHERE ID = @mediaID";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@mediaID", media.ID));

                    command.ExecuteNonQuery();
                }
                Close();
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
        }
        public void VerwijderMedia(Media media)
        {
            List<Reactie> reactielijst = AlleReactiesOpvragen();
            foreach (Reactie r in reactielijst)
            {
                if (r.MediaID == media.ID)
                {
                    VerwijderReactie(r);
                }
            }
            Connect();
            try
            {
                string query = "DELETE FROM Media WHERE ID = @ID";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@ID", media.ID));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
        }
        public Media SelectLaatstIngevoerdeMedia()
        {
            Media media = null;
            Connect();
            try
            {
                string query = "SELECT MAX(ID) maxID FROM Media";
                using (command = new SqlCommand(query, SQLcon))
                {
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        media = new Media();

                        if (reader["maxID"].GetType() != typeof(DBNull))
                        {
                            media.ID = Convert.ToInt32(reader["maxID"]);
                        }
                        else
                        {
                            media.ID = 1;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();

            return media;
        }


        public Categorie[] AlleCategorienOpvragen()
        {
            List<Categorie> categorieLijst = new List<Categorie>();
            string query;
            Connect();
            try
            {
                query = "SELECT COUNT(*) 'aantalCategorien' FROM Categorie";
                using (command = new SqlCommand(query, SQLcon))
                {
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        categorieArray = new Categorie[Convert.ToInt32(reader["aantalCategorien"])];
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();

            Connect();
            try
            {
                query = "SELECT * FROM Categorie";
                using (command = new SqlCommand(query, SQLcon))
                {
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Categorie cat = new Categorie();
                        cat.Naam = reader["Naam"].ToString();
                        cat.ID = Convert.ToInt32(reader["ID"]);
                        cat.Parent = Convert.ToInt32(reader["Categorie_ID"]);
                        categorieLijst.Add(cat);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();

            for (int i = 0; i < categorieLijst.Count; i++)
            {
                if (!categorieArray.Contains(categorieLijst[i]))
                {
                    int a = i;
                    Start:
                    if (categorieArray[a] == null)
                    {
                        categorieArray[a] = categorieLijst[i];
                        CheckVoorSubCategorien(categorieLijst, categorieArray, categorieArray[a]);
                    }
                    else
                    {
                        a++;
                        goto Start;
                    }
                }
            }
            return categorieArray;
        }
        public void ToevoegenCategorie(Categorie cat)
        {
            Connect();
            try
            {
                string query = "INSERT INTO Categorie VALUES (@Naam, @ParentCat)";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@Naam", cat.Naam));
                    command.Parameters.Add(new SqlParameter("@ParentCat", cat.Parent));

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
        }
        public Categorie GetCategorieMetNaam(string naam)
        {
            Categorie cat = null;
            Connect();
            try
            {
                string query = "SELECT * FROM Categorie WHERE Naam = @naam";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@naam", naam));
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cat = new Categorie();
                        cat.ID = Convert.ToInt32(reader["ID"]);
                        cat.Naam = reader["Naam"].ToString();
                        cat.Parent = Convert.ToInt32(reader["ParentCategorie"]);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();

            return cat;
        }

        public List<Categorie> ZoekenCategorie(string naam)
        {
            List<Categorie> catlist = new List<Categorie>();
            Connect();
            try
            {
                string query = "SELECT * FROM Categorie WHERE Naam LIKE @naam";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@naam", "%" + naam + "%"));
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Categorie cat = new Categorie();
                        cat.ID = Convert.ToInt32(reader["ID"]);
                        cat.Naam = reader["Naam"].ToString();
                        cat.Parent = Convert.ToInt32(reader["ParentCategorie"]);
                        catlist.Add(cat);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();

            return catlist;
        }

        public void VerwijderReactie(Reactie reactie)
        {
            Connect();
            try
            {
                string query = "DELETE FROM Reactie WHERE ID = @ID";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@ID", reactie.ReactieID));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
        }
        public List<Reactie> AlleReactiesOpvragen(Media media)
        {
            List<Reactie> reactieLijst = new List<Reactie>();
            Connect();
            try
            {
                string query = "SELECT * FROM Reactie WHERE Media_ID = @mediaID ORDER BY ID DESC";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@mediaID", media.ID));
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Reactie reactie = new Reactie();
                        reactie.DatumTijd = Convert.ToDateTime(reader["DatumTijd"]);
                        reactie.Flagged = Convert.ToInt32(reader["Flagged"]);
                        reactie.GeplaatstDoor = Convert.ToInt32(reader["Gebruiker_ID"]);
                        reactie.Inhoud = reader["Inhoud"].ToString();
                        reactie.MediaID = Convert.ToInt32(reader["Media_ID"]);
                        reactie.ReactieID = Convert.ToInt32(reader["ID"]);
                        reactieLijst.Add(reactie);
                    }
                }
            }
            catch (SqlException e)
            {
                Close();
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
            return reactieLijst;
        }
        private List<Reactie> AlleReactiesOpvragen()
        {
            List<Reactie> reactieLijst = new List<Reactie>();
            Connect();
            try
            {
                string query = "SELECT * FROM Reactie ORDER BY ID DESC";
                using (command = new SqlCommand(query, SQLcon))
                {
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Reactie reactie = new Reactie();
                        reactie.DatumTijd = Convert.ToDateTime(reader["DatumTijd"]);
                        reactie.Flagged = Convert.ToInt32(reader["Flagged"]);
                        reactie.GeplaatstDoor = Convert.ToInt32(reader["Gebruiker_ID"]);
                        reactie.Inhoud = reader["Inhoud"].ToString();
                        reactie.MediaID = Convert.ToInt32(reader["Media_ID"]);
                        reactie.ReactieID = Convert.ToInt32(reader["ID"]);
                        reactieLijst.Add(reactie);
                    }
                }
            }
            catch (SqlException e)
            {
                Close();
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
            return reactieLijst;
        }
        public void ToevoegenReactie(Reactie reactie)
        {
            Connect();
            try
            {
                string query = "INSERT INTO Reactie VALUES (@geplaatstDoor, @mediaID, 0, @inhoud, @datetime, 0)";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@geplaatstDoor", 1)); // Later aanpassen
                    command.Parameters.Add(new SqlParameter("@mediaID", reactie.MediaID));
                    command.Parameters.Add(new SqlParameter("@inhoud", reactie.Inhoud));
                    command.Parameters.Add(new SqlParameter("@datetime", DateTime.Now.ToString()));
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
        }

        public void ToevoegenEvent(Event ev)
        {
            Connect();
            try
            {
                string query = "INSERT INTO Event VALUES (@locatie, @datumVan, @titel, @beschrijving, @datumTot)";
                using (command = new SqlCommand(query, SQLcon))
                {
                    command.Parameters.Add(new SqlParameter("@locatie", ev.Locatie));
                    command.Parameters.Add(new SqlParameter("@datumVan", ev.DatumVan));
                    command.Parameters.Add(new SqlParameter("@titel", ev.Titel));
                    command.Parameters.Add(new SqlParameter("@beschrijving", ev.Beschrijving));
                    command.Parameters.Add(new SqlParameter("@datumTot", ev.DatumTot));
                    command.ExecuteNonQuery();
                }
            }

            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();
        }

        public List<Event> AlleEventsOpvragen()
        {
            List<Event> eventList = new List<Event>();
            Connect();
            try
            {
                string query = "SELECT * FROM Event ORDER BY DatumVan DESC";
                using (command = new SqlCommand(query, SQLcon))
                {
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Event e = new Event();
                        e.Beschrijving = reader["Beschrijving"].ToString();
                        e.Titel = reader["Titel"].ToString();
                        e.Locatie = reader["Locatie"].ToString();
                        e.ID = Convert.ToInt32(reader["ID"]);
                        e.DatumVan = Convert.ToDateTime(reader["DatumVan"]);
                        e.DatumTot = Convert.ToDateTime(reader["DatumTot"]);
                        eventList.Add(e);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FoutBijUitvoerenQueryException(e.Message);
            }
            Close();

            return eventList;
        }

        public void VerwijderMedia(int MediaID)
        {
            Connect();
            string[] query = new string[2];
            query[0] = "DELETE FROM Media WHERE ID = @MediaID";
            query[1] = "DELETE FROM Reactie WHERE Media_ID = @MediaID";
            for(int i =0; i< query.Length; i++)
            {
                using(command = new SqlCommand(query[i], SQLcon))
                {
                    command.Parameters.AddWithValue("@MediaID", MediaID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Media CreateMediaFromReader(SqlDataReader reader)
        {
            Media media = new Media();
            media.ID = Convert.ToInt32(reader["ID"]);
            media.GeplaatstDoor = Convert.ToInt32(reader["Gebruiker_ID"]);
            media.Categorie = Convert.ToInt32(reader["Categorie_ID"]);
            media.Pad = Convert.ToString(reader["BestandPad"]);
            media.Type = Convert.ToString(reader["MediaType"]);
            media.Likes = Convert.ToInt32(reader["Likes"]);
            media.Geplaats = Convert.ToDateTime(reader["DatumTijd"]);
            media.Beschrijving = Convert.ToString(reader["Beschrijving"]);
            media.Flagged = Convert.ToInt32(reader["Flagged"]);
            return media;
        }
    }
}