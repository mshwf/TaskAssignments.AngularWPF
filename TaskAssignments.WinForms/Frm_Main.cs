using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using System.Collections;
using TaskAssignments.Core.Entities;

namespace UsersAndRolesWF
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }
        ApplicationDbContext ctx = new ApplicationDbContext();
        public static bool Auth { get; set; }
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Frm_Login().ShowDialog();
            if (Auth)
            {
                groupBox1.Enabled = true;
                logOffToolStripMenuItem.Enabled = true;
                loginToolStripMenuItem.Enabled = false;
                listBoxUsers.DataSource = ctx.Users.ToList();
                listBoxUsers.DisplayMember = "UserName";
                listBoxUsers.ValueMember = "Id";
                listBoxUsers.ClearSelected();
            }
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text))
            {
                MessageBox.Show("A task must have a title at least.");
                return;
            }
            try
            {
                Task ut = new Task()
                {
                    Title = txtTaskTitle.Text,
                    Description = txtDesc.Text,
                    DueDate = dtDueTime.Value,
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
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Clear();
                }
            }
            listBoxUsers.ClearSelected();
        }

        public void SelectedUsers(IEnumerable selectedItems, Task userTask)
        {
            foreach (var item in selectedItems)
            {
                var user = ctx.Users.Find(item.ToString());
                userTask.Users.Add(user);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            logOffToolStripMenuItem.Enabled = false;
        }

        private void logOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sign Out?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                ClearText();
                listBoxUsers.DataSource = null;
                listBoxUsers.Items.Clear();
                groupBox1.Enabled = false;
                loginToolStripMenuItem.Enabled = true;
                logOffToolStripMenuItem.Enabled = false;
                Auth = false;
            }
        }
    }
}
