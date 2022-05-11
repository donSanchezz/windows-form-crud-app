using investments.Forms;
using investments.Models;

namespace investments
{
    public partial class Form1 : Form
    {
        private AppDbContext db { get; set; }
        private int Id { get; set; }
        private bool flag = false;
        public Form1()
        {
            InitializeComponent();
            this.db = new AppDbContext();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

                     

            PopoulateTable();
        }


        private void PopoulateTable()
        {
            dataGridView1.Rows.Clear();
            var coupons = (from cp in db.Coupons
                           from sec in db.Securities
                           from status in db.StatusCodes
                           where cp.IsinCode == sec.IsinCode && cp.StatusId == status.StatusId
                           select new
                           {
                               CouponId = cp.CouponId,
                               Description = sec.Description,
                               PaymentDate = cp.PaymentDate,
                               Status = status.StatusName
                           }).ToList();

            if (coupons.Count > 0)
            {
                for (int i = 0; i < coupons.Count; i++)
                {
                    dataGridView1.Rows.Add(coupons[i].CouponId, coupons[i].Description, coupons[i].PaymentDate, coupons[i].Status);
                    if (coupons[i].Status != "ACTIVE")
                    {
                        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
                        dataGridViewCellStyle2.Padding = new Padding(99999, 0, 0, 0);
                        dataGridView1.Rows[i].Cells["Cancel"].Style = dataGridViewCellStyle2;
                    }

                    if (coupons[i].Status != "PENDING")
                    {
                        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
                        dataGridViewCellStyle2.Padding = new Padding(99999, 0, 0, 0);
                        dataGridView1.Rows[i].Cells["Approve"].Style = dataGridViewCellStyle2;
                    }
                }
                   
            }
            else
            {
                //MessageBox.Show("No coupons found");
            }

            isinComboBox.DataSource = db.Coupons.Select(x => x.IsinCode).Distinct().ToList();
            statusComboBox.DataSource = db.StatusCodes.Select(x =>new KeyValuePair<int, string>(x.StatusId, x.StatusName)).Distinct().ToList();
            statusComboBox.DisplayMember = "Value";

            
        }

        public void btnDashboard_Click(object sender, EventArgs e)
        {
            //securityLabel.Visible = true;
            //isinComboBox.Visible = true;
            //comboBoxFilter.Visible = true;
            //startDateLbl.Visible = true;
            //dateTimePicker1.Visible = true;
            //dateTimePicker2.Visible = true;
            //endDateLbl.Visible = true;
            //statusLbl.Visible = true;
            //statusComboBox.Visible = true;
            dataGridView1.Rows.Clear();
            DesktopPanel.Controls.Clear();
            DesktopPanel.Controls.Add(dataGridView1);
            PopoulateTable();
            label1.Text = "Dashboard";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //securityLabel.Visible = false;
            //isinComboBox.Visible = false;
            //comboBoxFilter.Visible = false;
            //startDateLbl.Visible = false;
            //dateTimePicker1.Visible = false;
            //dateTimePicker2.Visible = false;
            //endDateLbl.Visible = false;
            //statusLbl.Visible = false;
            //statusComboBox.Visible = false;
            //DesktopPanel.Controls.Clear();
            Form child = new AddCoupon(Id);
            child.TopLevel = false;
            child.Dock = DockStyle.Fill;
            DesktopPanel.Controls.Add(child);
            DesktopPanel.Tag = child;
            child.BringToFront();
            child.Show();

            if(flag)
            {
                label1.Text = "Edit Coupon";
                flag = false;
                Id = 0;
            }
            else
            {
                label1.Text = "New Coupon";
            }
               
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void isinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var coupons = (from cp in db.Coupons
                           from sec in db.Securities
                           from status in db.StatusCodes
                           where cp.IsinCode == sec.IsinCode && cp.StatusId == status.StatusId && cp.IsinCode.Contains(isinComboBox.SelectedItem.ToString())
                           select new
                           {
                               CouponId = cp.CouponId,
                               Description = sec.Description,
                               PaymentDate = cp.PaymentDate,
                               Status = status.StatusName
                           }).ToList();

            if (coupons.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < coupons.Count; i++)
                {
                    dataGridView1.Rows.Add(coupons[i].CouponId, coupons[i].Description, coupons[i].PaymentDate, coupons[i].Status);
                    if (coupons[i].Status == "PENDING")
                    {
                        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
                        dataGridViewCellStyle2.Padding = new Padding(99999, 0, 0, 0);
                        dataGridView1.Rows[i].Cells["Cancel"].Style = dataGridViewCellStyle2;
                    }
                }
            }
            else
            {
                PopoulateTable();
            }
        }

