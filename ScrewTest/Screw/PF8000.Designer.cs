namespace ScrewTest.Screw
{
    partial class PF8000
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbl_ethernetclose = new Label();
            btn_ethernetClose = new Button();
            lbl_ethernetopen = new Label();
            btn_ethernetOpen = new Button();
            richTextBox_Log = new RichTextBox();
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            chkBox_OK_NOK = new CheckBox();
            txt_AngleValue = new TextBox();
            lbl_AngleDisplay = new Label();
            btn_SendData = new Button();
            txt_TorqueValue = new TextBox();
            lbl_TorqueDisplay = new Label();
            btn_PsetSil = new Button();
            btn_PsetEkle = new Button();
            txt_PsetEkle = new TextBox();
            listBox_Pset = new ListBox();
            groupBox2 = new GroupBox();
            btn_LogClear = new Button();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_ethernetclose
            // 
            lbl_ethernetclose.AutoSize = true;
            lbl_ethernetclose.Location = new Point(185, 74);
            lbl_ethernetclose.Name = "lbl_ethernetclose";
            lbl_ethernetclose.Size = new Size(38, 15);
            lbl_ethernetclose.TabIndex = 7;
            lbl_ethernetclose.Text = "label1";
            // 
            // btn_ethernetClose
            // 
            btn_ethernetClose.BackColor = SystemColors.ControlLight;
            btn_ethernetClose.FlatAppearance.BorderSize = 0;
            btn_ethernetClose.FlatStyle = FlatStyle.Flat;
            btn_ethernetClose.Font = new Font("Segoe UI", 9F);
            btn_ethernetClose.ForeColor = Color.Black;
            btn_ethernetClose.Location = new Point(19, 66);
            btn_ethernetClose.Name = "btn_ethernetClose";
            btn_ethernetClose.Size = new Size(144, 31);
            btn_ethernetClose.TabIndex = 6;
            btn_ethernetClose.Text = "Ethernet Portunu Kapat";
            btn_ethernetClose.UseVisualStyleBackColor = false;
            btn_ethernetClose.Click += btn_ethernetClose_Click;
            // 
            // lbl_ethernetopen
            // 
            lbl_ethernetopen.AutoSize = true;
            lbl_ethernetopen.Location = new Point(185, 35);
            lbl_ethernetopen.Name = "lbl_ethernetopen";
            lbl_ethernetopen.Size = new Size(38, 15);
            lbl_ethernetopen.TabIndex = 5;
            lbl_ethernetopen.Text = "label1";
            // 
            // btn_ethernetOpen
            // 
            btn_ethernetOpen.BackColor = SystemColors.ControlLight;
            btn_ethernetOpen.BackgroundImageLayout = ImageLayout.None;
            btn_ethernetOpen.FlatAppearance.BorderSize = 0;
            btn_ethernetOpen.FlatStyle = FlatStyle.Flat;
            btn_ethernetOpen.Font = new Font("Segoe UI", 10F);
            btn_ethernetOpen.Location = new Point(19, 31);
            btn_ethernetOpen.Name = "btn_ethernetOpen";
            btn_ethernetOpen.Size = new Size(144, 27);
            btn_ethernetOpen.TabIndex = 4;
            btn_ethernetOpen.Text = "Ethernet Portunu Aç";
            btn_ethernetOpen.UseVisualStyleBackColor = false;
            btn_ethernetOpen.Click += btn_ethernetOpen_Click;
            // 
            // richTextBox_Log
            // 
            richTextBox_Log.Location = new Point(6, 45);
            richTextBox_Log.Name = "richTextBox_Log";
            richTextBox_Log.Size = new Size(489, 200);
            richTextBox_Log.TabIndex = 8;
            richTextBox_Log.Text = "";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(btn_PsetSil);
            groupBox1.Controls.Add(btn_PsetEkle);
            groupBox1.Controls.Add(txt_PsetEkle);
            groupBox1.Controls.Add(listBox_Pset);
            groupBox1.Controls.Add(btn_ethernetOpen);
            groupBox1.Controls.Add(lbl_ethernetclose);
            groupBox1.Controls.Add(btn_ethernetClose);
            groupBox1.Controls.Add(lbl_ethernetopen);
            groupBox1.Location = new Point(21, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(524, 385);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "İşlemler";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(chkBox_OK_NOK);
            groupBox3.Controls.Add(txt_AngleValue);
            groupBox3.Controls.Add(lbl_AngleDisplay);
            groupBox3.Controls.Add(btn_SendData);
            groupBox3.Controls.Add(txt_TorqueValue);
            groupBox3.Controls.Add(lbl_TorqueDisplay);
            groupBox3.Location = new Point(19, 115);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(457, 119);
            groupBox3.TabIndex = 25;
            groupBox3.TabStop = false;
            groupBox3.Text = "Data Gönder";
            // 
            // chkBox_OK_NOK
            // 
            chkBox_OK_NOK.AutoSize = true;
            chkBox_OK_NOK.Checked = true;
            chkBox_OK_NOK.CheckState = CheckState.Checked;
            chkBox_OK_NOK.Location = new Point(348, 26);
            chkBox_OK_NOK.Name = "chkBox_OK_NOK";
            chkBox_OK_NOK.Size = new Size(83, 19);
            chkBox_OK_NOK.TabIndex = 25;
            chkBox_OK_NOK.Text = "checkBox1";
            chkBox_OK_NOK.UseVisualStyleBackColor = true;
            chkBox_OK_NOK.CheckedChanged += chkBox_OK_NOK_CheckedChanged;
            // 
            // txt_AngleValue
            // 
            txt_AngleValue.Location = new Point(6, 22);
            txt_AngleValue.MaxLength = 5;
            txt_AngleValue.Name = "txt_AngleValue";
            txt_AngleValue.Size = new Size(132, 23);
            txt_AngleValue.TabIndex = 23;
            txt_AngleValue.KeyPress += txt_AngleValue_KeyPress;
            // 
            // lbl_AngleDisplay
            // 
            lbl_AngleDisplay.AutoSize = true;
            lbl_AngleDisplay.Location = new Point(6, 48);
            lbl_AngleDisplay.Name = "lbl_AngleDisplay";
            lbl_AngleDisplay.Size = new Size(67, 15);
            lbl_AngleDisplay.TabIndex = 22;
            lbl_AngleDisplay.Text = "Açı Değeri :";
            lbl_AngleDisplay.TextChanged += lbl_AngleDisplay_TextChanged;
            // 
            // btn_SendData
            // 
            btn_SendData.Location = new Point(135, 66);
            btn_SendData.Name = "btn_SendData";
            btn_SendData.Size = new Size(92, 27);
            btn_SendData.TabIndex = 19;
            btn_SendData.Text = "Data Gönder";
            btn_SendData.UseVisualStyleBackColor = true;
            btn_SendData.Click += btn_SendData_Click;
            // 
            // txt_TorqueValue
            // 
            txt_TorqueValue.Location = new Point(184, 22);
            txt_TorqueValue.MaxLength = 6;
            txt_TorqueValue.Name = "txt_TorqueValue";
            txt_TorqueValue.Size = new Size(132, 23);
            txt_TorqueValue.TabIndex = 18;
            // 
            // lbl_TorqueDisplay
            // 
            lbl_TorqueDisplay.AutoSize = true;
            lbl_TorqueDisplay.Location = new Point(184, 48);
            lbl_TorqueDisplay.Name = "lbl_TorqueDisplay";
            lbl_TorqueDisplay.Size = new Size(72, 15);
            lbl_TorqueDisplay.TabIndex = 17;
            lbl_TorqueDisplay.Text = "Tork Değeri :";
            lbl_TorqueDisplay.TextChanged += lbl_TorqueDisplay_TextChanged;
            // 
            // btn_PsetSil
            // 
            btn_PsetSil.Location = new Point(159, 325);
            btn_PsetSil.Name = "btn_PsetSil";
            btn_PsetSil.Size = new Size(100, 33);
            btn_PsetSil.TabIndex = 13;
            btn_PsetSil.Text = "Pset Sil";
            btn_PsetSil.UseVisualStyleBackColor = true;
            btn_PsetSil.Click += btn_PsetSil_Click;
            // 
            // btn_PsetEkle
            // 
            btn_PsetEkle.Location = new Point(159, 286);
            btn_PsetEkle.Name = "btn_PsetEkle";
            btn_PsetEkle.Size = new Size(100, 33);
            btn_PsetEkle.TabIndex = 12;
            btn_PsetEkle.Text = "Pset Ekle";
            btn_PsetEkle.UseVisualStyleBackColor = true;
            btn_PsetEkle.Click += btn_PsetEkle_Click;
            // 
            // txt_PsetEkle
            // 
            txt_PsetEkle.Location = new Point(154, 254);
            txt_PsetEkle.Name = "txt_PsetEkle";
            txt_PsetEkle.Size = new Size(128, 23);
            txt_PsetEkle.TabIndex = 11;
            // 
            // listBox_Pset
            // 
            listBox_Pset.FormattingEnabled = true;
            listBox_Pset.ItemHeight = 15;
            listBox_Pset.Location = new Point(19, 249);
            listBox_Pset.Name = "listBox_Pset";
            listBox_Pset.Size = new Size(119, 109);
            listBox_Pset.TabIndex = 10;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btn_LogClear);
            groupBox2.Controls.Add(richTextBox_Log);
            groupBox2.Location = new Point(21, 403);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(524, 273);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Log";
            // 
            // btn_LogClear
            // 
            btn_LogClear.Location = new Point(395, 16);
            btn_LogClear.Name = "btn_LogClear";
            btn_LogClear.Size = new Size(100, 23);
            btn_LogClear.TabIndex = 10;
            btn_LogClear.Text = "Logu Temizle";
            btn_LogClear.UseVisualStyleBackColor = true;
            btn_LogClear.Click += btn_LogClear_Click;
            // 
            // PF8000
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "PF8000";
            Size = new Size(765, 790);
            Load += PF_8000_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Label lbl_ethernetclose;
        private Button btn_ethernetClose;
        private Label lbl_ethernetopen;
        private Button btn_ethernetOpen;
        private RichTextBox richTextBox_Log;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btn_LogClear;
        private Button btn_PsetEkle;
        private TextBox txt_PsetEkle;
        private ListBox listBox_Pset;
        private Button btn_PsetSil;
        private TextBox txt_TorqueValue;
        private Label lbl_TorqueDisplay;
        private Button btn_SendData;
        private GroupBox groupBox3;
        private TextBox txt_AngleValue;
        private Label lbl_AngleDisplay;
        private CheckBox chkBox_OK_NOK;
    }
}
