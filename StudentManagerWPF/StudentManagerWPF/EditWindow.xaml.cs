using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        List<StudentCard> Cards;
        string connString;
        SqlConnection con;
        public int filiere;
        int valueButton = 1;
        public static EditWindow current;
        public EditWindow(int f)
        {
            Cards = new List<StudentCard>();
            current = this;
            InitializeComponent();
            this.filiere = f;
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            MenuWindow.currentWindow.ComboBox1.SelectedIndex = filiere - 1;
            MenuWindow.currentWindow.Show();
            if (InsertDataWindow.countWindow > 0)
            {
                InsertDataWindow.currentInsertWindow.Close();
                InsertDataWindow.countWindow--;
            }
            this.Close();
            

        }
        private void Close(object sender, System.Windows.RoutedEventArgs e)
        {
            MenuWindow.currentWindow.Close();
            if (InsertDataWindow.countWindow > 0)
            {
                InsertDataWindow.currentInsertWindow.Close();
                InsertDataWindow.countWindow--;
            }
            Close();
            
        }
        
        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button1ere_Click(object sender, RoutedEventArgs e)
        {
           valueButton = 1;
            Button1ere.Background = new SolidColorBrush(Color.FromRgb(32, 0, 255));
            Button2eme.Background = new SolidColorBrush(Color.FromRgb(114, 167, 218));
            Button3eme.Background = new SolidColorBrush(Color.FromRgb(114, 167, 218));
            showCards();
        }

        private void Button2eme_Click(object sender, RoutedEventArgs e)
        {
            valueButton = 2;
            Button2eme.Background = new SolidColorBrush(Color.FromRgb(32, 0, 255));
            Button1ere.Background = new SolidColorBrush(Color.FromRgb(114, 167, 218));
            Button3eme.Background = new SolidColorBrush(Color.FromRgb(114, 167, 218));
            showCards();
        }

        private void Button3eme_Click(object sender, RoutedEventArgs e)
        {
            valueButton = 3;
            Button3eme.Background = new SolidColorBrush(Color.FromRgb(32, 0, 255));
            Button2eme.Background = new SolidColorBrush(Color.FromRgb(114, 167, 218));
            Button1ere.Background = new SolidColorBrush(Color.FromRgb(114, 167, 218));
            showCards();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            showCards();
        }
        public void showCards()
        {
            cards.Children.Clear();
            String filiereString = filiere.ToString();
            String valueButtonString = filiere.ToString();
            string SaadServer = "DESKTOP-SL2AUNJ";
            connString = "Data Source =" + SaadServer + "; Initial Catalog = Gestion_Etudiant; Integrated Security = true;";
            con = new SqlConnection();
            con.ConnectionString = connString;
            con.Open();
            SqlCommand commande = new SqlCommand("Select photo,cne,nom,prenom From Etudiant where id_fil ="+ filiereString+" and annee_cycle ="+valueButton , con);
            
            SqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Cards.Add(new StudentCard(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                cards.Children.Add(this.Cards.Last());
            }
            reader.Close();
            
        }

        private void addStudent_Click(object sender, RoutedEventArgs e)
        {
            InsertDataWindow ins = new InsertDataWindow(true, "",filiere);
            ins.Show();
        }
        public void clearCards()
        {
            foreach(StudentCard elem in Cards)
            {
                elem.disposeImage();
                cards.Children.Clear();
            }
        }
    }
}
