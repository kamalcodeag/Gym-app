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
    public partial class Packages : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        private Employee employee;

        public Packages(dbFitnessEntities fitnessEntities2, Employee employee2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
            employee = employee2;
        }

        private void Packages_Load(object sender, EventArgs e)
        {
            if (employee.id != 1)
            {
                label5.Visible = false;
                numericPrice.Visible = false;
                label4.Visible = false;
                label3.Visible = false;
                comboBox2.Visible = false;
                button3.Visible = false;
                button2.Visible = false;
                label1.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;
                flowLayoutPanel1.Visible = false;
                label2.Location = new Point(51, 141);
                comboBox1.Location = new Point(54, 180);
            }
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Packages.ToArray());
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(fitnessEntities.Times.ToArray());

            foreach (var item in fitnessEntities.Services)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.Text = item.Name;
                checkbox.Tag = item.id;
                flowLayoutPanel1.Controls.Add(checkbox);
            }
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            Time time = (Time)comboBox2.SelectedItem;
            decimal price = numericPrice.Value;
            Package package = new Package();
            package.Name = name;
            package.Time = time;
            package.Price__per_month_ = price;
            fitnessEntities.Packages.Add(package);
            await fitnessEntities.SaveChangesAsync();

            foreach (var item in flowLayoutPanel1.Controls)
            {
                CheckBox checkbox = (CheckBox)item;
                if (checkbox.Checked)
                {
                    ServicesToPackage servicesToPackage = new ServicesToPackage();
                    servicesToPackage.ServiceId = Convert.ToInt32(checkbox.Tag);
                    servicesToPackage.PackageId = package.id;
                    fitnessEntities.ServicesToPackages.Add(servicesToPackage);
                }
            }
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Yeni paket əlavə olundu");
            textBox1.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Packages.ToArray());
            comboBox2.SelectedIndex = -1;
            numericPrice.Value = 0;
            foreach (var item in flowLayoutPanel1.Controls)
            {
                CheckBox checkbox = (CheckBox)item;
                checkbox.Checked = false;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in flowLayoutPanel1.Controls)
            {
                CheckBox checkbox = (CheckBox)item;
                checkbox.Checked = false;
            }
            Package selectedPackage = (Package)comboBox1.SelectedItem;
            textBox1.Text = selectedPackage.Name;
            comboBox2.SelectedIndex = (int)selectedPackage.TimeId - 5;
            numericPrice.Value = selectedPackage.Price__per_month_;

            List<ServicesToPackage> servicesToPackages = fitnessEntities.ServicesToPackages.Where(stp => stp.PackageId == selectedPackage.id).ToList();

            foreach (var item in flowLayoutPanel1.Controls)
            {
                CheckBox checkbox = (CheckBox)item;
                foreach (var stp in servicesToPackages)
                {
                    if (stp.ServiceId == Convert.ToInt32(checkbox.Tag))
                    {
                        checkbox.Checked = true;
                    }
                }
            }
        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            Package selectedPackage = (Package)comboBox1.SelectedItem;
            selectedPackage.Name = textBox1.Text.Trim();
            Time selectedTime = (Time)comboBox2.SelectedItem;
            selectedPackage.Time = selectedTime;
            selectedPackage.Price__per_month_ = numericPrice.Value;

            foreach(var item in fitnessEntities.ServicesToPackages.Where(stp => stp.PackageId == selectedPackage.id).ToList())
            {
                fitnessEntities.ServicesToPackages.Remove(item);
            }
            await fitnessEntities.SaveChangesAsync();

            foreach (var item in flowLayoutPanel1.Controls)
            {
                CheckBox checkbox = (CheckBox)item;
                if(checkbox.Checked)
                {
                    ServicesToPackage servicesToPackage = new ServicesToPackage();
                    servicesToPackage.PackageId = selectedPackage.id;
                    servicesToPackage.ServiceId = (int)checkbox.Tag;
                    fitnessEntities.ServicesToPackages.Add(servicesToPackage);
                }
            }
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Seçilmiş paketin məlumatları yeniləndi");
            textBox1.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Packages.ToArray());
            comboBox2.SelectedIndex = -1;
            numericPrice.Value = 0;
            foreach (var item in flowLayoutPanel1.Controls)
            {
                CheckBox checkbox = (CheckBox)item;
                checkbox.Checked = false;
            }
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            Package selectedPackage = (Package)comboBox1.SelectedItem;
            foreach (var item in fitnessEntities.ServicesToPackages.Where(stp => stp.PackageId == selectedPackage.id).ToList())
            {
                fitnessEntities.ServicesToPackages.Remove(item);
            }
            fitnessEntities.Packages.Remove(selectedPackage);
            await fitnessEntities.SaveChangesAsync();

            foreach (var con in flowLayoutPanel1.Controls)
            {
                CheckBox checkbox = (CheckBox)con;
                if (checkbox.Checked)
                {
                    foreach (var item in fitnessEntities.ServicesToPackages.Where(stp => stp.ServiceId == (int)checkbox.Tag).ToList())
                    {
                        fitnessEntities.ServicesToPackages.Remove(item);
                    }
                }
            }
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Seçilmiş paket silindi");
            textBox1.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(fitnessEntities.Packages.ToArray());
            comboBox2.SelectedIndex = -1;
            numericPrice.Value = 0;
            foreach (var item in flowLayoutPanel1.Controls)
            {
                CheckBox checkbox = (CheckBox)item;
                checkbox.Checked = false;
            }
        }
    }
}
