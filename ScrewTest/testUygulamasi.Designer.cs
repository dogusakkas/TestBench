namespace ScrewTest
{
    partial class testUygulamasi
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel_main = new Panel();
            panel2 = new Panel();
            groupBox1 = new GroupBox();
            btn_pf8000 = new Button();
            btn_mt6000 = new Button();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel_main
            // 
            panel_main.Location = new Point(12, 80);
            panel_main.Name = "panel_main";
            panel_main.Size = new Size(609, 683);
            panel_main.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(groupBox1);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(609, 62);
            panel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_pf8000);
            groupBox1.Controls.Add(btn_mt6000);
            groupBox1.Location = new Point(3, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(606, 56);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Cihazlar";
            // 
            // btn_pf8000
            // 
            btn_pf8000.Location = new Point(6, 19);
            btn_pf8000.Name = "btn_pf8000";
            btn_pf8000.Size = new Size(81, 31);
            btn_pf8000.TabIndex = 0;
            btn_pf8000.Text = "PF-8000";
            btn_pf8000.UseVisualStyleBackColor = true;
            btn_pf8000.Click += btn_pf8000_Click;
            // 
            // btn_mt6000
            // 
            btn_mt6000.Location = new Point(93, 19);
            btn_mt6000.Name = "btn_mt6000";
            btn_mt6000.Size = new Size(81, 31);
            btn_mt6000.TabIndex = 1;
            btn_mt6000.Text = "MT-6000";
            btn_mt6000.UseVisualStyleBackColor = true;
            btn_mt6000.Click += btn_mt6000_Click;
            // 
            // testUygulamasi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(631, 771);
            Controls.Add(panel2);
            Controls.Add(panel_main);
            Name = "testUygulamasi";
            Text = "Test Uygulaması";
            FormClosing += testUygulamasi_FormClosing;
            panel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_main;
        private Panel panel2;
        private Button btn_mt6000;
        private Button btn_pf8000;
        private GroupBox groupBox1;
    }
}
