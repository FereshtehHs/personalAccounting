using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using System;
using Accounting.DataLayer.Repository;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;
using System.Security.Cryptography.X509Certificates;

namespace PersonalAccounting.app
{
    public partial class frmNewTransaction : Form
    {
        UnitOfWorks db = new UnitOfWorks();
        public frmNewTransaction()
        {
            InitializeComponent();
        }

        private void frmNewTransaction_Load(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetCustomerNames();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetCustomerNames(txtFilter.Text);
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbRecieve.Checked || rbPay.Checked)
                {
                    Accounting.DataLayer.Accounting accounting = new Accounting.DataLayer.Accounting()
                    {
                      
                        Amount = int.Parse(txtAmount.Value.ToString()),
                        CustomerID = db.CustomerRepository.GetCustomerIdByName(txtName.Text),
                        TypeID = (rbRecieve.Checked) ? 1 : 2,
                        DateTime = DateTime.Now,
                        Description = txtDescription.Text,
                };
                    db.AccountingRepository.Insert(accounting);
                    db.save();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("لطفا نوع تراکنش را انتخاب کنید.", "نوع تراکنش");
                }
            }
        }
    }
}
