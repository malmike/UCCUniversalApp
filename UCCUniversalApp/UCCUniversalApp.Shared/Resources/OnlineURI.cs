using System;
using System.Collections.Generic;
using System.Text;

namespace UCCUniversalApp.Resources
{
    class OnlineURI
    {

        //private const string serverURI = "http://malmike21.freevar.com/UCCFieldProjectPHP/Mobile/";
        private const string serverURI = "http://localhost:25223/UCCFieldProjectPHP/Mobile/";

        public string getEmployeeDetails { get; private set; }
        public string insertEmployeeDetails { get; private set; }
        public string getSupervisorDetails { get; private set; }
        public string employeeCheckIn { get; private set; }
        public string updatePushURI { get; private set; }
        public string changeApproval { get; private set; }
        public string getCheckData { get; private set; }


        public OnlineURI()
        {
            this.getEmployeeDetails = serverURI + "RetrieveEmployeeDetails/getEmployeeDetails.php";
            this.insertEmployeeDetails = serverURI + "InsertEmployee/insertEmployee.php";
            this.getSupervisorDetails = serverURI + "RetrieveEmployeeDetails/getSupervisor.php";
            this.employeeCheckIn = serverURI + "PushNotifications/employeeCheckIn.php";
            this.updatePushURI = serverURI + "InsertEmployee/updatePushURI.php";
            this.changeApproval = serverURI + "ChangeApproval/ChangeApproval.php";
            this.getCheckData = serverURI + "GetCheckData/GetCheckData.php";
        }
 
    }
}
