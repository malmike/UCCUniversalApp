using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RawPushBGTask.Modules
{
    class EmployeeCheckInBG
    {
        public void storeEmployeeCheckIn(string json, int setNumber)
        {
            

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["CheckIn " + setNumber] = json;

            storeNumber(setNumber + 1);
            Debugger.Break();
            //int number = Convert.ToInt32(setNumber.Split(' ')[2]);

        }

        public string retrieveRegistrationSettings()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("employeeDetails"))
            {
                return localSettings.Values["employeeDetails"].ToString();
            }

            else
            {
                return null;
            }

        }

        private void storeNumber(int Number)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["number"] = Number;
        }
        public int checkNumber()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("number"))
            {
                return (int)localSettings.Values["number"];
            }

            else
            {
                return 0;
            }
        }
    }

    
}
