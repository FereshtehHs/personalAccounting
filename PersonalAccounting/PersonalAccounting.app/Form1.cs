﻿using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalAccounting.app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomers frmCustomers=new frmCustomers();
            frmCustomers.ShowDialog();
        }

        private void btnNewTracking_Click(object sender, EventArgs e)
        {
            frmNewTransaction frmNewTransaction =new frmNewTransaction();
            frmNewTransaction.ShowDialog();

        }
    }
}
