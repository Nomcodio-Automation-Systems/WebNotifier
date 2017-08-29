namespace WebNotifier
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                webi.Dispose();
                // thread_web.Abort();
              
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button5 = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.browserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.websocketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waitTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuWaitTime3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuWaitTime5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuWaitTime10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuWaitTime15 = new System.Windows.Forms.ToolStripMenuItem();
            this.maxTriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuMaxTries3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuMaxTries5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuMaxTries10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuMaxTries20 = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreCharsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuIgnoreChars0 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuIgnoreChars50 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuIgnoreChars100 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuIgnoreChars150 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuIgnoreChars200 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuIgnoreChars250 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuIgnoreChars500 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(23, 272);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(442, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(140, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Watch List:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = " WebNotifier";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(231, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Wait in minutes:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(367, 27);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(83, 20);
            this.textBox6.TabIndex = 9;
            this.textBox6.Text = "5";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(369, 71);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(61, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Hide IE";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(369, 94);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(98, 17);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "Hide Programm";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(23, 371);
            this.listBox1.MultiColumn = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(442, 173);
            this.listBox1.TabIndex = 12;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 337);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "Missed changes so far:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(23, 298);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(104, 298);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "Delete";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.Location = new System.Drawing.Point(23, 136);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(442, 108);
            this.listBox2.TabIndex = 16;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            this.listBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox2_MouseDown);
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.Location = new System.Drawing.Point(12, 565);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(174, 51);
            this.button5.TabIndex = 17;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(369, 119);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(73, 17);
            this.checkBox3.TabIndex = 18;
            this.checkBox3.Text = "NoPopUp";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browserToolStripMenuItem,
            this.waitTimeToolStripMenuItem,
            this.maxTriesToolStripMenuItem,
            this.ignoreCharsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 92);
            // 
            // browserToolStripMenuItem
            // 
            this.browserToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.webBrowserToolStripMenuItem,
            this.websocketToolStripMenuItem});
            this.browserToolStripMenuItem.Name = "browserToolStripMenuItem";
            this.browserToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.browserToolStripMenuItem.Text = "Browser";
            // 
            // webBrowserToolStripMenuItem
            // 
            this.webBrowserToolStripMenuItem.Name = "webBrowserToolStripMenuItem";
            this.webBrowserToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.webBrowserToolStripMenuItem.Tag = "WebBrowser";
            this.webBrowserToolStripMenuItem.Text = "WebBrowser";
            this.webBrowserToolStripMenuItem.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // websocketToolStripMenuItem
            // 
            this.websocketToolStripMenuItem.Name = "websocketToolStripMenuItem";
            this.websocketToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.websocketToolStripMenuItem.Tag = "WebSocket";
            this.websocketToolStripMenuItem.Text = "WebSocket";
            this.websocketToolStripMenuItem.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // waitTimeToolStripMenuItem
            // 
            this.waitTimeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuWaitTime3,
            this.toolStripMenuWaitTime5,
            this.toolStripMenuWaitTime10,
            this.toolStripMenuWaitTime15});
            this.waitTimeToolStripMenuItem.Name = "waitTimeToolStripMenuItem";
            this.waitTimeToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.waitTimeToolStripMenuItem.Text = "WaitTime";
            // 
            // toolStripMenuWaitTime3
            // 
            this.toolStripMenuWaitTime3.Name = "toolStripMenuWaitTime3";
            this.toolStripMenuWaitTime3.Size = new System.Drawing.Size(86, 22);
            this.toolStripMenuWaitTime3.Tag = "WaitTime3";
            this.toolStripMenuWaitTime3.Text = "3";
            this.toolStripMenuWaitTime3.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // toolStripMenuWaitTime5
            // 
            this.toolStripMenuWaitTime5.Name = "toolStripMenuWaitTime5";
            this.toolStripMenuWaitTime5.Size = new System.Drawing.Size(86, 22);
            this.toolStripMenuWaitTime5.Tag = "WaitTime5";
            this.toolStripMenuWaitTime5.Text = "5";
            this.toolStripMenuWaitTime5.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // toolStripMenuWaitTime10
            // 
            this.toolStripMenuWaitTime10.Name = "toolStripMenuWaitTime10";
            this.toolStripMenuWaitTime10.Size = new System.Drawing.Size(86, 22);
            this.toolStripMenuWaitTime10.Tag = "WaitTime10";
            this.toolStripMenuWaitTime10.Text = "10";
            this.toolStripMenuWaitTime10.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // toolStripMenuWaitTime15
            // 
            this.toolStripMenuWaitTime15.Name = "toolStripMenuWaitTime15";
            this.toolStripMenuWaitTime15.Size = new System.Drawing.Size(86, 22);
            this.toolStripMenuWaitTime15.Tag = "WaitTime15";
            this.toolStripMenuWaitTime15.Text = "15";
            this.toolStripMenuWaitTime15.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // maxTriesToolStripMenuItem
            // 
            this.maxTriesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuMaxTries3,
            this.toolStripMenuMaxTries5,
            this.toolStripMenuMaxTries10,
            this.toolStripMenuMaxTries20});
            this.maxTriesToolStripMenuItem.Name = "maxTriesToolStripMenuItem";
            this.maxTriesToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.maxTriesToolStripMenuItem.Text = "MaxTries";
            // 
            // toolStripMenuMaxTries3
            // 
            this.toolStripMenuMaxTries3.Name = "toolStripMenuMaxTries3";
            this.toolStripMenuMaxTries3.Size = new System.Drawing.Size(86, 22);
            this.toolStripMenuMaxTries3.Tag = "MaxTries3";
            this.toolStripMenuMaxTries3.Text = "3";
            this.toolStripMenuMaxTries3.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // toolStripMenuMaxTries5
            // 
            this.toolStripMenuMaxTries5.Name = "toolStripMenuMaxTries5";
            this.toolStripMenuMaxTries5.Size = new System.Drawing.Size(86, 22);
            this.toolStripMenuMaxTries5.Tag = "MaxTries5";
            this.toolStripMenuMaxTries5.Text = "5";
            this.toolStripMenuMaxTries5.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // toolStripMenuMaxTries10
            // 
            this.toolStripMenuMaxTries10.Name = "toolStripMenuMaxTries10";
            this.toolStripMenuMaxTries10.Size = new System.Drawing.Size(86, 22);
            this.toolStripMenuMaxTries10.Tag = "MaxTries10";
            this.toolStripMenuMaxTries10.Text = "10";
            this.toolStripMenuMaxTries10.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // toolStripMenuMaxTries20
            // 
            this.toolStripMenuMaxTries20.Name = "toolStripMenuMaxTries20";
            this.toolStripMenuMaxTries20.Size = new System.Drawing.Size(86, 22);
            this.toolStripMenuMaxTries20.Tag = "MaxTries20";
            this.toolStripMenuMaxTries20.Text = "20";
            this.toolStripMenuMaxTries20.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // ignoreCharsToolStripMenuItem
            // 
            this.ignoreCharsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuIgnoreChars0,
            this.toolStripMenuIgnoreChars50,
            this.toolStripMenuIgnoreChars100,
            this.toolStripMenuIgnoreChars150,
            this.toolStripMenuIgnoreChars200,
            this.toolStripMenuIgnoreChars250,
            this.toolStripMenuIgnoreChars500});
            this.ignoreCharsToolStripMenuItem.Name = "ignoreCharsToolStripMenuItem";
            this.ignoreCharsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.ignoreCharsToolStripMenuItem.Text = "IgnoreChars";
            // 
            // toolStripMenuIgnoreChars0
            // 
            this.toolStripMenuIgnoreChars0.Name = "toolStripMenuIgnoreChars0";
            this.toolStripMenuIgnoreChars0.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuIgnoreChars0.Tag = "IgnoreChars0";
            this.toolStripMenuIgnoreChars0.Text = "0";
            // 
            // toolStripMenuIgnoreChars50
            // 
            this.toolStripMenuIgnoreChars50.Name = "toolStripMenuIgnoreChars50";
            this.toolStripMenuIgnoreChars50.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuIgnoreChars50.Tag = "IgnoreChars50";
            this.toolStripMenuIgnoreChars50.Text = "50";
            this.toolStripMenuIgnoreChars50.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // toolStripMenuIgnoreChars100
            // 
            this.toolStripMenuIgnoreChars100.Name = "toolStripMenuIgnoreChars100";
            this.toolStripMenuIgnoreChars100.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuIgnoreChars100.Tag = "IgnoreChars100";
            this.toolStripMenuIgnoreChars100.Text = "100";
            this.toolStripMenuIgnoreChars100.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // toolStripMenuIgnoreChars150
            // 
            this.toolStripMenuIgnoreChars150.Name = "toolStripMenuIgnoreChars150";
            this.toolStripMenuIgnoreChars150.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuIgnoreChars150.Tag = "IgnoreChars150";
            this.toolStripMenuIgnoreChars150.Text = "150";
            this.toolStripMenuIgnoreChars150.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // toolStripMenuIgnoreChars200
            // 
            this.toolStripMenuIgnoreChars200.Name = "toolStripMenuIgnoreChars200";
            this.toolStripMenuIgnoreChars200.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuIgnoreChars200.Tag = "IgnoreChars200";
            this.toolStripMenuIgnoreChars200.Text = "200";
            this.toolStripMenuIgnoreChars200.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // toolStripMenuIgnoreChars250
            // 
            this.toolStripMenuIgnoreChars250.Name = "toolStripMenuIgnoreChars250";
            this.toolStripMenuIgnoreChars250.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuIgnoreChars250.Tag = "IgnoreChars250";
            this.toolStripMenuIgnoreChars250.Text = "250";
            // 
            // toolStripMenuIgnoreChars500
            // 
            this.toolStripMenuIgnoreChars500.Name = "toolStripMenuIgnoreChars500";
            this.toolStripMenuIgnoreChars500.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuIgnoreChars500.Tag = "IgnoreChars500";
            this.toolStripMenuIgnoreChars500.Text = "500";
            this.toolStripMenuIgnoreChars500.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 628);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "WebNotifier          V.0.0.1.5823";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox6;
        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        public System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button5;
        public System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem browserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem webBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem websocketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waitTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuWaitTime3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuWaitTime5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuWaitTime10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuWaitTime15;
        private System.Windows.Forms.ToolStripMenuItem maxTriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuMaxTries3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuMaxTries5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuMaxTries10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuMaxTries20;
        private System.Windows.Forms.ToolStripMenuItem ignoreCharsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuIgnoreChars50;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuIgnoreChars100;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuIgnoreChars150;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuIgnoreChars200;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuIgnoreChars500;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuIgnoreChars0;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuIgnoreChars250;
    }
}

