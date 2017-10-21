namespace WindowsFormsProject
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.wpfHost = new System.Windows.Forms.Integration.ElementHost();
            this.but_generate = new System.Windows.Forms.Button();
            this.but_reload = new System.Windows.Forms.Button();
            this.txtFSMTable = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.txtString = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // wpfHost
            // 
            this.wpfHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wpfHost.BackColor = System.Drawing.Color.White;
            this.wpfHost.Location = new System.Drawing.Point(0, 0);
            this.wpfHost.Name = "wpfHost";
            this.wpfHost.Size = new System.Drawing.Size(812, 448);
            this.wpfHost.TabIndex = 0;
            this.wpfHost.Text = "elementHost1";
            this.wpfHost.Child = null;
            // 
            // but_generate
            // 
            this.but_generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.but_generate.Image = ((System.Drawing.Image)(resources.GetObject("but_generate.Image")));
            this.but_generate.Location = new System.Drawing.Point(818, 0);
            this.but_generate.Name = "but_generate";
            this.but_generate.Size = new System.Drawing.Size(65, 65);
            this.but_generate.TabIndex = 1;
            this.but_generate.UseVisualStyleBackColor = true;
            this.but_generate.Click += new System.EventHandler(this.but_generate_Click);
            // 
            // but_reload
            // 
            this.but_reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.but_reload.Image = ((System.Drawing.Image)(resources.GetObject("but_reload.Image")));
            this.but_reload.Location = new System.Drawing.Point(818, 71);
            this.but_reload.Name = "but_reload";
            this.but_reload.Size = new System.Drawing.Size(65, 65);
            this.but_reload.TabIndex = 2;
            this.but_reload.UseVisualStyleBackColor = true;
            this.but_reload.Click += new System.EventHandler(this.but_reload_Click);
            // 
            // txtFSMTable
            // 
            this.txtFSMTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFSMTable.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFSMTable.Location = new System.Drawing.Point(12, 454);
            this.txtFSMTable.Multiline = true;
            this.txtFSMTable.Name = "txtFSMTable";
            this.txtFSMTable.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFSMTable.Size = new System.Drawing.Size(252, 198);
            this.txtFSMTable.TabIndex = 4;
            this.txtFSMTable.Text = "r0: x2-r5, x3-r1, x4-r3\r\nr1: x1-r2\r\nr2: x3-r3, x7-r3\r\nr3: x1-r10, x5-r4\r\nr4: x6-r" +
    "10, x0-r0\r\nr5: x2-r9, x5-r6, x7-r6\r\nr6: x4-r7\r\nr7: x6-r8\r\nr8: x0-r10\r\nr9: x0-r8\r" +
    "\nr10:";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(270, 454);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(148, 24);
            this.button4.TabIndex = 7;
            this.button4.Text = "parse rules";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtString
            // 
            this.txtString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtString.Location = new System.Drawing.Point(459, 458);
            this.txtString.Name = "txtString";
            this.txtString.Size = new System.Drawing.Size(210, 20);
            this.txtString.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(675, 458);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 24);
            this.button1.TabIndex = 9;
            this.button1.Text = "Parse string...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLog.Location = new System.Drawing.Point(459, 495);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(333, 157);
            this.txtLog.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 664);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtString);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txtFSMTable);
            this.Controls.Add(this.but_reload);
            this.Controls.Add(this.but_generate);
            this.Controls.Add(this.wpfHost);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GraphX WF Interop Sample Project v1.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost wpfHost;
        private System.Windows.Forms.Button but_generate;
        private System.Windows.Forms.Button but_reload;
        private System.Windows.Forms.TextBox txtFSMTable;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtString;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtLog;
    }
}

