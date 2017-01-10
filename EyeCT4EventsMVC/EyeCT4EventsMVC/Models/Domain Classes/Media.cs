// <copyright file="Media.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Exceptions;
using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class Media
    {
        public int Categorie { get; set; }

        public int Flagged { get; set; }

        public int GeplaatstDoor { get; set; }

        public int Likes { get; set; }

        public int ID { get; set; }

        public string Type { get; set; }

        public string Pad { get; set; }

        public string Beschrijving { get; set; }

        public DateTime Geplaats { get; set; }
        
        private readonly List<Reactie> reacties = new List<Reactie>();
        private int hoogsteID;
        private RepositorySocialMediaSharing smsr;
        private RepositoryGebruiker rg;

        public Media()
        {
            smsr = new RepositorySocialMediaSharing(new MSSQLSocialMediaSharing());
            rg = new RepositoryGebruiker(new MSSQLGebruiker());
        }

        public List<Reactie> AlleReacties()
        {
            return smsr.AlleReactiesOpvragen(this);
        }

        public string FilterVastStellen()
        {
            if (Type == "Afbeelding")
            {
                return "Afbeelding|*" + GetBestandsExtentie();
            }
            else if (Type == "Audio")
            {
                return "Audio Bestand|*" + GetBestandsExtentie();
            }
            else if (Type == "Video")
            {
                return "Video Bestand|*" + GetBestandsExtentie();
            }

            return "Overige Bestanden|*" + GetBestandsExtentie();
        }

        public void BestandOpslaan(string safeFileName, string fileName)
        {
            try
            {
                if (smsr.SelectHoogsteMediaID().ID == 1)
                {
                    hoogsteID = 1;
                }
                else
                {
                    hoogsteID = smsr.SelectHoogsteMediaID().ID + 1;
                }

                string directory = "SocialMediaSharingData\\" + hoogsteID.ToString() + "\\";
                Pad = directory + safeFileName;
                Directory.CreateDirectory(directory);
                File.Copy(fileName, directory + safeFileName);
            }
                        catch (Exception e)
            {
                throw new FoutBijOpslaanBestandException(e.Message);
            }
        }

        public override string ToString()
        {
            return "Geplaatst Door: " + rg.GetGebruikerByID(GeplaatstDoor).ToString() + " | Aantal keren gerapporteerd: " + Flagged.ToString();
        }

        public string GeplaatstDoorGebruiker()
        {
            return rg.GetGebruikerByID(GeplaatstDoor).ToString();
        }

        public string GetBestandsNaam()
        {
            string[] bestandsnaam = Pad.Split('\\');
            string test = bestandsnaam[bestandsnaam.Count() - 1];
            return bestandsnaam[bestandsnaam.Count() - 1];
        }

        public List<Media> GerapporteerdeMedia()
        {
            return smsr.AlleGerapporteerdeMediaOpvragen();
        }

        private string GetBestandsExtentie()
        {
            string[] splitPad = Pad.Split('.');
            return "." + splitPad[splitPad.Count() - 1];
        }

        public void VerwijderMedia(Media media)
        {
            smsr.VerwijderMedia(media);
        }
    }
}