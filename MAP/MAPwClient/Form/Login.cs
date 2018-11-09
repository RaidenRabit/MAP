using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Forms;
using MAPwClient.Controller;

namespace MAPwClient
{
    public partial class Login : Form
    {
        private UserController _userController;

        public Login()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Width = this.Width;
            Properties.Settings.Default.Height = this.Height;
            Properties.Settings.Default.Save();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Rectangle formRectangle = IsOnScreen();
            this.Top = formRectangle.Top;
            this.Left = formRectangle.Left;
            this.Width = formRectangle.Width;
            this.Height = formRectangle.Height;
        }
        private Rectangle IsOnScreen()
        {
            Rectangle formRectangle = new Rectangle(Properties.Settings.Default.Left, Properties.Settings.Default.Top, Properties.Settings.Default.Width, Properties.Settings.Default.Height);

            if (Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(formRectangle)))
                return formRectangle;
            else
            {
                List<int> defaultTLWH = Properties.Settings.Default.DefaultTLWH.Split(',').Select(int.Parse).ToList();
                return new Rectangle(defaultTLWH[1], defaultTLWH[0], defaultTLWH[2], defaultTLWH[3]);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Left = this.Left;
            Properties.Settings.Default.Top = this.Top;
            Properties.Settings.Default.Save();
        }

        private async void login_btn_Click(object sender, EventArgs e)
        {
            string b;
            var a = await _userController.Login(username_textBox.Text, password_textBox.Text);
            if (a.IsSuccessStatusCode)
                b = a.Content.ReadAsStringAsync().Result;
            else
            {
                b = "failed";
            }
            output_textBox.Text = b;
        }

        private string GetActiveTabUrl()
        {
            Process[] procsChrome = Process.GetProcessesByName("chrome");

            if (procsChrome.Length <= 0)
                return null;

            foreach (Process proc in procsChrome)
            {
                // the chrome process must have a window 
                if (proc.MainWindowHandle == IntPtr.Zero)
                    continue;

                // to find the tabs we first need to locate something reliable - the 'New Tab' button 
                AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
                var SearchBar = root.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));
                if (SearchBar != null)
                    return (string)SearchBar.GetCurrentPropertyValue(ValuePatternIdentifiers.ValueProperty);
            }

            return null;
        }
    }
}
