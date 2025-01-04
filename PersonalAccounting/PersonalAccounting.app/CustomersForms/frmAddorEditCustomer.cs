using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace PersonalAccounting.app
{
    public partial class frmAddorEditCustomer : Form
    {
        public frmAddorEditCustomer()
        {
            InitializeComponent();
        }
        UnitOfWorks db=new UnitOfWorks();

        private void btnSelectPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile=new OpenFileDialog();
            if(openFile.ShowDialog() == DialogResult.OK )
            {
                pcCustomerPic.ImageLocation=openFile.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {Customers customers= new Customers()
            {
                FulName=txtName.Text,
                Mobile=txtMobile.Text,
                Email=txtEmail.Text,
                Address=txtAddress.Text,
                CustomerImage="NoPhoto.jpg"
            };

                db.CustomerRepository.InsertCustomer(customers);
                db.save();
                DialogResult= DialogResult.OK;
            }
        }
    }
}
