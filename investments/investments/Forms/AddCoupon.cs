using investments.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace investments.Forms
{
    public partial class AddCoupon : Form
    {
        private AppDbContext db { get; set; }
        private Form1 form;
        private Coupon Coupon;
        private int id;
        private bool flag;
        public AddCoupon(int id)
        {
            var dict = new Dictionary<string, string>();
            InitializeComponent();
            this.db = new AppDbContext();
            form = new Form1();
            //var sec = new List<(string Id, string Desc)>();
            List<Security> sec = new List<Security>();

            if (id != 0)
            {
                flag = true;
                Coupon = db.Coupons.Find(id);
                var selected = db.Securities.Where(x => x.IsinCode == Coupon.IsinCode).ToList().First();
                sec.Add(new Security { IsinCode = selected.IsinCode, Description = selected.Description});
                var items = db.Securities.ToList();

                foreach(var item in items)
                {
                    if(item.IsinCode != selected.IsinCode)
                        sec.Add(item);
                }

                isinComboBox.DisplayMember = "Description";
                isinComboBox.ValueMember = "IsinCode";
                isinComboBox.DataSource = sec;
                dateTimePicker1.Value = (DateTime)Coupon.PaymentDate;
            }
            else
            {
                sec = db.Securities.ToList();
                isinComboBox.DisplayMember = "Description";
                isinComboBox.ValueMember = "IsinCode";
                isinComboBox.DataSource = sec;             
            }

            id = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(isinComboBox.Text))
            {
                isinComboBox.Focus();
                errorProvider1.SetError(isinComboBox, "Please enter ISIN code");
            }
            if (!flag)
            {
                Coupon coupon = new Coupon();
                coupon.IsinCode = isinComboBox.SelectedValue.ToString();
                coupon.PaymentDate = dateTimePicker1.Value;
                coupon.RecordDate = DateTime.UtcNow;
                coupon.StatusId = 1;

                db.Coupons.Add(coupon);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Record has been added");
                }
                catch
                {
                    MessageBox.Show("An error has occured, please try again later");
                }
            }
            else
            {
              Coupon.IsinCode = isinComboBox.SelectedValue.ToString();
              Coupon.PaymentDate = dateTimePicker1.Value;
              Coupon.RecordDate = DateTime.UtcNow;
              db.Update(Coupon);

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Record has been updated");
                   
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("An error has occured, please try again later");
                }
             
            }
           
            this.Hide();
            flag = false;

        }

        private void AddCoupon_Load(object sender, EventArgs e)
        {

        }
    }

}