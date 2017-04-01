using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

// Todo:
// Ping Settings reflects previous values, but the correct address is being pinged
// Let user define custom IP's to ping in place of "League of Legends NA"
// Add basic tray text color switch option
// Figure out why "Collection was modified; enumeration operation may not execute" error is thrown after clicking Exit...sometimes.
// TrayPing might be causing Windows to make tray icons invisible. Possibly too many ping calls?

namespace TrayPing
{
    public partial class MainForm : Form
    {
        // The variable used to show the message only once when the program gets closed to the system tray.
        bool showMinMessage = true;

        // Set ip address to localhost to later be replaced with custom IP
        int ip1 = 107;
        int ip2 = 0;
        int ip3 = 0;
        int ip4 = 1;

        int userIP1 = 107;
        int userIP2 = 0;
        int userIP3 = 0;
        int userIP4 = 1;

        int error = 0;
        bool showErrorBalloon = true;
        // Which radio button is pressed? (1 is 0)
        int userRadio = 0;

        //Ping low and mid
        int pingLow = int.Parse(Regex.Match(Properties.Settings.Default.lowIPsetting, @"\d+").Value);
        int pingMid = int.Parse(Regex.Match(Properties.Settings.Default.midIPsetting, @"\d+").Value);

        public MainForm()
        {
            InitializeComponent();
            UpdatePing();

            // Integrate WinSparkle updater
            WinSparkle.win_sparkle_set_appcast_url("https://natechung.me/trayping/appcast.xml");
            // WinSparkle.win_sparkle_set_app_details("Company","App", "Version"); // THIS CALL NOT IMPLEMENTED YET
            WinSparkle.win_sparkle_init();
            
            // Set WinSparkle to check for update every 1 hour
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Avion\\TrayPing\\WinSparkle", true);
            if (myKey != null)
            {
                myKey.SetValue("UpdateInterval", "3600", RegistryValueKind.String);

                myKey.Close();
            }
        }

        // Import WinSparkle dll (make sure it is in the same folder as the application)
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
            public static extern void win_sparkle_set_app_details(String company_name, String app_name, String app_version);
            [DllImport("WinSparkle.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void win_sparkle_set_registry_path(String path);
            [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void win_sparkle_check_update_with_ui();
        }

        // This delegate enables asynchronous calls for setting the text property on a TextBox control.
        delegate void SetTextCallBack(String text);

        // This method demonstrates a pattern for making thread-safe
        // calls on a Windows Forms control. 
        //
        // If the calling thread is different from the thread that
        // created the TextBox control, this method creates a
        // SetTextCallback and calls itself asynchronously using the
        // Invoke method.
        //
        // If the calling thread is the same as the thread that created
        // the TextBox control, the Text property is set directly. 
        private void SetText(String stuff)
        {
            if (this.pingLabel.InvokeRequired)
            {
                SetTextCallBack s = new SetTextCallBack(SetText);
                this.Invoke(s, new object[] { stuff });
            }
            else
            {
                pingLabel.Text = stuff;
            }
        }
        
        // Ping a server
        private void UpdatePing()
        {
            using (Ping pingSender = new Ping()) // Set using to .Dispose Ping
            {
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128, 
                // but change the fragmentation behavior.
                options.DontFragment = true;

                // Create buffer to be transmitted. 
                string data = "aaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                // Use try/catch blocks to prevent unhandled exceptions when TrayPing can't ping a server or when the computer goes to sleep
                try
                {
                    PingReply reply = pingSender.Send(ip1 + "." + ip2 + "." + ip3 + "." + ip4, timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        SetText(reply.RoundtripTime + "ms");
                        notifyIcon1.Text = "Ping: " + reply.RoundtripTime + "";
                        UpdateIcon((int)reply.RoundtripTime);
                        error = 0;
                    }
                    else
                    // Logic error, Error tooltip will never display
                    {
                        if (error >= 10)
                        {
                            if (showErrorBalloon == true)
                            {
                                // pingLabel.Text = "Error";
                                notifyIcon1.ShowBalloonTip(5, "TrayPing",
                                "There was a problem while checking the ping.",
                                ToolTipIcon.Error);
                            }

                            showErrorBalloon = false;
                            UpdateIcon(-1);
                        }
                        error++;
                    }
                    pingSender.Dispose();
                    ((IDisposable)pingSender).Dispose();
                }

                catch (Exception)
                {

                }
            }
        }

        delegate void radioCheckedHandler(CheckBox radioButton2, bool isChecked);
        private void checkRadioChecked(CheckBox radioButton2, bool isChecked)
        {
            MessageBox.Show("test");
            try
            {
                if (radioButton2.InvokeRequired)
                {
                    radioCheckedHandler d = new radioCheckedHandler(checkRadioChecked);
                    this.Invoke(d, new object[] { radioButton2, isChecked });
                }
                else
                {
                    if (userRadio == 0)
                    {
                        radioButton1.Checked = true;
                        MessageBox.Show("radio1 checked");

                    }

                    //radioButton1.Checked = isChecked;
                    if (userRadio == 1)
                    {
                        radioButton2.Checked = true;
                        MessageBox.Show("radio2 checked");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error checking radio");
            }
        }

        // Used to destroy generated bitmaps after use
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        // Create tray icon
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
                graphics.DrawString("E", new Font("Tahoma", 8), Brushes.Red, new PointF(3, 1));
            }
            else if (status >= 0 && status < pingLow)
            {
                graphics.DrawString(statusPing, new Font("Tahoma", 8, FontStyle.Regular), colorLow, new PointF(0, 1));
            }
            else if (status > pingLow && status <= pingMid)
            {
                graphics.DrawString(statusPing, new Font("Tahoma", 8, FontStyle.Regular), colorMedium, new PointF(0, 1));
            }
            else if (status > pingMid)
            {
                graphics.DrawString("H", new Font("Tahoma", 8), colorHigh, new PointF(3, 1));
            }

            // Convert the bitmap with text to an Icon
            IntPtr hicon = bitmap.GetHicon();
            Icon icon = Icon.FromHandle(hicon);

            notifyIcon1.Icon = icon;

            // Dispose used Objects
            graphics.Dispose();
            colorLow.Dispose();
            colorMedium.Dispose();
            colorHigh.Dispose();

            // Destroy the Icon, since the form creates its own copy of the icon.
            DestroyIcon(icon.Handle);
        }

        // Keeps pinging at interval of 1000ms (set in Designer)
        private void pingUpdateTimer_Tick(object sender, EventArgs e)
        {
            Thread t = new Thread(UpdatePing);
            t.Start();
        }

        // What happens when user closes main window
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
            if (e.CloseReason == CloseReason.UserClosing)
            {
                notifyIcon1.Visible = true;
                this.Hide();
                e.Cancel = true;
            }
        }

        // What happens when user double clicks taskbar icon
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Unhide the main window and show it in the taskbar.
            Visible = true;
            ShowInTaskbar = true;

            // Set the window state to normal (Brings it up from minimized)
            this.WindowState = FormWindowState.Normal;
        }

