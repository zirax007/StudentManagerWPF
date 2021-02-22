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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentManagerWPF
{
    class StudentCard : StackPanel
    {
        WrapPanel wPanel;
        Image img;
        Button b;
        TextBlock cne = new TextBlock();
        TextBlock nom = new TextBlock();
        TextBlock prenom = new TextBlock();

        public StudentCard(String imgSource, String cne, String nom, String prenom)
        {
            this.MouseEnter += new System.Windows.Input.MouseEventHandler(this.pMouseEnter);
            this.MouseLeave += new System.Windows.Input.MouseEventHandler(this.pMouseLeave);
            this.b = new Button();
            b.Click += new RoutedEventHandler(Onb2Click);
            this.Height = 180;
            this.Width = 135;
            this.img = new Image();
            this.cne.Text = cne;
            this.nom.Text = nom;
            this.prenom.Text = prenom;

            //image
            img.Source = new ImageSourceConverter().ConvertFromString(imgSource) as ImageSource;

            //cne
            //this.cne.Width = 80;
            this.cne.FontSize = 11;
            TextBlock lcne = new TextBlock();
            lcne.Text = "cne : ";
            lcne.FontSize = 11;



            //nom
            //this.nom.Width = 80;
            this.nom.FontSize = 11;
            TextBlock lnom = new TextBlock();
            lnom.Text = "nom : ";
            lnom.FontSize = 11;




            //prenom
            //this.prenom.Width = 80;
            this.prenom.FontSize = 11;
            TextBlock lprenom = new TextBlock();
            lprenom.Text = "prenom :";
            lprenom.FontSize = 11;



            //WrapPanel for image and StackedPanel of informations
            wPanel = new WrapPanel();
            wPanel.Children.Add(b);
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = this.img.Source;
            b.Background = imgBrush;
            b.Height = 90;
            b.Width = 130;


            //definitions
            StackPanel definitions = new StackPanel();
            definitions.Children.Add(lcne);
            definitions.Children.Add(lprenom);
            definitions.Children.Add(lnom);

            //Values
            StackPanel values = new StackPanel();
            values.Children.Add(this.cne);
            values.Children.Add(this.prenom);
            values.Children.Add(this.nom);

            //adding informations and values to the wrapPanel2
            Border border = new Border();
            border.Background = Brushes.Aquamarine;
            border.BorderThickness = new Thickness(3);
            border.BorderBrush = Brushes.LightGray;
            border.Padding = new Thickness(5);
            border.Height = 85;
            border.Width = 135;
            Thickness borderMargin = border.Margin;
            borderMargin.Left = -5;
            border.Margin = borderMargin;

            WrapPanel wPanel2 = new WrapPanel();
            Thickness wPanel2margin = this.Margin;
            wPanel2margin.Left = 5;
            this.Margin = wPanel2margin;
            border.Child = wPanel2;
            wPanel2.Children.Add(definitions);
            wPanel2.Children.Add(values);
            Thickness valuesMargin = values.Margin;
            valuesMargin.Left = 5;
            values.Margin = valuesMargin;

            //adding image
            //wPanel.Children.Add(img);




            this.Children.Add(wPanel);
            this.Children.Add(border);
            Thickness currentMargin = this.Margin;
            currentMargin.Left = 20;
            this.Margin = currentMargin;


        }

        private void pMouseEnter(object sender, System.EventArgs e)
        {
            ImageBrush editIcon = new ImageBrush();
            editIcon.ImageSource = new ImageSourceConverter().ConvertFromString("../../Assets/edit_icon.png") as ImageSource;
            editIcon.Opacity = 0.5;
            b.Background = editIcon;
            Mouse.OverrideCursor = Cursors.Hand;

        }

        private void pMouseLeave(object sender, System.EventArgs e)
        {
            ImageBrush imgBrush = new ImageBrush();
            if(img != null)
            {
                imgBrush.ImageSource = this.img.Source;
                b.Background = imgBrush;
            }
        
            
            Mouse.OverrideCursor = Cursors.Arrow;

        }

        void Onb2Click(object sender, RoutedEventArgs e)
        {
            InsertDataWindow insertdata = new InsertDataWindow(false, cne.Text,EditWindow.current.filiere) ;
            insertdata.Show();
            img.Source = null;
            img = null;
            GC.Collect();
        }
        public void disposeImage()
        {
            
            
            
        }



    }
}