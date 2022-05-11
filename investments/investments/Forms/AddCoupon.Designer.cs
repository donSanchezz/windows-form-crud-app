namespace investments.Forms
{
    partial class AddCoupon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCoupon));
            this.isinCodeLbl = new System.Windows.Forms.Label();
            this.isinComboBox = new System.Windows.Forms.ComboBox();
            this.paymentDateLbl = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // isinCodeLbl
            // 
            resources.ApplyResources(this.isinCodeLbl, "isinCodeLbl");
            this.isinCodeLbl.BackColor = System.Drawing.Color.Transparent;
            this.isinCodeLbl.Name = "isinCodeLbl";
            // 
            // isinComboBox
            // 
            this.isinComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.isinComboBox, "isinComboBox");
            this.isinComboBox.Name = "isinComboBox";
            // 
            // paymentDateLbl
            // 
            resources.ApplyResources(this.paymentDateLbl, "paymentDateLbl");
            this.paymentDateLbl.BackColor = System.Drawing.Color.Transparent;
            this.paymentDateLbl.Name = "paymentDateLbl";
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Name = "dateTimePicker1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddCoupon
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.paymentDateLbl);
            this.Controls.Add(this.isinComboBox);
            this.Controls.Add(this.isinCodeLbl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddCoupon";
            this.Load += new System.EventHandler(this.AddCoupon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label isinCodeLbl;
        private ComboBox isinComboBox;
        private Label paymentDateLbl;
        private DateTimePicker dateTimePicker1;
        private Label validationLbl1;
        private Label validationLbl2;
        private Button button1;
        private ErrorProvider errorProvider1;
    }
}