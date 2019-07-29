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
namespace Fitness
{
    public partial class Reception : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        private Employee employee;
        public Reception(dbFitnessEntities fitnessEntities2,Employee employee2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
            employee = employee2;
        }

        private void HesabdanÇıxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            string nameText = txtName.Text.Trim();
            string surnameText = txtSurname.Text.Trim();
            Customer customer = new Customer();
            customer.Name = nameText;
            customer.Surname = surnameText;
            fitnessEntities.Customers.Add(customer);
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Yeni müştəri əlavə olundu");
            txtName.Text = txtSurname.Text = "";
            OrderPage orderPage = new OrderPage(fitnessEntities, employee,customer);
            orderPage.ShowDialog();
        }

        private void ÜmumiSiyahıToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers(fitnessEntities);
            customers.ShowDialog();
        }

        private void ÜmumiSiyahıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Services services = new Services(fitnessEntities,employee);
            services.ShowDialog();
        }

        private void ÜmumiSiyahıToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Times times = new Times(fitnessEntities,employee);
            times.ShowDialog();
        }

        private void ÜmumiSiyahıToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Packages packages = new Packages(fitnessEntities, employee);
            packages.ShowDialog();
        }

        private void ÜmumiSiyahıToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            OrderDetailsList orderlist = new OrderDetailsList(fitnessEntities);
            orderlist.ShowDialog();
        }
    }
}
