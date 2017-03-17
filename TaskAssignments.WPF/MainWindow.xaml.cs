using System;
using System.Collections;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskAssignments.Core.Entities;

namespace TaskAssignments.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("A task must have a title at least.");
                return;
            }
            try
            {
                Core.Entities.Task ut = new Core.Entities.Task()
                {
                    Title = txtTitle.Text,
                    Description = txtDesc.Text,
                    DueDate = (DateTime)dtDueTime.SelectedDate,
                    Status = txtStatus.Text,
                    Users = new List<ApplicationUser>()
                };
                var selectedIds = listBoxUsers.SelectedItems.Cast<ApplicationUser>().Select(u => u.Id);
                SelectedUsers(selectedIds, ut);
                ctx.Tasks.Add(ut);
                ctx.SaveChanges();
                MessageBox.Show("The task has been assigned successfully.");
                ClearText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearText()
        {
            foreach (Control item in contentsGrid.Children)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Clear();
                }
            }
            listBoxUsers.UnselectAll();
        }

        public void SelectedUsers(IEnumerable selectedItems, Core.Entities.Task userTask)
        {
            foreach (var item in selectedItems)
            {
                var user = ctx.Users.Find(item.ToString());
                userTask.Users.Add(user);
            }
        }
        ApplicationDbContext ctx = new ApplicationDbContext();

        public static bool Auth { get; set; }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.ShowDialog();
            if (Auth)
            {
                contentsGrid.IsEnabled = true;
                logoutMenu.IsEnabled = true;
                loginMenu.IsEnabled = false;
                listBoxUsers.ItemsSource = ctx.Users.ToList();
                listBoxUsers.DisplayMemberPath = "UserName";
                listBoxUsers.SelectedValuePath = "Id";
                listBoxUsers.UnselectAll();
            }
        }
        private void logout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Sign Out?", "", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                ClearText();
                listBoxUsers.ItemsSource = null;
                listBoxUsers.Items.Clear();
                contentsGrid.IsEnabled = false;
                loginMenu.IsEnabled = true;
                logoutMenu.IsEnabled = false;
                Auth = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logoutMenu.IsEnabled = false;
        }

    }
}

