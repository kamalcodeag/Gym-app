using Fitness.Model;
using static Fitness.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitness
{
    public partial class Confirmation : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        private Employee user;
        public Confirmation(dbFitnessEntities fitnessEntities2,Employee user2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
            user = user2;
        }

        private async void BtnConfirm_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmationPassword = txtConfirmationPassword.Text.Trim();

            Employee employee = fitnessEntities.Employees.FirstOrDefault(emp => emp.id == user.id);

            if(txtCurrentPassword.Text != "" && txtNewPassword.Text != "" && txtConfirmationPassword.Text != "")
            {
                if (currentPassword != newPassword && newPassword == confirmationPassword)
                {
                    employee.Password = GetHash(newPassword);
                    MessageBox.Show("Şifrəniz dəyişdirildi");
                    this.Close();
                    employee.HasVerified = true;
                    await fitnessEntities.SaveChangesAsync();
                }
                else
                {
                    MessageBox.Show("Yeni şifrə cari şifrədən fərqli, təsdiq şifrəsi isə yeni şifrə ilə eyni olmalıdır");
                }
            }
            else
            {
                MessageBox.Show("Zəhmət olmasa, boşluqları doldurun");
            }
            

        }
    }
}
