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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public TimeEntering EnterTime;

        //when enter settings is clicked
        private void enterSettings_Click(object sender, EventArgs e)
        {
            //creates the arrays for all checkboxes and their corresponding dropdown indexes
            CheckBox[] checkBoxes = { CB1, CB2, CB3, CB6, CB7, CB8, CB9, CB10, CB13, CB14 };
            int[] dayNumbers = { 1, 2, 3, 6, 7, 8, 9, 10, 13, 14 };

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
    }
}
