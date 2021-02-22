using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerWPF
{
    public class plotInfoSource
    {
        
        public static ObservableCollection<PlotInfo> plotInfos(int nb)
        {
            ObservableCollection<PlotInfo> data = new ObservableCollection<PlotInfo>();
            string SaadServer = "DESKTOP-SL2AUNJ";
            string connString = "Data Source =" + SaadServer + "; Initial Catalog = Gestion_Etudiant; Integrated Security = true;";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connString;
            con.Open();
            SqlCommand commande = new SqlCommand("SELECT Filiere.Nom_filiere,Etudiant.annee_cycle , Count(Filiere.Nom_filiere) as number from Etudiant INNER JOIN FIliere ON Filiere.Id_filiere = Etudiant.id_fil where id_fil = " + nb + "Group by annee_cycle,Nom_filiere", con);
            SqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                data.Add(new PlotInfo(reader.GetString(0), reader[1].ToString(), reader.GetInt32(2)));
            }
            reader.Close();
            con.Close();
            return data;
            
        }

        public static ObservableCollection<PlotInfo> plotInfosFiliere()
        {
            ObservableCollection<PlotInfo> data = new ObservableCollection<PlotInfo>();
            string SaadServer = "DESKTOP-SL2AUNJ";
            string connString = "Data Source =" + SaadServer + "; Initial Catalog = Gestion_Etudiant; Integrated Security = true;";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connString;
            con.Open();
            SqlCommand commande = new SqlCommand("SELECT Filiere.Nom_filiere, Count(Filiere.Nom_filiere) as number from Etudiant INNER JOIN FIliere ON Filiere.Id_filiere = Etudiant.id_fil  Group by Nom_filiere", con);
            SqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                data.Add(new PlotInfo("Filiere", reader.GetString(0), reader.GetInt32(1)));
            }
            reader.Close();
            con.Close();


            return data;

        }

    }
}
