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
    public partial class Customers : Form
    {
        private readonly dbFitnessEntities fitnessEntities;

        public Customers(dbFitnessEntities fitnessEntities2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = fitnessEntities.Customers
                .Select(delegate (Customer customer)
                {
                    return new
                    {
                        Id = customer.id,
                        Ad = customer.Name,
                        Soyad = customer.Surname
                    };
                })
                .ToList();
        }
    }
}
