using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace StudentManagerWPF
{
    /// <summary>
    /// Logique d'interaction pour InsertDataWindow.xaml
    /// </summary>
    /// 
    public partial class InsertDataWindow : Window
    {
        OpenFileDialog op;
        List<string> existingCNE;
        string connString;
        SqlConnection con;
        string path = "../../Assets/";
        Boolean ajoute;
        Boolean photoInserted = false;
        string photoExtension  ="";
        string photoPath = "";
        public static int countWindow = 0;
        public string cneCard;
        public string defaultpath = "../../Assets/pic.png";
        public static InsertDataWindow currentInsertWindow;
        public int selectedFiliere;

        public InsertDataWindow(Boolean ajoute, string cneCard,int selecedFiliere)
        {
            InitializeComponent();
            this.selectedFiliere = selecedFiliere;
            this.cneCard = cneCard;
            op = new OpenFileDialog();
            
            if (countWindow > 0)
            {
                currentInsertWindow.Close();
                currentInsertWindow = this;
            }
            countWindow++; // fentres active actuelement

            //connexion au server
            string SaadServer = "DESKTOP-SL2AUNJ";
            connString = "Data Source =" + SaadServer + "; Initial Catalog = Gestion_Etudiant; Integrated Security = true;";
            con = new SqlConnection();
            con.ConnectionString = connString;
            con.Open();

            this.ajoute = ajoute;
            currentInsertWindow = this;
            existingCNE = new List<string>();
            
            SqlCommand comm = new SqlCommand("Select cne From Etudiant", con);
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                existingCNE.Add(read.GetString(0));
            }
            read.Close();
            
                //test si la fenetre est pour l'ajout ou bien pour la modification
            if (ajoute)
            {
                modifier.Visibility = Visibility.Collapsed;
                title.Text = "Ajouter un Etudiant";
                comm = new SqlCommand("Select * From Etudiant where cne = '" + cneCard + "'", con);
                read = comm.ExecuteReader();
                while (read.Read())
                {
                    
                    photoPath = read.GetString(5);
                    
                }
                read.Close();

            }
            else
            {
                ajouter.Visibility = Visibility.Hidden;
                title.Text = "Modifier cet Etudiant";
                //felling the informations of the clicked student to modify
                comm = new SqlCommand("Select * From Etudiant where cne = '" + cneCard+ "'", con);
                read = comm.ExecuteReader();
                while (read.Read())
                {
                    cneField.Text = read.GetString(0);
                    nomField.Text = read.GetString(1);
                    prenomField.Text = read.GetString(2);
                    if (read[3].ToString() == "M")
                        sexeComboBox.SelectedIndex = 0;
                    else
                        sexeComboBox.SelectedIndex = 1;
                    dateField.SelectedDate = read.GetDateTime(4);
                    photoPath = read.GetString(5);
                    photoExtension = photoPath.Substring(photoPath.LastIndexOf('.'), photoPath.Length - photoPath.LastIndexOf('.'));
                    emailField.Text = read.GetString(6);
                   
                    anneeComboBox.SelectedIndex = read.GetInt32(8) - 1;
                }
                read.Close();
            }
            
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            countWindow--;
            EditWindow.current.showCards();
            this.Close();
            
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (photoInserted)
            {
                Boolean condition = photoExtension != "" && cneField.Text != "" && prenomField.Text != "" && nomField.Text != "" && sexeComboBox.SelectedIndex != -1 && dateField.SelectedDate.HasValue && emailField.Text != "" &&  anneeComboBox.SelectedIndex != -1;
                if (condition)
                {
                    
                        SqlTransaction tr = con.BeginTransaction();
                        SqlCommand commande = con.CreateCommand();
                        commande.Transaction = tr;
                        try
                        {
                            char sexe = ' ';
                            if (sexeComboBox.SelectedIndex == 0)
                            {
                                sexe = 'M';
                            }
                            else if (sexeComboBox.SelectedIndex == 1)
                            {
                                sexe = 'F';
                            }
                            string values;
                            values = cneField.Text + "','" + nomField.Text + "','" + prenomField.Text + "','" + sexe + "','" + dateField.SelectedDate + "','" + path + cneField.Text + photoExtension + "','" + emailField.Text + "','" + selectedFiliere + "','" + (anneeComboBox.SelectedIndex + 1);
                        //values += 
                            commande.CommandText = "delete From Etudiant where cne = '" + cneCard + "'";
                            commande.ExecuteNonQuery();
                            string requete = "INSERT INTO Etudiant(cne, nom, prenom, sexe, date_naiss, photo, email, id_fil, annee_cycle) Values( '" + values + "')";
                            commande.CommandText = requete;
                            commande.ExecuteNonQuery();
                            MessageBox.Show("l'etudiant(e) " + nomField.Text + " " + prenomField.Text + " a été bien modifié(e) !");
                            
                            tr.Commit();
                            try
                            {
                                File.Delete(photoPath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + " error modifying pic");
                            }
                            FileStream f = new FileStream(path + cneField.Text + photoExtension, FileMode.Create);
                            var encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(new BitmapImage(new Uri(op.FileName))));
                            encoder.Save(f);
                            f.Close();
                        EditWindow.current.showCards();
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            MessageBox.Show(ex.Message);
                        }
                    
                }
                else
                {
                    MessageBox.Show("renseignez tous les champs");
                }
            }
            else
            {
                Boolean condition = cneField.Text != "" && prenomField.Text != "" && nomField.Text != "" && sexeComboBox.SelectedIndex != -1 && dateField.SelectedDate.HasValue && emailField.Text != ""  && anneeComboBox.SelectedIndex != -1;
                if (condition)
                {
                    if (cneField.Text == cneCard)
                    {
                        char sexe = ' ';
                        SqlTransaction tr = con.BeginTransaction();
                        SqlCommand commande = con.CreateCommand();
                        commande.Transaction = tr;
                        try
                        {

                            if (sexeComboBox.SelectedIndex == 0)
                            {
                                sexe = 'M';
                            }
                            else if (sexeComboBox.SelectedIndex == 1)
                            {
                                sexe = 'F';
                            }
                            EditWindow.current.clearCards();
                            commande.CommandText = "delete From Etudiant where cne = '" + cneCard + "'";
                            commande.ExecuteNonQuery();
                            string values = cneField.Text + "','" + nomField.Text + "','" + prenomField.Text + "','" + sexe + "','" + dateField.SelectedDate + "','" + path + cneField.Text + photoExtension + "','" + emailField.Text + "','" +selectedFiliere + "','" + (anneeComboBox.SelectedIndex + 1);
                            //values += 
                            string requete = "INSERT INTO Etudiant(cne, nom, prenom, sexe, date_naiss, photo, email, id_fil, annee_cycle) Values( '" + values + "')";
                            commande.CommandText = requete;
                            commande.ExecuteNonQuery();
                            
                            MessageBox.Show("l'etudiant(e) " + nomField.Text + " " + prenomField.Text + " a été bien modifié(e) !");
                            tr.Commit();
                            EditWindow.current.showCards();
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            tr.Rollback();
                        }
                    }
                    else
                    {
                        SqlTransaction tr = con.BeginTransaction();
                        SqlCommand commande = con.CreateCommand();
                        commande.Transaction = tr;
                        try
                        {
                            char sexe = ' ';
                            if (sexeComboBox.SelectedIndex == 0)
                            {
                                sexe = 'M';
                            }
                            else if (sexeComboBox.SelectedIndex == 1)
                            {
                                sexe = 'F';
                            }
                            
                            string values;
                            values = cneField.Text + "','" + nomField.Text + "','" + prenomField.Text + "','" + sexe + "','" + dateField.SelectedDate + "','" + path + cneField.Text + photoExtension + "','" + emailField.Text + "','" + selectedFiliere + "','" + (anneeComboBox.SelectedIndex + 1);
                            //values += 
                            string requete = "INSERT INTO Etudiant(cne, nom, prenom, sexe, date_naiss, photo, email, id_fil, annee_cycle) Values( '" + values + "')";
                            commande.CommandText = requete;
                            commande.ExecuteNonQuery();
                            MessageBox.Show("l'etudiant(e) " + nomField.Text + " " + prenomField.Text + " a été bien modifié(e) !");
                            try
                            {//hna akhir khaja drti briti tkhdm b intermediaire

                                System.IO.File.Move(photoPath, path + cneField.Text + photoExtension);
                                commande.CommandText = "delete From Etudiant where cne = '" + cneCard + "'";
                                commande.ExecuteNonQuery();
                               
                                File.Delete(photoPath);
                                

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + " error modifying pic");
                            }
                            
                            tr.Commit();
                            EditWindow.current.showCards();
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Boolean condition = photoExtension != "" && cneField.Text != "" && prenomField.Text != "" && nomField.Text != "" && sexeComboBox.SelectedIndex != -1 && dateField.SelectedDate.HasValue && emailField.Text != ""  && anneeComboBox.SelectedIndex != -1;
            if (condition)
            {
                if (isCneTaken(cneField.Text))
                {
                    MessageBox.Show(cneField.Text + " cne is already taken !");
                }
                else
                {
                    SqlTransaction tr = con.BeginTransaction();
                    SqlCommand commande = con.CreateCommand();
                    commande.Transaction = tr;
                    try
                    {
                        char sexe = ' ';
                        if (sexeComboBox.SelectedIndex == 0)
                        {
                            sexe = 'M';
                        }
                        else if (sexeComboBox.SelectedIndex == 1)
                        {
                            sexe = 'F';
                        }
                        string values;
                        values = cneField.Text + "','" + nomField.Text + "','" + prenomField.Text + "','" + sexe + "','" + dateField.SelectedDate + "','" + path + cneField.Text + photoExtension + "','" + emailField.Text + "','" + selectedFiliere + "','" + (anneeComboBox.SelectedIndex + 1);
                        //values += 
                        string requete = "INSERT INTO Etudiant(cne, nom, prenom, sexe, date_naiss, photo, email, id_fil, annee_cycle) Values( '" + values + "')";
                        commande.CommandText = requete;
                        commande.ExecuteNonQuery();
                        tr.Commit();
                        FileStream f = new FileStream(path + cneField.Text + photoExtension, FileMode.Create);
                        var encoder = new PngBitmapEncoder();
                        
                        encoder.Frames.Add(BitmapFrame.Create(new BitmapImage(new Uri(op.FileName))));
                        encoder.Save(f);
                        f.Close();

                        MessageBox.Show("l'etudiant(e) " + nomField.Text + " " + prenomField.Text + " a été bien ajouté(e) !");
                        EditWindow.current.showCards();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("renseignez tous les champs");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if(op.ShowDialog() == true)
            {
                photoInserted = true;
                int fileNameLength = op.FileName.Length;
                photoExtension = op.FileName.Substring(op.FileName.LastIndexOf('.'), fileNameLength - op.FileName.LastIndexOf('.'));
                photoInserted = true;
                

            }
        }
        private Boolean isCneTaken(string word)
        {
            foreach(string element in existingCNE)
            {
                if (word == element)
                    return true;
            }
            return false;
        }
    }
}
