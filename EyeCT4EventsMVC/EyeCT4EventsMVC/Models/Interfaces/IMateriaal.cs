// <copyright file="IMateriaal.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes;

namespace EyeCT4EventsMVC.Models.Interfaces
{
    public interface IMateriaal
    {
        List<Materiaal> HaalMaterialenOp();

        void ReserveerMateriaal(int gebruikerid, int materiaalid, int aantal, DateTime datum);

        void WerkVoorraadBij(int voorraad, int id);

        void ToevoegenMateriaal(string naam, decimal prijs, decimal vooraad);
    }
}