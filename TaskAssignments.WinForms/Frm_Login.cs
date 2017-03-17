using System.Windows.Forms;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TaskAssignments.Core.Entities;

namespace UsersAndRolesWF
{
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, System.EventArgs e)
        {
            if (await VerifyUserNamePassword(txtUsername.Text, txtPassword.Text))
            {
                Frm_Main.Auth = true;
                Close();
            }
            else
                MessageBox.Show("Wrong username or password.");
        }
        public async Task<bool> VerifyUserNamePassword(string userName, string password)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = await usermanager.FindAsync(userName, password);
            if (user != null)
            {
                if (usermanager.IsInRole(user.Id, "Admin"))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
