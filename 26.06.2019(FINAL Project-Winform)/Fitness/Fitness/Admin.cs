using Fitness.Model;
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
    public partial class Admin : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        private Employee user;

        public Admin(dbFitnessEntities fitnessEntities2,Employee user2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
            user = user2;
        }

        public Admin()
        {
            InitializeComponent();
            fitnessEntities = new dbFitnessEntities();
        }

        private void ÜmumiSiyahıToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Employees employees = new Employees(fitnessEntities);
            employees.ShowDialog();
        }

        private void ƏlavəEtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EmployeesEdit employeesAdd = new EmployeesEdit(fitnessEntities);
            employeesAdd.ShowDialog();
        }

        private void HesabdanÇıxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ÜmumiSiyahıToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers(fitnessEntities);
            customers.ShowDialog();
        }

        private void VəzifəToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Positions positions = new Positions(fitnessEntities,user);
            positions.ShowDialog();
        }

        private void XidmətToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Services services = new Services(fitnessEntities,user);
            services.ShowDialog();
        }

        private void VaxtBölgüsüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Times times = new Times(fitnessEntities, user);
            times.ShowDialog();
        }

        private void PaketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fitnessEntities.Services.Count() > 0)
            {
                Packages packages = new Packages(fitnessEntities, user);
                packages.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hal-hazırda xidmət olmadığından paketlərdən yararlanmaq mümkün deyil");
            }
        }

        private void ÜmumiSiyahıToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            OrderDetailsList orderlist = new OrderDetailsList(fitnessEntities);
            orderlist.ShowDialog();
        }
    }
}
