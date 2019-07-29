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
    public partial class Employees : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        public Employees(dbFitnessEntities fitnessEntities2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
        }

        public Employees()
        {
            InitializeComponent();
            fitnessEntities = new dbFitnessEntities();
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            dgwCustomers.DataSource = null;
            dgwCustomers.DataSource = fitnessEntities.Employees
                .Select(delegate (Employee employee)
                {
                    return new
                    {
                        Name = employee.Name,
                        Surname = employee.Surname,
                        Position = employee.Position
                    };
                })
                .ToList();
        }

        private void ƏlavəEtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EmployeesEdit employeesAdd = new EmployeesEdit(fitnessEntities);
            employeesAdd.ShowDialog();
        }
    }
}
