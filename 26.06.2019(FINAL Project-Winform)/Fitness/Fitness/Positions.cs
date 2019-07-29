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
    public partial class Positions : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        private Employee employee;

        public Positions(dbFitnessEntities fitnessEntities2,Employee employee2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
            employee = employee2;
        }

        private void Positions_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Positions.ToArray());
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();

            Position position = new Position();
            position.Name = name;
            fitnessEntities.Positions.Add(position);
            MessageBox.Show("Yeni vəzifə əlavə olundu");
            await fitnessEntities.SaveChangesAsync();
            textBox1.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Positions.ToArray());
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Position selectedPosition = (Position)comboBox1.SelectedItem;
            textBox3.Text = selectedPosition.Name;
            textBox2.Text = selectedPosition.Name;
        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text.Trim();
            Position selectedPosition = (Position)comboBox1.SelectedItem;
            Position position = fitnessEntities.Positions.FirstOrDefault(s => s.id == selectedPosition.id);
            position.Name = name;
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Seçilmiş vəzifə yeniləndi");
            textBox2.Text = textBox3.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Positions.ToArray());
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            Position selectedPosition = (Position)comboBox1.SelectedItem;
            Position position = fitnessEntities.Positions.FirstOrDefault(s => s.id == selectedPosition.id);
            fitnessEntities.Positions.Remove(position);
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Seçilmiş vəzifə silindi");
            textBox2.Text = textBox3.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Positions.ToArray());
        }
    }
}
