using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigTimeHelper
{
    public partial class SettingsPage : Form
    {

        public TimeEntering EnterTime;
        private DateTime startOfPayPeriod;
        private DateTime endOfPayPeriod;
        private CheckBox[] checkBoxes;
        private int[] dayNumbers;

        public SettingsPage()
        {
            InitializeComponent();

            //creates the arrays for all checkboxes and their corresponding dropdown indexes
            checkBoxes = new CheckBox[] { CB1, CB2, CB3, CB6, CB7, CB8, CB9, CB10, CB13, CB14 };
            dayNumbers = new int[] { 1, 2, 3, 6, 7, 8, 9, 10, 13, 14 };

            CalculateCurrentPayPeriod();
            AddDateToDayCheckboxes();
        }

        //when enter settings is clicked
        private void enterSettings_Click(object sender, EventArgs e)
        {
            ArrayList days = new ArrayList();
            Boolean Time = new Boolean();
            EnterTime.SetUserDefaults(false);

            //read the users selection in the dropdown. if not selected set to part time
            try
            {
                if (comboBox1.SelectedItem.ToString().Equals("Full Time"))
                {
                    Time = true;
                }
                else
                {
                    Time = false;
                }
            }
            catch(Exception)
            {
                Time = false;
            }

            //record the checked boxes and add the drop down indexes to an array
            for(int i = 0; i < checkBoxes.Length; i++)
            {
                if(checkBoxes[i].Checked){
                    days.Add(dayNumbers[i]);
                }
            }

            //pass the values of the settings table to the main instance of the EnterTime class
            EnterTime.SetDays(days);
            EnterTime.SetTime(Time);
            //hide settings window
            this.Hide();

        }

        /// <summary>
        /// Uses the current date and a past pay period to determine the current pay period
        /// </summary>
        private void CalculateCurrentPayPeriod()
        {
            DateTime endOfAPayPeriod = new DateTime(2018, 12, 25);
            DateTime currentTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (currentTime > endOfAPayPeriod)
            {
                endOfAPayPeriod = endOfAPayPeriod.AddDays(14);
            }

            this.startOfPayPeriod = endOfAPayPeriod.Subtract(new TimeSpan(13, 0, 0, 0));
            this.endOfPayPeriod = endOfAPayPeriod;
        }

        /// <summary>
        /// Add date to the checkbox text for the days worked
        /// </summary>
        private void AddDateToDayCheckboxes()
        {
            for (int index = 0; index < checkBoxes.Length; index++)
            {
                // Due to weekends we need to offset the third index by 2 and the eigth by 4
                // indexOffset represents the number of days we offset by
                int indexOffset = index;

                if (index >= 8)
                {
                    indexOffset += 4;
                }
                else if (index >= 3)
                {
                    indexOffset += 2;
                }

                checkBoxes[index].Text += " " + this.startOfPayPeriod.AddDays(indexOffset).Month.ToString() + "/" +
                    this.startOfPayPeriod.AddDays(indexOffset).Day.ToString();
            }
        }
    }
}
