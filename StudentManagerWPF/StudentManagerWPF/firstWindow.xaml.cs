using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentManagerWPF
{
    /// <summary>
    /// Logique d'interaction pour firstWindow.xaml
    /// </summary>
    public partial class firstWindow : Window
    {
        string connString;
        SqlConnection con;
        public firstWindow()
        {
            InitializeComponent();
            string SaadServer = "DESKTOP-SL2AUNJ";
            connString = "Data Source =" + SaadServer + "; Initial Catalog = Gestion_Etudiant; Integrated Security = true;";
            con = new SqlConnection();
            con.ConnectionString = connString;
            con.Open();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand commande = new SqlCommand("Select * From Filiere", con);
            SqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                //id_filiere.Text = reader[0].ToString();
                //nom_filiere.Text = reader.GetString(1);
                //.Text = reader.GetString(2);

            }
        }
    }
}
