using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fitness.Model;
using static Fitness.Hash;

namespace Fitness
{
    public partial class UserMenu : System.Windows.Forms.Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        public UserMenu()
        {
            InitializeComponent();
            fitnessEntities = new dbFitnessEntities();
        }

        public void ResetFields()
        {
            txtUsername.Text = txtPassword.Text = string.Empty;
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            Employee employee = fitnessEntities.Employees.FirstOrDefault(emp => emp.Username.ToLower() == username.ToLower());

            if(txtUsername.Text == "" && txtPassword.Text == "")
            {
                MessageBox.Show("Zəhmət olmasa, boşluqları doldurun");
                ResetFields();
                return;
            }
            if (employee == null)
            {
                MessageBox.Show("Istifadəçi adı və ya şifrə səhvdir");
                ResetFields();
                return;
            }
            if (!CheckHash(employee.Password, password))
            {
                MessageBox.Show("Istifadəçi adı və ya şifrə səhvdir");
                ResetFields();
                return;
            }
            if (employee.HasVerified == false)
            {
                Confirmation confirmation = new Confirmation(fitnessEntities, employee);
                confirmation.ShowDialog();
                ResetFields();
                return;
            }

            if (employee.PositionId == 1)
            {
                Admin admin = new Admin(fitnessEntities, employee);
                admin.Show();
                ResetFields();
            }
            else if(employee.PositionId == 2)
            {
                Reception reception = new Reception(fitnessEntities, employee);
                reception.Show();
                ResetFields();
            }
        }
    }
}
