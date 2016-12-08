using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Interfaces;

namespace EyeCT4EventsMVC.Models.Repositories
{
    public class RepositoryKampeerPlaatsen
    {
        IKampeerplaats context;

        public RepositoryKampeerPlaatsen(IKampeerplaats context)
        {
            this.context = context;
        }

        public List<Kampeerplaats> KampeerplaatsenOpvragen(int comfort, int invalide, int lawaai, string eigentent,
                                     string bungalow, string bungalino, string blokhut, string stacaravan, string huurtent)
        {
            return context.KampeerplaatsenOpvragen(comfort, invalide, lawaai, eigentent, bungalow, bungalino, blokhut,
                                            stacaravan, huurtent);
        }

        public List<Kampeerplaats> AlleKampeerplaatsenOpvragen()
        {
            return context.AlleKampeerplaatsenOpvragen();
        }

        public void ReserveringPlaatsen(int gebruikerid, int plaatsid, DateTime datumVan, DateTime datumTot)
        {
            context.ReserveringPlaatsen(gebruikerid, plaatsid, datumVan, datumTot);
        }

        public Reservering HaalReserveringOpNaAanmaken(int gebruikerid, int plaatsid, DateTime datumVan, DateTime datumTot)
        {
            return context.HaalReserveringOpNaAanmaken(gebruikerid, plaatsid, datumVan, datumTot);
        }

        public void ReserveringgroepToevoegen(int verantwoordelijke, int gebruiker, int kampeerplaats, int reservering)
        {
            context.ReserveringgroepToevoegen(verantwoordelijke, gebruiker, kampeerplaats, reservering);
        }
    }
}