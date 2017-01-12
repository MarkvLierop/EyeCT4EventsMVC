// <copyright file="Materiaal.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using EyeCT4EventsMVC.Models.Persistencies;
using EyeCT4EventsMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Domain_Classes
{
    public class Materiaal
    {
        public int MateriaalID { get; set; }

        public string Naam { get; set; }

        public decimal Prijs { get; set; }

        public int Aantal { get; set; }
        public string Merk { get; set; }
        public string Categorie { get; set; }

        public Materiaal()
        {
        }

        public override string ToString()
        {
            return "Item: " + Naam + " Prijs: " + Prijs + " Aantal beschikbaar: " + Aantal;
        }
        
        public List<string> MateriaalCategorieën()
        {
            RepositoryMateriaal RepoMateriaal = new RepositoryMateriaal(new MSSQLReserveren());
            return RepoMateriaal.MateriaalCategorieën();
        }

        public int CategorieID(string categorie)
        {
            RepositoryMateriaal repoMateriaal = new RepositoryMateriaal(new MSSQLReserveren());
            return repoMateriaal.CategorieID(categorie);
        }

        public void ToevoegenMateriaal(Materiaal materiaal)
        {
            RepositoryMateriaal repomateriaal = new RepositoryMateriaal(new MSSQLReserveren());
            repomateriaal.ToevoegenMateriaal(materiaal);
        }

        public List<Materiaal> HaalMateriaalOp()
        {
            RepositoryMateriaal repoMateriaal = new RepositoryMateriaal(new MSSQLReserveren());
            return repoMateriaal.HaalMaterialenOp();
        }
    }
}