        private void comboBoxFilter_TextChanged(object sender, EventArgs e)
        {
            isinComboBox.DataSource = db.Coupons.Where(x => x.IsinCode.Contains(comboBoxFilter.Text)).Select(x => x.IsinCode).Distinct().ToList();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine(dateTimePicker1.Value);


            var coupons = (from cp in db.Coupons
                           from sec in db.Securities
                           from status in db.StatusCodes
                           where cp.IsinCode == sec.IsinCode && cp.StatusId == status.StatusId && cp.PaymentDate == dateTimePicker1.Value
                           select new
                           {
                               CouponId = cp.CouponId,
                               Description = sec.Description,
                               PaymentDate = cp.PaymentDate,
                               Status = status.StatusName
                           }).ToList();

            if (coupons.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < coupons.Count; i++)
                {
                    dataGridView1.Rows.Add(coupons[i].CouponId, coupons[i].Description, coupons[i].PaymentDate, coupons[i].Status);
                    if (coupons[i].Status == "PENDING")
                    {
                        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
                        dataGridViewCellStyle2.Padding = new Padding(99999, 0, 0, 0);
                        dataGridView1.Rows[i].Cells["Cancel"].Style = dataGridViewCellStyle2;
                    }
                }
            }
            else
            {
                PopoulateTable();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            var coupons = (from cp in db.Coupons
                           from sec in db.Securities
                           from status in db.StatusCodes
                           where cp.IsinCode == sec.IsinCode && cp.StatusId == status.StatusId && cp.PaymentDate >= dateTimePicker1.Value.Date && cp.PaymentDate <= dateTimePicker2.Value.Date
                           select new
                           {
                               CouponId = cp.CouponId,
                               Description = sec.Description,
                               PaymentDate = cp.PaymentDate,
                               Status = status.StatusName
                           }).ToList();

            if (coupons.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < coupons.Count; i++)
                {
                    dataGridView1.Rows.Add(coupons[i].CouponId, coupons[i].Description, coupons[i].PaymentDate, coupons[i].Status);
                    if (coupons[i].Status == "PENDING")
                    {
                        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
                        dataGridViewCellStyle2.Padding = new Padding(99999, 0, 0, 0);
                        dataGridView1.Rows[i].Cells["Cancel"].Style = dataGridViewCellStyle2;
                    }
                }
            }
            else
            {
                PopoulateTable();
            }
        }

        private void statusComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            var key =  ((KeyValuePair<int, string>)statusComboBox.SelectedItem).Key;
            var coupons = (from cp in db.Coupons
                           from sec in db.Securities
                           from status in db.StatusCodes
                           where cp.IsinCode == sec.IsinCode && cp.StatusId == status.StatusId && key == status.StatusId
                           select new
                           {
                               CouponId = cp.CouponId,
                               Description = sec.Description,
                               PaymentDate = cp.PaymentDate,
                               Status = status.StatusName
                           }).ToList();
            
            if (coupons.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < coupons.Count; i++)
                {
                    dataGridView1.Rows.Add(coupons[i].CouponId, coupons[i].Description, coupons[i].PaymentDate, coupons[i].Status);
                    if (coupons[i].Status == "PENDING")
                    {
                        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
                        dataGridViewCellStyle2.Padding = new Padding(99999, 0, 0, 0);
                        dataGridView1.Rows[i].Cells["Cancel"].Style = dataGridViewCellStyle2;
                    }
                }
            }
            else
            {
                PopoulateTable();
            }
        }

        public void DeleteCoupon(int id)
        {
            var coupon = db.Coupons.Find(id);
            var message = "Are you sure to delete Coupon with Isin Code: " + coupon.IsinCode;
            DialogResult dialogResult = MessageBox.Show(message,"Confirm", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                
                db.Coupons.Remove(coupon);                
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Record has been deleted successfully");
                    PopoulateTable();
                }
                catch
                {
                    MessageBox.Show("An error has occured, please try again later");
                }
               
            }
            else
            {
               
            }
        }

        public void ApproveCoupon(int id)
        {
            var coupon = db.Coupons.Find(id);
            var message = "Are you sure to approve Coupon with Isin Code: " + coupon.IsinCode;
            DialogResult dialogResult = MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                coupon.StatusId = 2;
                db.Update(coupon);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Record has been updated successfully");
                    PopoulateTable();
                }
                catch
                {
                    MessageBox.Show("An error has occured, please try again later");
                }
            }
            else
            {

            }
        }


        public void CancelCoupon(int id)
        {
            var coupon = db.Coupons.Find(id);
            var message = "Are you sure to cancel Coupon with Isin Code: " + coupon.IsinCode;
            DialogResult dialogResult = MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                coupon.StatusId = 3;
                db.Update(coupon);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Record has been cancelled successfully");
                    PopoulateTable();
                }
                catch
                {
                    MessageBox.Show("An error has occured, please try again later");
                }
            }
            else
            {

            }
        }


        void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == dataGridView1.NewRowIndex || e.RowIndex < 0)
                return;

            //Handle Button Column Click
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index)
            {
                this.Id = Int32.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                this.flag = true;
                button1_Click(sender, e);
            }

            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                var id = Int32.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                DeleteCoupon(id);
            }

            if (e.ColumnIndex == dataGridView1.Columns["Approve"].Index)
            {
                var id = Int32.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                ApproveCoupon(id);
            }


            if (e.ColumnIndex == dataGridView1.Columns["Cancel"].Index)
            {
                var id = Int32.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                CancelCoupon(id);
            }

        }


        

    }
}