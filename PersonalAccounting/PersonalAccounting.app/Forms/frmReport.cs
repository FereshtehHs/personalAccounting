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
    public partial class frmReport : Form
    {
        public int TypeID ;
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            if(TypeID ==1) { this.Text = "گزارش دریافتی ها"; }
            else { this.Text = "گزارش پرداختی ها"; }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        void Filter()
        {
            using (UnitOfWorks db = new UnitOfWorks())
            {
                var result = db.AccountingRepository.Get(a => a.TypeID == TypeID);
                dgvReport.AutoGenerateColumns = false;
                dgvReport.DataSource= result;
            }
        }
    }
}
