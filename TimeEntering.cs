using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Windows.Forms;

namespace BigTimeHelper
{
    public class TimeEntering
    {
        private Boolean useDefaults = true;
        private String ID;
        private String P;
        private Boolean Time;
        private ArrayList days = new ArrayList();

        public void SetID(String textBox)
        {
            this.ID = textBox;
        }

        public void SetP(String textBox)
        {
            this.P = textBox;
        }

        public void SetUserDefaults(Boolean defaultVals)
        {
            this.useDefaults = defaultVals;
        }

        public void SetTime(Boolean time)
        {
            this.Time = time;
        }

        public void SetDays(ArrayList checkedDays)
        {
            this.days = checkedDays;
        }

        public Boolean ExecuteTimeAddition()
        {

            //if user doesnt modify settings, the default 4 hour a day work period will be entered
            if (this.useDefaults == true)
            {
                this.Time = false;
                this.days.Add(1);
                this.days.Add(2);
                this.days.Add(3);
                this.days.Add(6);
                this.days.Add(7);
                this.days.Add(8);
                this.days.Add(9);
                this.days.Add(10);
                this.days.Add(13);
                this.days.Add(14);

            }

            //disables the command window while selenium is being used and creates the chrome instance
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");
            var driver = new ChromeDriver(driverService, options);


            //goes to eservices
            driver.Navigate().GoToUrl("https://www.mnsu.edu/eservices/");
            driver.Navigate().Refresh();

            //navigates user from login, to the add time worked page
            driver.FindElement(By.XPath("//*[@id='techid']")).SendKeys(ID);
            driver.FindElement(By.XPath("//*[@id='pin']")).SendKeys(P);
            driver.FindElement(By.XPath("//*[@id='Submit']")).Click();

            //check if user entered account info correctly
            try
            {
                if (driver.FindElementByXPath("//*[@id='Job']/p[2]/span").Text == "Your credentials did not match our records. If you believe your credentials are correct it is possible that the web service is down for repair; please wait and try again later.")
                {
                    driver.Quit();
                    MessageBox.Show("Invalid username and password");
                    return false;
                }
            }
            catch (Exception) {
            }


            driver.FindElement(By.XPath("//*[@id='accept_tuition']")).Click();
            driver.FindElement(By.XPath("//*[@id='understand_drop']")).Click();
            driver.FindElement(By.XPath("//*[@id='Job']/p[3]/input")).Click();
            //might not always be here
            try
            {
                driver.FindElement(By.XPath("//*[@name = 'Continue']")).Click();
            }
            catch (Exception) {

            }

            //navigates to employment, time worked
            driver.FindElement(By.XPath("//*[@id='app-links']/ul/li[9]/a")).Click();
            driver.FindElement(By.XPath("//*[@id='main']/div[6]/a")).Click();

            int timeIndex;
            //sets the time index of the dropdowns based on whether full time or part time is selected
            if (Time)
            {
                timeIndex = 32; //selects 8 am for a full time day, 8 hours per day
            }
            else
            {
                timeIndex = 16; //selects 4am for part time, or 4 hours per day
            }

            //loop until the end of billing period for as many days that are worked
            foreach (int index in days)
            {

                driver.FindElement(By.XPath("//*[@id='addTime']")).Click();
                driver.FindElement(By.XPath("//*[@id='date']/option[" + index + "]")).Click();
                //sets to midnight
                driver.FindElement(By.XPath("//*[@id='startTime']/option[1]")).Click();
                //sets to 4am or 8am depending on if full time or part time
                driver.FindElement(By.XPath("//*[@id='endTime']/option[" + timeIndex + "]")).Click();
                //adds the time
                driver.FindElement(By.XPath("//*[@id='timeSaveOrAddId']")).Click();

                try
                {
                    driver.FindElement(By.XPath("//*[@id='continueId']")).Click();
                }
                catch (Exception) {
                    continue;
                }


            }


            while (true) {
                Thread.Sleep(2000);
                try {
                    if (driver.FindElementByXPath("//*[@id='content']/div[1]/div/div/div").Text == "Time worked records submitted successfully.")
                    {
                        driver.Quit();
                        break;
                    }
                    else {
                        continue;
                    }
                }
                catch (Exception) {
                    continue;
                }
            }

            return true;
        }
    }
}
