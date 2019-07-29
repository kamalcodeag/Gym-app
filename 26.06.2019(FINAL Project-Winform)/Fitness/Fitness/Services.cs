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
    public partial class Services : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        private Employee employee;

        public Services(dbFitnessEntities fitnessEntities2, Employee employee2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
            employee = employee2;
        }
        private void Services_Load(object sender, EventArgs e)
        {
            if(employee.id != 1)
            {
                label1.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;
                textBox3.Visible = false;
                button3.Visible = false;
                button2.Visible = false;
                textBox2.Visible = false;
                comboBox2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                numericPrice.Visible = false;
                label5.Visible = false;
                label2.Location = new Point(48,141);
                comboBox1.Location = new Point(51,180);
            }
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Services.ToArray());
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(fitnessEntities.Times.ToArray());
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            Time time = (Time)comboBox2.SelectedItem;
            decimal price = numericPrice.Value;
            Service service = new Service();
            service.Name = name;
            service.Time = time;
            service.Price__per_hour_ = price;
            fitnessEntities.Services.Add(service);
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Yeni xidmət əlavə olundu");
            textBox1.Text = "";
            numericPrice.Value = 0;
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Services.ToArray());
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Service selectedService = (Service)comboBox1.SelectedItem;
            textBox3.Text = selectedService.Name;
            textBox2.Text = selectedService.Name;
        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text.Trim();
            Service selectedService = (Service)comboBox1.SelectedItem;
            Service service = fitnessEntities.Services.FirstOrDefault(s => s.id == selectedService.id);
            service.Name = name;
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Seçilmiş xidmət yeniləndi");
            textBox2.Text = textBox3.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Services.ToArray());
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            Service selectedService = (Service)comboBox1.SelectedItem;
            Service service = fitnessEntities.Services.FirstOrDefault(s => s.id == selectedService.id);
            fitnessEntities.Services.Remove(service);
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Seçilmiş xidmət silindi");
            textBox2.Text = textBox3.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Services.ToArray());
        }
    }
}
