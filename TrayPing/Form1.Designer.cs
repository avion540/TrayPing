namespace TrayPing
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pingLabel = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Info_Option = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Settings_Option = new System.Windows.Forms.ToolStripMenuItem();
            this.Show_Option = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit_Option = new System.Windows.Forms.ToolStripMenuItem();
            this.pingUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.userCustomIP4 = new System.Windows.Forms.NumericUpDown();
            this.userCustomIP3 = new System.Windows.Forms.NumericUpDown();
            this.userCustomIP2 = new System.Windows.Forms.NumericUpDown();
            this.userCustomIP1 = new System.Windows.Forms.NumericUpDown();
            this.userCustomIP = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.launchOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userCustomIP4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userCustomIP3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userCustomIP2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userCustomIP1)).BeginInit();
            this.SuspendLayout();
            // 
            // pingLabel
            // 
            this.pingLabel.AutoSize = true;
            this.pingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pingLabel.Location = new System.Drawing.Point(123, 8);
            this.pingLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pingLabel.Name = "pingLabel";
            this.pingLabel.Size = new System.Drawing.Size(152, 31);
            this.pingLabel.TabIndex = 0;
            this.pingLabel.Text = "Checking...";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Checking...";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Info_Option,
            this.checkForUpdateToolStripMenuItem,
            this.toolStripSeparator1,
            this.Settings_Option,
            this.Show_Option,
            this.launchOnStartupToolStripMenuItem,
            this.Exit_Option});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(172, 164);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // Info_Option
            // 
            this.Info_Option.Name = "Info_Option";
            this.Info_Option.Size = new System.Drawing.Size(171, 22);
            this.Info_Option.Text = "Info";
            this.Info_Option.Click += new System.EventHandler(this.Info_Option_Click);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.checkForUpdateToolStripMenuItem.Text = "Check for Update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // Settings_Option
            // 
            this.Settings_Option.Name = "Settings_Option";
            this.Settings_Option.Size = new System.Drawing.Size(171, 22);
            this.Settings_Option.Text = "Settings";
            this.Settings_Option.Click += new System.EventHandler(this.Settings_Option_Click);
            // 
            // Show_Option
            // 
            this.Show_Option.Name = "Show_Option";
            this.Show_Option.Size = new System.Drawing.Size(171, 22);
            this.Show_Option.Text = "Show";
            this.Show_Option.Click += new System.EventHandler(this.Show_Option_Click);
            // 
            // Exit_Option
            // 
            this.Exit_Option.Name = "Exit_Option";
            this.Exit_Option.Size = new System.Drawing.Size(171, 22);
            this.Exit_Option.Text = "Exit";
            this.Exit_Option.Click += new System.EventHandler(this.Exit_Option_Click);
            // 
            // pingUpdateTimer
            // 
            this.pingUpdateTimer.Enabled = true;
            this.pingUpdateTimer.Interval = 1000;
            this.pingUpdateTimer.Tick += new System.EventHandler(this.pingUpdateTimer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.userCustomIP4);
            this.groupBox1.Controls.Add(this.userCustomIP3);
            this.groupBox1.Controls.Add(this.userCustomIP2);
            this.groupBox1.Controls.Add(this.userCustomIP1);
            this.groupBox1.Controls.Add(this.userCustomIP);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(13, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 85);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select server to ping";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // userCustomIP4
            // 
            this.userCustomIP4.Location = new System.Drawing.Point(222, 52);
            this.userCustomIP4.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.userCustomIP4.Name = "userCustomIP4";
            this.userCustomIP4.Size = new System.Drawing.Size(38, 20);
            this.userCustomIP4.TabIndex = 10;
            this.userCustomIP4.ValueChanged += new System.EventHandler(this.userCustomIP4_ValueChanged);
            // 
            // userCustomIP3
            // 
            this.userCustomIP3.Location = new System.Drawing.Point(178, 52);
            this.userCustomIP3.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.userCustomIP3.Name = "userCustomIP3";
            this.userCustomIP3.Size = new System.Drawing.Size(38, 20);
            this.userCustomIP3.TabIndex = 9;
            this.userCustomIP3.ValueChanged += new System.EventHandler(this.userCustomIP3_ValueChanged);
            // 
            // userCustomIP2
            // 
            this.userCustomIP2.Location = new System.Drawing.Point(134, 52);
            this.userCustomIP2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.userCustomIP2.Name = "userCustomIP2";
            this.userCustomIP2.Size = new System.Drawing.Size(38, 20);
            this.userCustomIP2.TabIndex = 8;
            this.userCustomIP2.ValueChanged += new System.EventHandler(this.userCustomIP2_ValueChanged);
            // 
            // userCustomIP1
            // 
            this.userCustomIP1.Location = new System.Drawing.Point(90, 52);
            this.userCustomIP1.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.userCustomIP1.Name = "userCustomIP1";
            this.userCustomIP1.Size = new System.Drawing.Size(38, 20);
            this.userCustomIP1.TabIndex = 7;
            this.userCustomIP1.ValueChanged += new System.EventHandler(this.userCustomIP1_ValueChanged);
            // 
            // userCustomIP
            // 
            this.userCustomIP.AutoSize = true;
            this.userCustomIP.Location = new System.Drawing.Point(11, 52);
            this.userCustomIP.Name = "userCustomIP";
            this.userCustomIP.Size = new System.Drawing.Size(73, 17);
            this.userCustomIP.TabIndex = 2;
            this.userCustomIP.TabStop = true;
            this.userCustomIP.Text = "Custom IP";
            this.userCustomIP.UseVisualStyleBackColor = true;
            this.userCustomIP.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(134, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(135, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "League of Legends NA";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            this.radioButton2.Click += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(11, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(77, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Open DNS";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // launchOnStartupToolStripMenuItem
            // 
            this.launchOnStartupToolStripMenuItem.CheckOnClick = true;
            this.launchOnStartupToolStripMenuItem.Name = "launchOnStartupToolStripMenuItem";
            this.launchOnStartupToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.launchOnStartupToolStripMenuItem.Text = "Launch on Startup";
            this.launchOnStartupToolStripMenuItem.Click += new System.EventHandler(this.launchOnStartupToolStripMenuItem_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(307, 143);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pingLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(323, 182);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrayPing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userCustomIP4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userCustomIP3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userCustomIP2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userCustomIP1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pingLabel;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Show_Option;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Exit_Option;
        private System.Windows.Forms.ToolStripMenuItem Info_Option;
        private System.Windows.Forms.Timer pingUpdateTimer;
        private System.Windows.Forms.ToolStripMenuItem Settings_Option;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton userCustomIP;
        private System.Windows.Forms.NumericUpDown userCustomIP4;
        private System.Windows.Forms.NumericUpDown userCustomIP3;
        private System.Windows.Forms.NumericUpDown userCustomIP2;
        private System.Windows.Forms.NumericUpDown userCustomIP1;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchOnStartupToolStripMenuItem;
    }
}

