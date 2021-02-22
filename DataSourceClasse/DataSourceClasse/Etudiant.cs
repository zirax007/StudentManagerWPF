using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSourceClasse
{
    public class Etudiant
    {
        public string cne;
        public string nom;
        public string prenom;
        public char sexe;
        public string date_naissance;
        public string photo;
        public string nom_filiere;
        public int annee;

        public Etudiant(string cne, string nom, string prenom, char sex, string date_naissance, string photo, string nom_filiere, int annee)
        {
            this.cne = cne;
            this.nom = nom;
            this.prenom = prenom;
            this.sexe = sex;
            this.date_naissance = date_naissance;
            this.photo = photo;
            this.nom_filiere = nom_filiere;
            this.annee = annee;
        }


    }
}
