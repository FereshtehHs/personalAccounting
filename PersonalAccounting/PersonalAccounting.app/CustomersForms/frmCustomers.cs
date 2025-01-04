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

namespace PersonalAccounting.app
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        void BindGrid()
        {
            using(UnitOfWorks db=new UnitOfWorks())
            {
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource = db.CustomerRepository.GetAllCustomers();
            }
        }

        private void btnRefreshCustomers_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            BindGrid();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using(UnitOfWorks db=new UnitOfWorks())
            {
                dgvCustomers.DataSource=db.CustomerRepository.GetCustomersByFilter(txtFilter.Text);
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            using (UnitOfWorks db = new UnitOfWorks())
            {
                if (dgvCustomers.CurrentRow != null)
                {
                    string name = dgvCustomers.CurrentRow.Cells[1].Value.ToString();
                    if (MessageBox.Show($"آیا از حذف {name} مطمن هستید؟", "توجه", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning )== DialogResult.Yes)

                  { /* Customers customer= dgvCustomers.CurrentRow.ro;
                    db.CustomerRepository.DeleteCustomer(customer);*/
                        int customerId = Convert.ToInt32(dgvCustomers.CurrentRow.Cells[0].Value);
                        db.CustomerRepository.DeleteCustomerById(customerId);
                        db.save();
                        BindGrid();
                  }
                }
                else
                {
                    MessageBox.Show("لطفا فرد مورد نظر را انتخاب کنید. ");
                }
            }
        }

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            frmAddorEditCustomer frmAddorEditCustomer=new frmAddorEditCustomer();
           if( frmAddorEditCustomer.ShowDialog()==DialogResult.OK)
            {
                BindGrid();
            }
        }
    }
}