        // Brings main window up when user clicks "Show" in the right-click menu
        private void Show_Option_Click(object sender, EventArgs e)
        {
            // Unhide the main window and show it in the taskbar.
            Visible = true;
            ShowInTaskbar = true;

            // Set the window state to normal (Brings it up from minimized)
            this.WindowState = FormWindowState.Normal;
        }

        // What happens when user right clicks tray icon then, Exit
        private void Exit_Option_Click(object sender, EventArgs e)
        {
            string pingLowString = pingLow.ToString();
            string pingMidString = pingMid.ToString();
            // Save user radio selection and window location
            Properties.Settings.Default["Location"] = this.Location;
            Properties.Settings.Default["userRadio1"] = radioButton1.Checked;
            Properties.Settings.Default["userRadio2"] = radioButton2.Checked;
            Properties.Settings.Default["lowIPsetting"] = pingLowString;//lowIPsetting;
            Properties.Settings.Default["midIPsetting"] = pingMidString; // midIPsetting;
            Properties.Settings.Default.Save();

            // Remove the icon from the system tray.
            notifyIcon1.Dispose();
            WinSparkle.win_sparkle_cleanup();

            // Close Application
            Environment.Exit(1);
            // Close the main Frame
            Application.Exit();
        }

        // Right click tray, Info
        private void Info_Option_Click(object sender, EventArgs e)
        {
            // Get TrayPing version
            string version = Application.ProductVersion;

            // This is the text for the information popup.
            MessageBox.Show("Version " + version
            + "\n"
            + "\n"
            + "\nPing will not be 100% accurate."
            + "\nOrange M means your ping is > " +pingLow 
            + "\nRed H means your ping is > " + pingMid 
            + "\nRed E means there was an error while trying to ping.\n");
        }

        // Right click tray, Options
        private void Settings_Option_Click(object sender, EventArgs e)
        {
            int low = int.Parse(Regex.Match(Properties.Settings.Default.lowIPsetting, @"\d+").Value);
            int mid = int.Parse(Regex.Match(Properties.Settings.Default.midIPsetting, @"\d+").Value);

            if (InputBox(ref low, ref mid) == DialogResult.OK)
            {
                MessageBox.Show("Settings have been changed.\n"
                    + "Low(green) is now <= " + low + "\n"
                    + "Mid(orange) is now " + low + " < orange <= " + mid + "\n"
                    + "And high(red) ping will be anything above " + mid + ".");

                pingLow = low;
                pingMid = mid;
            }
        }

