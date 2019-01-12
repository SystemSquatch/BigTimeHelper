using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace BigTimeHelper
{
    public partial class LoginPage : Form
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private TimeEntering EnterTime = new TimeEntering();
        private SettingsPage Settings = new SettingsPage();

        /// <summary>
        /// When continue button on login page is clicked, users info is sent to EnterTime class
        /// </summary>
        private void Continue_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please enter a username and password");
            }
            else
            {
                EnterTime.SetID(textBox1.Text);
                EnterTime.SetP(textBox2.Text);
                Boolean complete;
                try
                {
                    complete = EnterTime.ExecuteTimeAddition();
                }
                catch
                {
                    complete = false;
                }

                if (complete)
                {
                    Settings.Close();
                    this.Close();
                }
            }

        }

        /// <summary>
        /// When settings button is clicked, settings page opens. if it doesnt exist, create new settings page
        /// </summary>
        private void SettingsButton(object sender, EventArgs e)
        {
            try
            {
                Settings.EnterTime = this.EnterTime;
                Settings.Show();
            }
            catch(Exception)
            {
                SettingsPage Settings = new SettingsPage();
                Settings.EnterTime = this.EnterTime;
                Settings.Show();
            }
        }

    }
}
