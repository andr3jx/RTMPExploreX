namespace WindowsFormsApplication
{
    using EasyHook;
    using NativeMethodes;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class Main : Form, IEntryPoint
    {
        public WebBrowser Browser;
        private IContainer components = null;
        private LocalHook CreateConnectHook;
        private IntPtr m_Lib;
        private ToolStrip toolStrip1;
        private StatusStrip statusStrip1;
        private ToolStripButton cmdBack;
        private ToolStripTextBox txtNavigate;
        private ToolStripButton cmdGo;
        private ToolStripButton Monitor_Button;
        private ToolStripComboBox rtmptool;
        private ToolStripButton showLog;
        private ToolStripStatusLabel statuslabel;
        private ToolStripTextBox portconnect_text;
        int toolbarbuttons = 450;
        private ToolStripLabel rtmpport_label;
        private ToolStripLabel monitor_status_label;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator3;
        public static int portconnect = 1935;

        public Main()
        {
            this.InitializeComponent();
        }

        private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            navigated = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BrowserNavigate();
        }
        private void BrowserNavigate()
        {
            navigated = false;
            this.Browser.ScriptErrorsSuppressed = true;
            this.Browser.Navigate(this.txtNavigate.Text);
        }
        private void cmdBack_Click(object sender, EventArgs e)
        {
            if (this.Browser.CanGoBack)
            {
                this.Browser.GoBack();
            }
        }

        public static int portnum = 0x78f;
        private static int connect_Hooked(IntPtr socketHandle, ref NativeSocketMethode.sockaddr name, ref int namelen)
        {
            portnum = Convert.ToInt32(portconnect.ToString("x"), 16);

            lock (typeof(Main))
            {
                if (NativeSocketMethode.ntohs(name.sin_port) == portnum)//0x78f is 1935 in Hex
                {
                    string str = Marshal.PtrToStringAnsi(NativeSocketMethode.inet_ntoa(name.sin_addr)) + ":" + NativeSocketMethode.ntohs(name.sin_port);
                    name.sin_addr.sin_addr[0] = 0x7f;//127 in Hex
                    name.sin_addr.sin_addr[1] = 0;
                    name.sin_addr.sin_addr[2] = 0;
                    name.sin_addr.sin_addr[3] = 1;
                }
                return NativeSocketMethode.connect(socketHandle, ref name, ref namelen);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            portconnect_text.Text = portconnect.ToString();
            this.Browser.Size = new Size(this.Size.Width - 20, this.ClientSize.Height - 50);
            txtNavigate.Size = new Size(this.ClientSize.Width - toolbarbuttons, txtNavigate.Height);
            this.Browser.NewWindow += new CancelEventHandler(this.webBrowser1_NewWindow);
            rtmptool.Items.Add("cmd.exe");
            rtmptool.Items.Add("rtmpsrv");
            rtmptool.Items.Add("rtmpsrv-vlc");
            rtmptool.Items.Add("rtmpsuck");
            rtmptool.SelectedIndex = 0;

            //try
            //{
            //    if (Process.GetProcessesByName("rtmpsrv").Length == 0)
            //    {
            //        new Process { StartInfo = { FileName = "rtmpsrv.exe" } }.Start();
            //    }
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show("Unable to start not rtmpsrv.exe\n Please copy rtmpsrv to rtmpExplorer.exe or start rtmpsrv manual.\n" + exception.Message);
            //}
        }

        private void MonitorRTMP()
        {
            try
            {
                this.m_Lib = NativeAPI.LoadLibrary("Ws2_32.dll");
                this.CreateConnectHook = LocalHook.Create(LocalHook.GetProcAddress("Ws2_32.dll", "connect"), new NativeSocketMethode.DConnect(Main.connect_Hooked), this);
                this.CreateConnectHook.ThreadACL.SetExclusiveACL(new int[1]);
            }
            catch (Exception exception2)
            {
                throw exception2;

            }
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdBack = new System.Windows.Forms.ToolStripButton();
            this.txtNavigate = new System.Windows.Forms.ToolStripTextBox();
            this.cmdGo = new System.Windows.Forms.ToolStripButton();
            this.rtmptool = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.monitor_status_label = new System.Windows.Forms.ToolStripLabel();
            this.Monitor_Button = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rtmpport_label = new System.Windows.Forms.ToolStripLabel();
            this.portconnect_text = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showLog = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Browser
            // 
            this.Browser.Location = new System.Drawing.Point(0, 28);
            this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Browser.Name = "Browser";
            this.Browser.ScriptErrorsSuppressed = true;
            this.Browser.Size = new System.Drawing.Size(391, 195);
            this.Browser.TabIndex = 0;
            this.Browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.Browser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.Browser_Navigated);
            this.Browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.Browser_Navigating);
            this.Browser.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdBack,
            this.txtNavigate,
            this.cmdGo,
            this.rtmptool,
            this.toolStripSeparator2,
            this.monitor_status_label,
            this.Monitor_Button,
            this.toolStripSeparator1,
            this.rtmpport_label,
            this.portconnect_text,
            this.toolStripSeparator3,
            this.showLog});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(997, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdBack
            // 
            this.cmdBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdBack.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBack.Image = ((System.Drawing.Image)(resources.GetObject("cmdBack.Image")));
            this.cmdBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(23, 22);
            this.cmdBack.Text = "◄";
            this.cmdBack.ToolTipText = "Back";
            this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // txtNavigate
            // 
            this.txtNavigate.CausesValidation = false;
            this.txtNavigate.Name = "txtNavigate";
            this.txtNavigate.Size = new System.Drawing.Size(100, 25);
            this.txtNavigate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNavigate_KeyPress);
            // 
            // cmdGo
            // 
            this.cmdGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdGo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGo.Image = ((System.Drawing.Image)(resources.GetObject("cmdGo.Image")));
            this.cmdGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdGo.Name = "cmdGo";
            this.cmdGo.Size = new System.Drawing.Size(23, 22);
            this.cmdGo.Text = "►";
            this.cmdGo.ToolTipText = "Navigate";
            this.cmdGo.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtmptool
            // 
            this.rtmptool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rtmptool.Name = "rtmptool";
            this.rtmptool.Size = new System.Drawing.Size(80, 25);
            this.rtmptool.Click += new System.EventHandler(this.rtmptool_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // monitor_status_label
            // 
            this.monitor_status_label.BackColor = System.Drawing.SystemColors.Control;
            this.monitor_status_label.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.monitor_status_label.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monitor_status_label.ForeColor = System.Drawing.Color.IndianRed;
            this.monitor_status_label.Name = "monitor_status_label";
            this.monitor_status_label.Size = new System.Drawing.Size(16, 22);
            this.monitor_status_label.Text = "█";
            // 
            // Monitor_Button
            // 
            this.Monitor_Button.BackColor = System.Drawing.SystemColors.Control;
            this.Monitor_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Monitor_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Monitor_Button.Name = "Monitor_Button";
            this.Monitor_Button.Size = new System.Drawing.Size(54, 22);
            this.Monitor_Button.Text = "Monitor";
            this.Monitor_Button.ToolTipText = "Reroute streams to rtmpdump";
            this.Monitor_Button.Click += new System.EventHandler(this.MonitorStart_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // rtmpport_label
            // 
            this.rtmpport_label.Name = "rtmpport_label";
            this.rtmpport_label.Size = new System.Drawing.Size(69, 22);
            this.rtmpport_label.Text = "RTMP-Port:";
            // 
            // portconnect_text
            // 
            this.portconnect_text.MaxLength = 5;
            this.portconnect_text.Name = "portconnect_text";
            this.portconnect_text.Size = new System.Drawing.Size(40, 25);
            this.portconnect_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.portconnect_text_KeyPress);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // showLog
            // 
            this.showLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showLog.Image = ((System.Drawing.Image)(resources.GetObject("showLog.Image")));
            this.showLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showLog.Name = "showLog";
            this.showLog.Size = new System.Drawing.Size(31, 22);
            this.showLog.Text = "Log";
            this.showLog.ToolTipText = "Show passed parameters";
            this.showLog.Click += new System.EventHandler(this.showLog_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(997, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statuslabel
            // 
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(39, 17);
            this.statuslabel.Text = "Ready";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 466);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.Browser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "RTMPExploreX";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private static int recv_Hooked(IntPtr socketHandle, IntPtr buf, int count, int socketFlags)
        {
            lock (typeof(Main))
            {
                NativeSocketMethode.sockaddr address = new NativeSocketMethode.sockaddr();
                int namelen = 0x10;
                int num2 = NativeSocketMethode.getpeername(socketHandle, ref address, ref namelen);
                int length = NativeSocketMethode.recv(socketHandle, buf, count, socketFlags);
                string str = address.sin_addr.sin_addr[0].ToString() + "." + address.sin_addr.sin_addr[1].ToString() + "." + address.sin_addr.sin_addr[2].ToString() + "." + address.sin_addr.sin_addr[3].ToString() + ":" + address.sin_port.ToString();
                if (length > 0)
                {
                    byte[] destination = new byte[length];
                    Marshal.Copy(buf, destination, 0, length);
                    string str2 = Encoding.ASCII.GetString(destination);
                }
                return length;
            }
        }

        private static int send_Hooked(IntPtr socketHandle, IntPtr buf, int count, int socketFlags)
        {
            lock (typeof(Main))
            {
                NativeSocketMethode.sockaddr address = new NativeSocketMethode.sockaddr();
                int namelen = 0x10;
                int num2 = NativeSocketMethode.getpeername(socketHandle, ref address, ref namelen);
                string str = Marshal.PtrToStringAnsi(NativeSocketMethode.inet_ntoa(address.sin_addr)) + ":" + NativeSocketMethode.ntohs(address.sin_port);
                byte[] destination = new byte[count];
                Marshal.Copy(buf, destination, 0, count);
                return NativeSocketMethode.send(socketHandle, buf, count, socketFlags);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.OriginalString.Contains(":"))
            {
                this.statuslabel.Text = e.Url.OriginalString;
            }
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Browser.Navigate(this.Browser.StatusText);
        }
        //Namespaces needed

        public void FindAndKillProcess(string name)
        {

            foreach (Process clsProcess in Process.GetProcesses())
            {

                if (clsProcess.ProcessName.StartsWith(name))
                {
                    clsProcess.Kill();

                }
            }

        }

        private void BatchStart(string batch)
        {
            try
            {
                //if (Process.GetProcessesByName("cmd").Length == 0)
                //{
                statuslabel.Text = batch + " started. RtmpExplorer monitors RTMP-Streams now..";
                if (batch == "rtmpsuck.bat")
                {
                    MessageBox.Show("The output of rtmpsuck is saved only in rtmpsuck-log.txt - To see the output click 'Log'. When rtmpsuck finds the parameters, you will be able to copy them easy.");
                }
                System.Diagnostics.Process.Start(batch);
                //}
                //else
                //{
                //    rtmptool.SelectedIndex = 0;
                //    statuslabel.Text="Batch should run in cmd.exe already. RtmpExplorer monitors RTMP-Streams now..";
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show("Unable to start " + batch + " " + exception.Message);
            }

        }
        Boolean monitoring = false;

        private void MonitorStart_Click(object sender, EventArgs e)
        {
            if (monitoring == false)
            {
                portconnect_text.Enabled = false;
                portconnect = Convert.ToInt32(portconnect_text.Text);


                monitoring = true;
                monitor_status_label.ForeColor = Color.LightGreen;
                Monitor_Button.Text = "Deactivate";
                rtmptool.Enabled = false;
                if (portconnect_text.Text != "1935")
                {
                    try
                    {
                        if (Process.GetProcessesByName("portforward").Length == 0)
                        {
                            System.Diagnostics.Process.Start("portforward.exe", portconnect_text.Text + " 127.0.0.1 1935");

                        }
                        else
                        {
                            FindAndKillProcess("portforward");
                            System.Diagnostics.Process.Start("portforward.exe", portconnect_text.Text + " 127.0.0.1 1935");
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Error! Portforward.exe not started! " + exception.Message);

                    }
                }
                else
                {
                    FindAndKillProcess("portforward");
                }
                MonitorRTMP();
                switch (rtmptool.Text)
                {
                    case "cmd.exe":
                        statuslabel.Text = "No batch-files started. RtmpExploreX monitors RTMP-Streams now..";
                        break;
                    case "rtmpsrv":
                        if (Process.GetProcessesByName("rtmpsrv").Length == 0)
                        {
                            BatchStart("rtmpsrv.bat");
                        }
                        else
                        {
                            statuslabel.Text = "rtmpsrv should run already. RtmpExploreX monitors RTMP-Streams now..";
                        }
                        break;
                    case "rtmpsrv-vlc":
                        if (Process.GetProcessesByName("rtmpsrv").Length == 0)
                        {
                            BatchStart("rtmpsrv-vlc.bat");
                        }
                        else
                        {
                            statuslabel.Text = "rtmpsrv-vlc should run already. RtmpExploreX monitors RTMP-Streams now..";
                        }
                        break;
                    case "rtmpsuck":
                        if (Process.GetProcessesByName("rtmpsuck").Length == 0)
                        {
                            BatchStart("rtmpsuck.bat");
                        }
                        else
                        {
                            statuslabel.Text = "rtmpsuck should run already. RtmpExploreX monitors RTMP-Streams now..";
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                monitoring = false;
                unhook();
                Monitor_Button.Text = "Monitor";
                monitor_status_label.ForeColor = Color.IndianRed;
                portconnect_text.Enabled = true;

            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            this.Browser.Size = new Size(this.Size.Width - 20, this.ClientSize.Height - 50);
            txtNavigate.Size = new Size(this.ClientSize.Width - toolbarbuttons, txtNavigate.Height);
        }

        private void txtNavigate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BrowserNavigate();
                e.Handled = true;
            }
        }
        Boolean navigated = false;
        private void Browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {


            if (navigated == false)
            {

                this.txtNavigate.Text = this.Browser.Url.ToString();
                navigated = true;
            }
        }

        private void showLog_Click(object sender, EventArgs e)
        {
            Log f2 = new Log();
            f2.Show();

        }

        private void rtmptool_Click(object sender, EventArgs e)
        {

        }
        private void unhook()
        {
            this.CreateConnectHook.Dispose();
            rtmptool.Enabled = true;
        }





        private void portconnect_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            this.Browser.Size = new Size(this.Size.Width - 20, this.ClientSize.Height - 50);
            txtNavigate.Size = new Size(this.ClientSize.Width - toolbarbuttons, txtNavigate.Height);
        }






    }
}

