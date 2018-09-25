using System;
using System.Diagnostics;
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
            this.Top = Properties.Settings.Default.Top;
            this.Left = Properties.Settings.Default.Left;
            this.Width = Properties.Settings.Default.Width;
            this.Height = Properties.Settings.Default.Height;
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
