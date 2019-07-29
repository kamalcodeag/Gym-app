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
using static Fitness.Hash;

namespace Fitness
{
    public partial class EmployeesEdit : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        public EmployeesEdit(dbFitnessEntities fitnessEntities2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
        }

        public EmployeesEdit()
        {
            InitializeComponent();
            fitnessEntities = new dbFitnessEntities();
        }

        private void EmployeesEdit_Load(object sender, EventArgs e)
        {
            List<Position> position = fitnessEntities.Positions.Where(x => x.id > 1).ToList();
            cmbPosition.Items.AddRange(position.ToArray());
            cmbPosition.SelectedIndex = 0;
            comboBox2.Items.AddRange(fitnessEntities.Employees.ToArray());
        }
        public void ResetFields()
        {
            txtName.Text = txtSurname.Text = txtUsername.Text = txtPassword.Text = string.Empty;
        }

        private async void BtnCreate_Click(object sender, EventArgs e)
        {
            string nameText = txtName.Text.Trim();
            string surnameText = txtSurname.Text.Trim();
            string usernameText = txtUsername.Text.Trim();
            string passwordText = txtPassword.Text.Trim();
            Position position = cmbPosition.SelectedItem as Position;
            Employee employee = new Employee();
            employee.Name = nameText;
            employee.Surname = surnameText;
            employee.Username = usernameText;
            employee.Password = GetHash(passwordText);
            employee.Position = position;
            employee.HasVerified = false;
            fitnessEntities.Employees.Add(employee);
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Yeni işçi əlavə olundu");
            ResetFields();
            List<Position> position2 = fitnessEntities.Positions.Where(x => x.id > 1).ToList();
            cmbPosition.Items.Clear();
            cmbPosition.Items.AddRange(position2.ToArray());
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(fitnessEntities.Employees.ToArray());
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employee selectedEmployee = (Employee)comboBox2.SelectedItem;
            Employee employee = fitnessEntities.Employees.FirstOrDefault(emp => emp.id == selectedEmployee.id);
            textBox5.Text = employee.Name;
            textBox4.Text = employee.Surname;
            comboBox1.Items.Clear();
            comboBox1.Items.Add(employee.Position.ToString());
            comboBox1.SelectedIndex = 0;
        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            Employee selectedEmployee = (Employee)comboBox2.SelectedItem;
            string name = textBox5.Text.Trim();
            string surname = textBox4.Text.Trim();
            string position = comboBox1.SelectedItem.ToString();
            selectedEmployee.Name = name;
            selectedEmployee.Surname = surname;
            selectedEmployee.Position.Name = position;
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Seçilmiş işçinin məlumatları yeniləndi");
            cmbPosition.Items.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(fitnessEntities.Employees.ToArray());
            textBox5.Text = textBox4.Text = "";
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            Employee selectedEmployee = (Employee)comboBox2.SelectedItem;
            Employee employee = fitnessEntities.Employees.FirstOrDefault(emp => emp.id == selectedEmployee.id);
            fitnessEntities.Employees.Remove(employee);
            await fitnessEntities.SaveChangesAsync();
            MessageBox.Show("Seçilmiş işçi silindi");
            cmbPosition.Items.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(fitnessEntities.Employees.ToArray());
            textBox5.Text = textBox4.Text = "";
        }
    }
}
