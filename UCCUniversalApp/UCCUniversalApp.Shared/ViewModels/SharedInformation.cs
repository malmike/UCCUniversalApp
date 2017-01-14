using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCCUniversalApp.Model;

namespace UCCUniversalApp.ViewModels
{
    class SharedInformation
    {
        private static SharedInformation instance = new SharedInformation();
        GetEmployeeDetails getEmployeeDetails = new GetEmployeeDetails();
        AppSettings appSettings;
        public SupervisorDetails supervisorDetails { get; private set; }
        public ObservableCollection<EmployeeDetails> employeeDetails { get; private set; }
        public EmployeeDetails empData { get; private set; }


        public string userVariables { get; set; }

        public string pushURI { get; set; }

        public string jsn { get; set; }

        private SharedInformation() { }

        public static SharedInformation getInstance()
        {
            return instance;
        }

        public void retrieveEmployeeDetails()
        {
            appSettings = new AppSettings();

            this.employeeDetails = getEmployeeDetails.DataElements(appSettings.retrieveRegistrationSettings());
      
        }

        public void retrieveSupervisorDetails(String json)
        {
            this.supervisorDetails = getEmployeeDetails.SupervisorDataElements(json);
        }


        public void employeeData()
        {
            empData = getEmployeeDetails.emp;
            
        }

        public void storePushURI(string pushURI)
        {
            appSettings = new AppSettings();
            appSettings.storePushURISettings(pushURI);
        }

    }

}
