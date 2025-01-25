using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public int customerId = 0;
        UnitOfWorks db = new UnitOfWorks();

        private void btnSelectPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pcCustomerPic.ImageLocation = openFile.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomerPic.ImageLocation);
                string path = Application.StartupPath + "/Images/";
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pcCustomerPic.Image.Save(path + imageName );
                Customers customers = new Customers()
                {
                    FulName = txtName.Text,
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    Address = txtAddress.Text,
                    CustomerImage = imageName
                };
                if (customerId == 0)
                {
                    db.CustomerRepository.InsertCustomer(customers);
                }
                else
                {
                    customers.CustomerID = customerId;
                    db.CustomerRepository.UpdateCustomer(customers);
                }
                    db.save();
                DialogResult = DialogResult.OK;
            }
        }

        private void frmAddorEditCustomer_Load(object sender, EventArgs e)
        {
            if(customerId !=0)
            {
                this.Text = "ویرایش فرد";
                btnSave.Text = "ویرایش";
                var customer = db.CustomerRepository.GetCustomerById(customerId);
                txtName.Text=customer.FulName; 
                txtMobile.Text=customer.Mobile;
                txtAddress.Text=customer.Address;
                txtEmail.Text=customer.Email;
                pcCustomerPic.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerImage;
            }
        }
    }
}
