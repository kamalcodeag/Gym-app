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
    public partial class OrderDetailsPage : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        private Order order;
        private Service service;
        private Package package;
        public OrderDetailsPage(dbFitnessEntities fitnessEntities2, Order order2, Service service2,Package package2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
            order = order2;
            service = service2;
            package = package2;
        }

        private async void OrderDetailsPage_Load(object sender, EventArgs e)
        {
            comboBox3.Items.Add(order.Employee);
            comboBox3.SelectedIndex = 0;
            comboBox4.Items.Add(order.Customer);
            comboBox4.SelectedIndex = 0;
            dateTimePicker1.Value = order.Date;
            numericUpDown1.Value = order.Price;
            OrderDetail orderDetail = new OrderDetail();
            if (service != null)
            {
                label5.Visible = false;
                comboBox2.Visible = false;
                comboBox1.Items.Add(service);
                comboBox1.SelectedIndex = 0;
                orderDetail.ServiceId = service.id;
            }
            if (package != null)
            {
                label6.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Items.Add(package);
                comboBox2.SelectedIndex = 0;
                orderDetail.PackageId = package.id;
            }
            orderDetail.OrderId = order.id;
            fitnessEntities.OrderDetails.Add(orderDetail);
            await fitnessEntities.SaveChangesAsync();
        }
    }
}
