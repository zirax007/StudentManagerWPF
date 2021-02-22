using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerWPF
{
    class FiliereService
    {
        public static ObservableCollection<Filiere> GetEmployees()
        {
            ObservableCollection<Filiere> filieres = new ObservableCollection<Filiere>();

            string connString;
            SqlConnection con;
            string SafaeServer = "DESKTOP-SL2AUNJ";
            connString = "Data Source =" + SafaeServer + "; Initial Catalog = Gestion_Etudiant; Integrated Security = true;";
            con = new SqlConnection();
            con.ConnectionString = connString;
            con.Open();
            SqlCommand commande = new SqlCommand("Select * From Filiere", con);
            SqlDataReader reader = commande.ExecuteReader();
            Filiere filiere;
            while (reader.Read())
            {
                filiere = new Filiere();
                filiere.Id = Convert.ToInt32(reader[0].ToString());
                filiere.FiliereName = reader.GetString(1);
                filiere.Responsable = reader.GetString(2);
                filieres.Add(filiere);

            }
            reader.Close();
            return filieres;

        }

    }
}
