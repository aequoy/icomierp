using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICOMI_DOMAIN
{
    public class IcomiTask
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string TileTitle { get; set; }
        public int NbOccurence { get; set; }
        public string User { get; set; }
        public string Emplacement { get; set; }
        public string HeureDebut { get; set; }
        public string HeureFin { get; set; }
        public string Description { get; set; }
        public IcomiTaskState Etat { get; set; }

        public string NomComplet { get; set; }

        public IcomiTask(string nomComplet)
        {
            this.NomComplet = nomComplet;
            this.Etat = IcomiTaskState.Encours;
        }
    }

    public enum IcomiTaskState
    {
        Encours, Realisee, Probleme
    }
}
