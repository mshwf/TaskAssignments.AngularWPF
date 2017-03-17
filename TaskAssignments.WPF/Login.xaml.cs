using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TaskAssignments.Core.Entities;

namespace TaskAssignments.WPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public BackgroundWorker bw = new BackgroundWorker();

        public Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (await VerifyUserNamePassword(txtBxuserName.Text, passBxPassword.Password))
            {
                MainWindow.Auth = true;
                Close();
            }
            else
                MessageBox.Show("Wrong username or password.");
        }
        public async Task<bool> VerifyUserNamePassword(string userName, string password)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            try
            {
                var user = await usermanager.FindAsync(userName, password);
                if (user != null)
                {
                    if (usermanager.IsInRole(user.Id, "Admin"))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return false;
        }
        private void txtBxuserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBxuserName.BorderBrush = Brushes.White;
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void passBxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passBxPassword.BorderBrush = Brushes.White;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
