using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.InteropServices;

// Todo:
// Figure out why windows task bar is freezing when closing app to tray
// Integrate Sparkle to auto update
// Add basic tray text color switch option
// Add option to enter custom IP to ping

namespace TrayPing
{
    public partial class MainForm : Form
    {
        // The variable used to show the message only once when the program gets closed to the system tray.
        Boolean showMinMessage = true;

        int ip1 = 8;
        int ip2 = 8;
        int ip3 = 8;
        int ip4 = 8;

        int error = 0;
        Boolean showErrorBalloon = true;

        //Ping low and mid
        int pingLow = 80;
        int pingMid = 100;

        public MainForm()
        {
            InitializeComponent();
            UpdatePing();

            // Attempting to implement Sparkle updater
            WinSparkle.win_sparkle_set_appcast_url("https://natechung.me/trayping/appcast.xml");
            //WinSparkle.win_sparkle_set_app_details("Company","App", "Version"); // THIS CALL NOT IMPLEMENTED YET
            WinSparkle.win_sparkle_init();
            WinSparkle.win_sparkle_check_update_with_ui();
        }

        class WinSparkle
        {
            // Note that some of these functions are not implemented by WinSparkle YET.
            [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void win_sparkle_init();
            [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void win_sparkle_cleanup();
            [DllImport("WinSparkle.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void win_sparkle_set_appcast_url(String url);
            [DllImport("WinSparkle.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern void win_sparkle_set_app_details(String company_name,
                                                 String app_name,
                                                 String app_version);
            [DllImport("WinSparkle.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void win_sparkle_set_registry_path(String path);
            [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void win_sparkle_check_update_with_ui();
        }

        delegate void SetTextCallBack(String text);

        private void SetText(String stuff)
        {
            if (this.pingLabel.InvokeRequired)
            {
                SetTextCallBack s = new SetTextCallBack(SetText);
                this.Invoke(s, new object[] { stuff });
            }
            else
            {
                this.pingLabel.Text = stuff;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ip1 = 8;
                ip2 = 8;
                ip3 = 8;
                ip4 = 8;
            }
            else if (radioButton2.Checked)
            {
                ip1 = 104;
                ip2 = 160;
                ip3 = 131;
                ip4 = 1;
            }
        }

        private void UpdatePing()
        {
            using (Ping pingSender = new Ping()) // Set using to .Dispose Ping
            {
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128, 
                // but change the fragmentation behavior.
                options.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted. 
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = pingSender.Send(ip1 + "." + ip2 + "." + ip3 + "." + ip4, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    SetText(reply.RoundtripTime + "ms");
                    notifyIcon1.Text = "Ping: " + reply.RoundtripTime + "";
                    UpdateIcon((int)reply.RoundtripTime);
                    error = 0;
                }
                else
                {
                    if (error >= 10)
                    {
                        if (showErrorBalloon == true)
                        {
                            //pingLabel.Text = "Error";
                            notifyIcon1.ShowBalloonTip(5, "TrayPing",
                            "There was a problem while checking the ping.",
                            ToolTipIcon.Error);
                        }

                        showErrorBalloon = false;
                        UpdateIcon(-1);
                    }
                    error++;
                }
            }
        }

        public void UpdateIcon(int status)
        {
            string statusPing = status.ToString();

            // Create a bitmap and draw text on it
            Bitmap bitmap = new Bitmap(16, 16);
            Graphics graphics = Graphics.FromImage(bitmap);
            SolidBrush colorLow = new SolidBrush(Color.LimeGreen);
            SolidBrush colorMedium = new SolidBrush(Color.Orange);
            SolidBrush colorHigh = new SolidBrush(Color.Red);

            // If the status is -1 that means there was an error trying to get the ping.
            if (status == -1)
            {
                graphics.DrawString("E", new Font("Tahoma", 8), Brushes.Red, new PointF(0, 1));
            }
            else if (status >= 0 && status < pingLow)
            {
                graphics.DrawString(statusPing, new Font("Tahoma", 8, FontStyle.Regular), colorLow, new PointF(0, 1));
            }
            else if (status > pingLow && status <= pingMid)
            {
                graphics.DrawString(statusPing, new Font("Tahoma", 9, FontStyle.Regular), colorMedium, new PointF(0, 1));
            }
            else if (status > pingMid)
            {
                graphics.DrawString("H", new Font("Tahoma", 8), colorHigh, new PointF(0, 1));
            }

            // Convert the bitmap with text to an Icon
            IntPtr hIcon = bitmap.GetHicon();
            Icon icon = Icon.FromHandle(bitmap.GetHicon()); //hIcon);

            // Dispose used Objects
            //
            graphics.Dispose();
            colorLow.Dispose();
            colorMedium.Dispose();
            colorHigh.Dispose();
            //ImageGraphics.Dispose();

            notifyIcon1.Icon = icon;
        }

        private void pingUpdateTimer_Tick(object sender, EventArgs e)
        {
            Thread t = new Thread(UpdatePing);
            t.Start();
            //UpdatePing();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hide the main window and hide it from the taskbar.
            Visible = false;
            ShowInTaskbar = false;

            //Show this only once when the program first starts. The variable is up at the top.
            if (showMinMessage == true)
            {
                notifyIcon1.ShowBalloonTip(5, "TrayPing",
                "This program has been minimized to the system tray.\n" +
                "To return it to it's normal state right click on the tray icon and click Show.",
                ToolTipIcon.Info);

                //Don't show the message again.
                showMinMessage = false;
            }

            // e is the form closing event. e.cancel means it's canceling the form closing.
            e.Cancel = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Unhide the main window and show it in the taskbar.
            Visible = true;
            ShowInTaskbar = true;

            // Set the window state to normal (Brings it up from minimized)
            this.WindowState = FormWindowState.Normal;
        }

        private void Show_Option_Click(object sender, EventArgs e)
        {
            // Unhide the main window and show it in the taskbar.
            Visible = true;
            ShowInTaskbar = true;

            // Set the window state to normal (Brings it up from minimized)
            this.WindowState = FormWindowState.Normal;
        }

        private void Exit_Option_Click(object sender, EventArgs e)
        {
            //Remove the icon from the system tray.
            notifyIcon1.Dispose();
            WinSparkle.win_sparkle_cleanup();
            //Close the main Frame
            Application.Exit();
            
        }

        private void Info_Option_Click(object sender, EventArgs e)
        {
            //This is the text for the information popup.
            MessageBox.Show("Ping will not be 100% accurate."
            //+ "\nGreen L means your ping is <= "+pingLow+"\n"
            + "\nOrange M means your ping is > 95ms" //+ pingLow + " && <= " + pingMid + "\n"
            + "\nRed H means your ping is > 120ms" //+ pingMid + "\n"
            + "\n\nErrors\nRed E means there was an error while trying to ping.\n");
            //+ "The only error I've run into is where a firewall blocks the pings.\n"
            //+ "If you're at a school it might not work correctly.");
        }

        private void Settings_Option_Click(object sender, EventArgs e)
        {
            int low = 80;
            int mid = 100;
            if (InputBox(ref low, ref mid) == DialogResult.OK)
            {
                MessageBox.Show("Settings have been changed.\n"
                    + "Low(green) is now <= " + low + "\n"
                    + "Mid(orange) is now " + low + "< orange <= " + mid + "\n"
                    + "And high(red) ping will be anything above " + mid + ".");

                pingLow = low;
                pingMid = mid;
            }
        }

        public static DialogResult InputBox(ref int low, ref int mid)
        {
            Form form = new Form();
            Label labelLow = new Label();
            TextBox textBoxLow = new TextBox();
            Label labelMid = new Label();
            TextBox textBoxMid = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            Label labelExample = new Label();

            form.Text = "Set Ping";
            labelLow.Text = "Low";
            textBoxLow.Text = "";

            labelMid.Text = "Mid";
            textBoxMid.Text = "";

            labelExample.Text = "green <= orange <= red";

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            labelExample.SetBounds(9, 10, 372, 30);

            labelLow.SetBounds(9, 50, 372, 13);
            textBoxLow.SetBounds(12, 66, 372, 20);

            labelMid.SetBounds(9, 100, 372, 13);
            textBoxMid.SetBounds(12, 116, 372, 20);

            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            labelLow.AutoSize = true;
            labelMid.AutoSize = true;


            textBoxLow.Anchor = textBoxLow.Anchor | AnchorStyles.Right;
            textBoxMid.Anchor = textBoxMid.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { labelExample, labelLow, textBoxLow, labelMid, textBoxMid, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, labelLow.Right + 10), form.ClientSize.Height + 100);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            if (textBoxLow.Text != "" && textBoxMid.Text != "")
            {
                try
                {
                    low = int.Parse(textBoxLow.Text);
                    mid = int.Parse(textBoxMid.Text);
                }
                catch (Exception e)
                {
                    MessageBox.Show("You have done the unspeakable!\n" + e);
                    low = 90;
                    mid = 150;
                }
            }
            else
            {
                MessageBox.Show("Both settings must be changed at the same time.\n"
                + "Reverting back to defaults.");
            }
            return dialogResult;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ip1 = 8;
            ip2 = 8;
            ip3 = 8;
            ip4 = 8;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ip1 = 104;
            ip2 = 160;
            ip3 = 131;
            ip4 = 1;
        }



        private void launchOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
    }
}
