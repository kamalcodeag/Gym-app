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
    public partial class OrderPage : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        private Employee employee;
        private Customer customer;


        public OrderPage(dbFitnessEntities fitnessEntities2, Employee employee2,Customer customer2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
            employee = employee2;
            customer = customer2;
        }

        private void OrderPage_Load(object sender, EventArgs e)
        {
            comboBox3.Items.Add(employee);
            comboBox3.SelectedIndex = 0;
            comboBox4.Items.Add(customer);
            comboBox4.SelectedIndex = 0;
            comboBox1.Items.AddRange(fitnessEntities.Services.ToArray());
            comboBox2.Items.AddRange(fitnessEntities.Packages.ToArray());
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            Employee selectedEmployee = (Employee)comboBox3.SelectedItem;
            Customer selectedCustomer = (Customer)comboBox4.SelectedItem;
            DateTime dateTime = dateTimePicker1.Value;
            decimal price = numericUpDown1.Value;
            Order order = new Order();
            order.EmployeeId = selectedEmployee.id;
            order.CustomerId = selectedCustomer.id;
            order.Date = dateTime;
            order.Price = price;
            fitnessEntities.Orders.Add(order);
            await fitnessEntities.SaveChangesAsync();
            Service service = (Service)comboBox1.SelectedItem;
            Package package = (Package)comboBox2.SelectedItem;
            if (comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex == -1)
            {
                comboBox2.Visible = false;
                package = null;
            }
            if(comboBox2.SelectedIndex >= 0 && comboBox1.SelectedIndex == -1)
            {
                comboBox1.Visible = false;
                service = null;
            }
            MessageBox.Show("Sifariş qeydə alındı");
            dateTimePicker1.Value = DateTime.Now;
            numericUpDown1.Value = 0;
            this.Close();
            OrderDetailsPage orderDetailsPage = new OrderDetailsPage(fitnessEntities,order,service,package);
            orderDetailsPage.ShowDialog();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Visible = false;
            comboBox2.Visible = false;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Visible = false;
            comboBox1.Visible = false;
        }
    }
}
