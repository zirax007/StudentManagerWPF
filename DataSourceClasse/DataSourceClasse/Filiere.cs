using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSourceClasse
{
    class Filiere
    {
        public int id;
        public string nom_filiere;
        public string responsable;

        public Filiere(int id, string nom_filiere, string responsable)
        {
            this.id = id;
            this.nom_filiere = nom_filiere;
            this.responsable = responsable;
        }
    }
}
