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
    public partial class Times : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        private Employee user;
        public Times(dbFitnessEntities fitnessEntities2, Employee user2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
            user = user2;
        }
        public Times()
        {
            InitializeComponent();
            fitnessEntities = new dbFitnessEntities();
        }

        private void Times_Load(object sender, EventArgs e)
        {
            if(user.id != 1)
            {
                label1.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;
                textBox2.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                label3.Visible = false;
                textBox4.Visible = false;
                textBox3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                textBox5.Visible = false;
                label2.Location = new Point(275, 150);
                comboBox1.Location = new Point(278, 196);


            }
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Times.ToArray());
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string hours = textBox4.Text.Trim();
            fitnessEntities.Times.Add(new Time
            {
                Name = name,
                Hours = hours
            });
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Yeni xidmət saatı əlavə olundu");
            textBox1.Text = textBox4.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Times.ToArray());
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Time times = (Time)comboBox1.SelectedItem;
            textBox5.Text = times.Name;
            textBox3.Text = times.Hours;
            textBox2.Text = times.ToString();
        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            Time selectedTime = (Time)comboBox1.SelectedItem;
            Time time = fitnessEntities.Times.FirstOrDefault(t => t.id == selectedTime.id);
            time.Name = textBox5.Text.Trim();
            time.Hours = textBox3.Text.Trim();
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Seçilmiş vaxt bölgüsü yeniləndi");
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Times.ToArray());
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            Time selectedTime = (Time)comboBox1.SelectedItem;
            Time time = fitnessEntities.Times.FirstOrDefault(t => t.id == selectedTime.id);
            fitnessEntities.Times.Remove(time);
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Seçilmiş vaxt bölgüsü silindi");
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Times.ToArray());
        }
    }
}
