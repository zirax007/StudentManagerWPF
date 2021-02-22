using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Telerik.Windows.Controls.ChartView;

namespace StudentManagerWPF
{
    /// <summary>
    /// Logique d'interaction pour MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public int filiere;
        public String FiliereName ="";
        string connString;
        public static MenuWindow currentWindow;
        public DataTable dt;
        SqlConnection con;
        Image img;
        public MenuWindow()
        {
            InitializeComponent();
            currentWindow = this;
           
            

            radGridView.Visibility = Visibility.Hidden;
            infoFiliere.Visibility = Visibility.Hidden;
            ButtonEdit.Visibility = Visibility.Hidden;
            string SaadServer = "DESKTOP-SL2AUNJ";
            connString = "Data Source =" + SaadServer + "; Initial Catalog = Gestion_Etudiant; Integrated Security = true;";
            con = new SqlConnection();
            con.ConnectionString = connString;
            con.Open();
            SqlCommand commande = new SqlCommand("Select * From Filiere", con);
            SqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                ComboBox1.Items.Add(reader.GetString(1));
            }
            reader.Close();

            
            fillingChart();
            fillingChart2();
            





        }
        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            if (InsertDataWindow.countWindow > 0)
            {
                InsertDataWindow.currentInsertWindow.Close();
                InsertDataWindow.countWindow--;
            }
            this.Close();

        }
        private void Close(object sender, System.Windows.RoutedEventArgs e)
        {
            if (InsertDataWindow.countWindow > 0)
            {
                InsertDataWindow.currentInsertWindow.Close();
                InsertDataWindow.countWindow--;
            }
            Close();
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //ButtonEdit.Background = new SolidColorBrush(Color.FromRgb(32, 0, 255));
            EditWindow edit = new EditWindow(filiere);
            clearView();
            edit.Show();
            this.Hide();
            
           
        }
        private void Choix_menu(object sender, System.Windows.RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            //GridCursor.Margin = new Thickness(10 + (326 * index), 0, 0, -40);
            switch (index)
            {
                case 0: // Gestion des etudiants
                    break;
                case 1: // gestion des filieres
                    break;
                case 2: // statistique
                    break;

            }
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!radGridView.IsVisible)
            {
                radGridView.Visibility = Visibility.Visible;
                ButtonEdit.Visibility = Visibility.Visible;
            }
            radGridView.ItemsSource = null;
            FiliereName = ComboBox1.SelectedItem.ToString();
            filiere = Convert.ToInt32(ComboBox1.SelectedIndex) + 1;
            SqlCommand commande1 = new SqlCommand("Select Id_filiere from Filiere where Nom_filiere = '" + FiliereName + "'",con);
            SqlDataReader reader2 = commande1.ExecuteReader();
            if(reader2.Read())
            {
                filiere = reader2.GetInt32(0);
            }
            reader2.Close();
            SqlParameter para = new SqlParameter("@filiere", filiere);
            SqlParameter para2 = new SqlParameter("@filiere", filiere);
            SqlCommand commande = new SqlCommand("Select * From Etudiant where id_fil = @filiere", con);
            commande.Parameters.Add(para);
            SqlDataReader reader = commande.ExecuteReader();
            dt  = new DataTable();
            dt.Columns.Add("cne", typeof(string));
            dt.Columns.Add("image", typeof(ImageSource));
            dt.Columns.Add("nom", typeof(string));
            dt.Columns.Add("prenom", typeof(string));
            dt.Columns.Add("sexe", typeof(string));
            dt.Columns.Add("date", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("idFiliere", typeof(int));
            dt.Columns.Add("annee", typeof(int));
            //List < Image > = new List() < Image >;
            while (reader.Read())
            {
                img = new Image();
                img.Source = new ImageSourceConverter().ConvertFromString(reader.GetString(5)) as ImageSource;
                dt.Rows.Add(reader.GetString(0), img.Source, reader.GetString(1), reader.GetString(2), reader[3].ToString(), reader.GetDateTime(4).Date.ToString("d"), reader.GetString(6), Convert.ToInt32(reader[7].ToString()), Convert.ToInt32(reader[8].ToString()));
                radGridView.ItemsSource = dt.DefaultView;

            }
            reader.Close();
            SqlCommand commande2 = new SqlCommand("Select * From Filiere where Id_filiere = @filiere", con);
            if (!infoFiliere.IsVisible)
                infoFiliere.Visibility = Visibility.Visible;
            commande2.Parameters.Add(para2);
            reader = commande2.ExecuteReader();
            if (reader.Read())
            {
                nomFiliere.Text = reader.GetString(1);
                respo.Text = reader.GetString(2);
                reader.Close();
            }
        }
        
        public void clearView()
        {
            if (filiere == 1) 
            {
                //filiere++;
                ComboBox1.SelectedIndex = filiere;
                
            }
            else
            {
                //filiere = filiere - 1;
                ComboBox1.SelectedIndex = filiere - 2;
            }
            /*SqlParameter para = new SqlParameter("@filiere", filiere);
            SqlCommand commande = new SqlCommand("Select * From Etudiant where id_fil = @filiere", con);
            commande.Parameters.Add(para);
            SqlDataReader reader = commande.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("cne", typeof(string));
            dt.Columns.Add("image", typeof(ImageSource));
            dt.Columns.Add("nom", typeof(string));
            dt.Columns.Add("prenom", typeof(string));
            dt.Columns.Add("sexe", typeof(string));
            dt.Columns.Add("date", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("idFiliere", typeof(int));
            dt.Columns.Add("annee", typeof(int));
            //List < Image > = new List() < Image >;
            while (reader.Read())
            {
                img = new Image();
                img.Source = new ImageSourceConverter().ConvertFromString(reader.GetString(5)) as ImageSource;
                dt.Rows.Add(reader.GetString(0), img.Source, reader.GetString(1), reader.GetString(2), reader[3].ToString(), reader.GetDateTime(4).Date.ToString("d"), reader.GetString(6), Convert.ToInt32(reader[7].ToString()), Convert.ToInt32(reader[8].ToString()));
                radGridView.ItemsSource = dt.DefaultView;

            }
            reader.Close();
            for(int i = 0; i < 100; i++)
            {
                i = i+1;
            }*/


        }

        private void fillingChart()
        {
            
            bar11.DataContext = plotInfoSource.plotInfos(1);
            rectangleChart1.Palette = Chart3DPalettes.Material;
            SqlCommand commande = new SqlCommand("Select Id_filiere From Filiere", con);
            SqlDataReader reader = commande.ExecuteReader();

            int nbFiliere = 1;

            while (reader.Read()) { 
                nbFiliere = reader.GetInt32(0);
                BarSeries3D geneBar1 = new BarSeries3D();
                geneBar1.XValueBinding = bar11.XValueBinding;
                geneBar1.YValueBinding = bar11.YValueBinding;
                geneBar1.ZValueBinding = bar11.ZValueBinding;
                geneBar1.ItemsSource = plotInfoSource.plotInfos(nbFiliere);
                rectangleChart1.Series.Add(geneBar1);
            }
        reader.Close();

        }

        private void fillingChart2()
        {
            barF.ItemsSource = plotInfoSource.plotInfosFiliere();
            rectangleChart2.Palette = Chart3DPalettes.Material;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            secondData.Visibility = Visibility.Collapsed;
            firstData.Visibility = Visibility.Visible;
            parAneeButoon.Background = Brushes.Blue;
            parFiliereButoon.Background = Brushes.CadetBlue;
        }

        private void parFiliereButoon_Click(object sender, RoutedEventArgs e)
        {
           firstData.Visibility = Visibility.Collapsed;
            secondData.Visibility = Visibility.Visible;
            parAneeButoon.Background = Brushes.CadetBlue;
            parFiliereButoon.Background = Brushes.Blue;
        }

        private void MyCarousel_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.MyCarousel.FindCarouselPanel() != null)
                this.MyCarousel.FindCarouselPanel().MoveBy(4);
              
            
        }
        private void Search_Employees(object sender, RoutedEventArgs e)
        {
            if (!this.MyCarousel.IsLoaded)
            {
                return;
            }

            var searchQuery = this.TextBoxSearchName.Text;

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return;
            }

            var items = (IEnumerable<Filiere>)this.MyCarousel.ItemsSource;
            Filiere selectedFiliere = null;

            if (items != null)
            {
                selectedFiliere = items.FirstOrDefault(x => x.FiliereName.ToLower().Contains(searchQuery.ToLower()));

                this.MyCarousel.BringDataItemIntoView(selectedFiliere);
                this.MyCarousel.SelectedItem = selectedFiliere;
            }
        }
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MyCarousel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //int index = MyCarousel.Items.IndexOf(MyCarousel.SelectedItem);
            //mytext.Text = "Heeeeeey";
            FiliereEdit win = new FiliereEdit();

            this.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            // this.Background = Brushes.Black;
            //this.Opacity = 0.8;

            win.Show();
            var currentFiliere = this.MyCarousel.CurrentItem as Filiere;
            win.TextName.Text = currentFiliere.FiliereName;
            win.TextRespo.Text = currentFiliere.Responsable;
            win.TextId.Content = currentFiliere.Id;
            win.ajouter.Visibility = Visibility.Hidden;





            //this.AllowsTransparency = true;    
            // WindowStyle = WindowStyle.None,  
            // WindowState = WindowState.Maximized;

        }
        private void left(object sender, RoutedEventArgs e)
        {
            this.MyCarousel.FindCarouselPanel().MoveBy(-1);
        }
        private void right(object sender, RoutedEventArgs e)
        {
            this.MyCarousel.FindCarouselPanel().MoveBy(1);
        }
        private void Fleft(object sender, RoutedEventArgs e)
        {
            this.MyCarousel.FindCarouselPanel().MoveBy(-2);
        }
        private void Fright(object sender, RoutedEventArgs e)
        {
            this.MyCarousel.FindCarouselPanel().MoveBy(2);
        }


        private void Ajouter(object sender, RoutedEventArgs e)
        {
            FiliereEdit win = new FiliereEdit();
            win.modifier.Visibility = Visibility.Hidden;
            win.supprimer.Visibility = Visibility.Hidden;
            win.ajouter.Visibility = Visibility.Visible;
            win.FilId.Visibility = Visibility.Hidden;
            win.Show();


        }
    }
}
