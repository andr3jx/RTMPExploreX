/*
 * Created by SharpDevelop.
 * User: Andrej
 * Date: 30.05.2012
 * Time: 18:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WindowsFormsApplication
{
	partial class Log
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.rtmpLogtext = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.logupdate = new System.Windows.Forms.Button();
            this.rtmpsuck_log = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtmpLogtext
            // 
            this.rtmpLogtext.BackColor = System.Drawing.Color.White;
            this.rtmpLogtext.DetectUrls = false;
            this.rtmpLogtext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtmpLogtext.Location = new System.Drawing.Point(3, 3);
            this.rtmpLogtext.Name = "rtmpLogtext";
            this.rtmpLogtext.ReadOnly = true;
            this.rtmpLogtext.Size = new System.Drawing.Size(504, 296);
            this.rtmpLogtext.TabIndex = 0;
            this.rtmpLogtext.Text = "";
            this.rtmpLogtext.TextChanged += new System.EventHandler(this.rtmpLogtext_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(78, 25);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(518, 335);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtmpLogtext);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(510, 302);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Commands.txt";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtmpsuck_log);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(510, 302);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "rtmpsuck-log.txt";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // logupdate
            // 
            this.logupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logupdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logupdate.Location = new System.Drawing.Point(174, 0);
            this.logupdate.Name = "logupdate";
            this.logupdate.Size = new System.Drawing.Size(73, 26);
            this.logupdate.TabIndex = 3;
            this.logupdate.Text = "stop update";
            this.logupdate.UseVisualStyleBackColor = true;
            this.logupdate.Click += new System.EventHandler(this.logupdate_Click);
            // 
            // rtmpsuck_log
            // 
            this.rtmpsuck_log.DetectUrls = false;
            this.rtmpsuck_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtmpsuck_log.Location = new System.Drawing.Point(3, 3);
            this.rtmpsuck_log.Name = "rtmpsuck_log";
            this.rtmpsuck_log.Size = new System.Drawing.Size(504, 296);
            this.rtmpsuck_log.TabIndex = 0;
            this.rtmpsuck_log.Text = "";
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 335);
            this.Controls.Add(this.logupdate);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Log";
            this.ShowInTaskbar = false;
            this.Text = "rtmpLog";
            this.Load += new System.EventHandler(this.Log_Load);
            this.Resize += new System.EventHandler(this.Log_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
        private System.Windows.Forms.RichTextBox rtmpLogtext;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox rtmpsuck_log;
        private System.Windows.Forms.Button logupdate;
	}
}
