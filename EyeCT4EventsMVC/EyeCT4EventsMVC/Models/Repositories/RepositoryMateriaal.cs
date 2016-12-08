using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Interfaces;

namespace EyeCT4EventsMVC.Models.Repositories
{
    public class RepositoryMateriaal
    {
        IMateriaal IM;

        public RepositoryMateriaal(IMateriaal im)
        {
            IM = im;
        }

        public void ToevoegenMateriaal(string naam, decimal prijs, decimal voorraad)
        {
            IM.ToevoegenMateriaal(naam, prijs, voorraad);
        }
        public List<Materiaal> HaalMaterialenOp()
        {
            return IM.HaalMaterialenOp();
        }

        public void ReserveerMateriaal(int gebruikerid, int materiaalid, int aantal, DateTime datum)
        {
            IM.ReserveerMateriaal(gebruikerid, materiaalid, aantal, datum);
        }

        public void WerkVoorraadBij(int voorraad, int id)
        {
            IM.WerkVoorraadBij(voorraad, id);
        }
    }
}