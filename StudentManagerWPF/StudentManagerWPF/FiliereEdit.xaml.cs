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
    /// Logique d'interaction pour FiliereEdit.xaml
    /// </summary>
    public partial class FiliereEdit : Window
    {
        public FiliereEdit()
        {
            InitializeComponent();
        }
        private void Close(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
        private void Modifier(object sender, RoutedEventArgs e)
        {
            string connString;
            SqlConnection con;
            string SafaeServer = "DESKTOP-SL2AUNJ";
            connString = "Data Source =" + SafaeServer + "; Initial Catalog = Gestion_Etudiant ; Integrated Security = true;";
            con = new SqlConnection();
            con.ConnectionString = connString;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update Filiere set Nom_filiere = '" + TextName.Text + "' , Responsable ='" + TextRespo.Text + "'where Id_filiere ='" + TextId.Content + "'";

            cmd.ExecuteNonQuery();

            MenuWindow.currentWindow.MyCarousel.ItemsSource = null;
            MenuWindow.currentWindow.MyCarousel.ItemsSource = FiliereService.GetEmployees();
            MenuWindow.currentWindow.MyCarousel.FindCarouselPanel().MoveBy(2);

            con.Close();
            this.Close();
        }

        private void Supprimer(object sender, RoutedEventArgs e)
        {
            string connString;
            SqlConnection con;
            string SafaeServer = "DESKTOP-SL2AUNJ";
            connString = "Data Source =" + SafaeServer + "; Initial Catalog = Gestion_Etudiant ; Integrated Security = true;";
            con = new SqlConnection();
            con.ConnectionString = connString;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Filiere where Id_filiere ='" + TextId.Content + "'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete from Etudiant where id_fil = '" + TextId.Content + "'";
            cmd.ExecuteNonQuery();
            MenuWindow.currentWindow.MyCarousel.ItemsSource = null;
            MenuWindow.currentWindow.MyCarousel.ItemsSource = FiliereService.GetEmployees();
            MenuWindow.currentWindow.MyCarousel.FindCarouselPanel().MoveBy(-2);
            con.Close();
            this.Close();
        }
        private void Ajouter(object sender, RoutedEventArgs e)
        {
            string connString;
            SqlConnection con;
            string SafaeServer = "DESKTOP-SL2AUNJ";
            connString = "Data Source =" + SafaeServer + "; Initial Catalog = Gestion_Etudiant ; Integrated Security = true;";
            con = new SqlConnection();
            con.ConnectionString = connString;
            con.Open();
            SqlTransaction tr = con.BeginTransaction();

            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = tr;

            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.CommandText = " INSERT INTO Filiere(Nom_filiere,Responsable) VALUES('" + TextName.Text + "','" + TextRespo.Text + "')";

                cmd.ExecuteNonQuery();
                tr.Commit();
                MenuWindow.currentWindow.MyCarousel.ItemsSource = null;
                MenuWindow.currentWindow.MyCarousel.ItemsSource = FiliereService.GetEmployees();
               MenuWindow.currentWindow.MyCarousel.FindCarouselPanel().MoveBy(2);

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tr.Rollback();

            }
            finally
            {
                con.Close();
                this.Close();


            }



        }

    }
}
