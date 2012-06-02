using System.IO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class Log : Form
	{
		public Log()
		{

			InitializeComponent();

		}

        private void timer1_Tick(object sender, EventArgs e)
        {
         
        }
        String logtext;
        private void readlog(string filename)
        {
               try
            {
                if (File.Exists(filename) == true)
                {
                    FileStream fs = new FileStream(@filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader sr = new StreamReader(fs);
                    logtext = sr.ReadToEnd();
                    sr.Close();
                }
                    else
                    {
                        logtext="";
                    }
                
            }
            catch
            {
                // Let the user know what went wrong.
                MessageBox.Show("Logfile(s) could not be read");
            }
        }
        private void rtmpLogtext_TextChanged(object sender, EventArgs e)
        {

        }

   

        private void Log_Load(object sender, EventArgs e)
        {
           
        }

        private void Log_Resize(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            readlog("Command.txt");
            rtmpLogtext.Text = logtext;
            readlog("rtmpsuck-log.txt");
            rtmpsuck_log.Text = logtext;
           
        }
        Boolean update = true;
        private void logupdate_Click(object sender, EventArgs e)
        {
            if (update == true)
            {
                timer1.Enabled = false;
                update = false;
                logupdate.Text = "start update";
            }
            else
            {
                timer1.Enabled = true;
                update = true;
                logupdate.Text = "stop update";
            }
        }
	}
}