        private void txtType1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }

        // Results of user changing Options
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
            textBoxLow.Text = Properties.Settings.Default.lowIPsetting;

            labelMid.Text = "Mid";
            textBoxMid.Text = Properties.Settings.Default.midIPsetting;

            labelExample.Text = "Set custom ping ranges.\ngreen <= orange <= red";

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
            // If user did not change values, simply exit form
            if (textBoxLow.Text == Properties.Settings.Default.lowIPsetting && textBoxMid.Text == Properties.Settings.Default.midIPsetting) 
            {
                return dialogResult;
            }
            // If user changes both values
            else if (textBoxLow.Text != Properties.Settings.Default.lowIPsetting && textBoxMid.Text != Properties.Settings.Default.midIPsetting)
            {
                try
                {
                    low = int.Parse(Regex.Match(Properties.Settings.Default.lowIPsetting, @"\d+").Value);
                    mid = int.Parse(Regex.Match(Properties.Settings.Default.midIPsetting, @"\d+").Value);

                    Properties.Settings.Default.lowIPsetting = textBoxLow.Text;
                    Properties.Settings.Default.midIPsetting = textBoxMid.Text;
                    Properties.Settings.Default.Save();
                }
                catch (Exception e)
                {
                    MessageBox.Show("You have done the unspeakable!\n" + e);
                    low = 80;
                    mid = 100;
                }
            }
            // If user changes only low value
            else if (textBoxLow.Text != Properties.Settings.Default.lowIPsetting)
            {
                try
                {
                    low = int.Parse(Regex.Match(Properties.Settings.Default.lowIPsetting, @"\d+").Value);

                    Properties.Settings.Default.lowIPsetting = textBoxLow.Text;
                    Properties.Settings.Default.Save();
                }
                catch (Exception e)
                {
                    MessageBox.Show("You have done the unspeakable!\n" + e);
                    low = 80;
                    mid = 100;
                }
            }
            // If user changes only mid value
            else if (textBoxMid.Text != Properties.Settings.Default.midIPsetting)
            {
                try
                {
                    mid = int.Parse(Regex.Match(Properties.Settings.Default.midIPsetting, @"\d+").Value);
                    //mid = int.Parse(Regex.Replace(pingMidString, "[^0-9]+", string.Empty));

                    Properties.Settings.Default.midIPsetting = textBoxMid.Text;
                    Properties.Settings.Default.Save();
                }
                catch (Exception e)
                {
                    MessageBox.Show("You have done the unspeakable!\n" + e);
                    low = 80;
                    mid = 100;
                }
            }

            /*else
            {
                MessageBox.Show("Both settings must be changed at the same time.\n"
                + "Reverting back to previous values.");
            }*/
            return dialogResult;
        }



        // Do not remove the classes below by hand. They are controlled by Design. Use Designer to add/remove more functions

        // Main application window
        private void MainForm_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = Properties.Settings.Default.userRadio1;
            radioButton2.Checked = Properties.Settings.Default.userRadio2;

            pingLow = int.Parse(Regex.Match(Properties.Settings.Default.lowIPsetting, @"\d+").Value);
            pingMid = int.Parse(Regex.Match(Properties.Settings.Default.midIPsetting, @"\d+").Value);
        }

        // Radio button 1
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ip1 = 208;
            ip2 = 67;
            ip3 = 222;
            ip4 = 222;
        }

        // Radio button 2
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ip1 = 104;
            ip2 = 160;
            ip3 = 131;
            ip4 = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ip1 = userIP1;
            ip2 = userIP2;
            ip3 = userIP3;
            ip4 = userIP4;
        }

        private void userCustomIP1_ValueChanged(object sender, EventArgs e)
        {
            userIP1 = (int)userCustomIP1.Value;
        }

        private void userCustomIP2_ValueChanged(object sender, EventArgs e)
        {
            userIP2 = (int)userCustomIP2.Value;
        }

        private void userCustomIP3_ValueChanged(object sender, EventArgs e)
        {
            userIP3 = (int)userCustomIP3.Value;
        }

        private void userCustomIP4_ValueChanged(object sender, EventArgs e)
        {
            userIP4 = (int)userCustomIP4.Value;
        }

        // groupBox where radio buttons 1 & 2 are housed
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        // Call winsparkle update process with UI
        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinSparkle.win_sparkle_check_update_with_ui();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        
        // Launch on Startup menu item
        private void launchOnStartupToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (launchOnStartupToolStripMenuItem.Checked)
            {
                key.SetValue("TrayPing", Application.ExecutablePath.ToString());
            }
            else
            {
                key.DeleteValue("TrayPing", false);
            }
            key.Close();
        }
    }
}
