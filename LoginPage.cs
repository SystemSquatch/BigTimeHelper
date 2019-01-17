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
        int debugModeClicks = 0;

        public LoginPage()
        {
            InitializeComponent();
        }

        private TimeEntering EnterTime = new TimeEntering();
        //create settings form
        private SettingsPage Settings = new SettingsPage();

        private void Continue_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please enter a username and password");
            }
            else
            {
                //record values of the users username and password
                EnterTime.SetID(textBox1.Text);
                EnterTime.SetP(textBox2.Text);
                //execute program and close the user settings window
                Boolean complete = EnterTime.ExecuteTimeAddition();
                if (complete)
                {
                    Settings.Close();
                    this.Close();
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //show the window
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

        private void usernameLabel_Click(object sender, EventArgs e)
        {
            debugModeClicks++;

            if (debugModeClicks == 5)
            {
                MessageBox.Show("Debug mode activated");

                // TODO: Implement what happens when debug mode is activated
            }
        }
    }
}
