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
using System.IO;
namespace StudentManagerWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            


        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text == "admin" && mdp.Password == "123456" )
            {
                MenuWindow menu = new MenuWindow();
                menu.Show();
                this.Close();
            }
            else
            {
                error.Content = "*Username or Password INCORRECT ! ";
            }
        }
        private void MinimizeClick(object sender, System.Windows.RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) {
                if (username.Text == "admin" && mdp.Password == "123456")
                {
                    MenuWindow menu = new MenuWindow();
                    menu.Show();
                    this.Close();
                }
                else
                {
                    error.Content = "*Username or Password INCORRECT ! ";
                }
            }

            
        }

        
    }
}
