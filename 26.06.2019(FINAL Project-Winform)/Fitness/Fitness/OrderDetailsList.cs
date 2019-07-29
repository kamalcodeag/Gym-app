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
    public partial class OrderDetailsList : Form
    {
        private readonly dbFitnessEntities fitnessEntities;
        public OrderDetailsList(dbFitnessEntities fitnessEntities2)
        {
            InitializeComponent();
            fitnessEntities = fitnessEntities2;
        }

        private void OrderList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = fitnessEntities.OrderDetails
                .Select(delegate (OrderDetail orderDetail)
                {
                    return new
                    {
                        Müştəri = orderDetail.Order,
                        Paket = orderDetail.Package,
                        Xidmət = orderDetail.Service
                    };
                })
                .ToList();
        }
    }
}
