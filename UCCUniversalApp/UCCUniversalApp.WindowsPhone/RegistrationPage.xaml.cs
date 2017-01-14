using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UCCUniversalApp.Resources;
using UCCUniversalApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556


namespace UCCUniversalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {
        AppSettings appSettings = new AppSettings();
        SharedInformation sharedInformation = SharedInformation.getInstance();
        OnlineURI webURI = new OnlineURI();
        private GetUserDetails userDetails = new GetUserDetails();
        private RetrieveValues retrieveValues = new RetrieveValues();

        public RegistrationPage()
        {
            this.InitializeComponent();
        }

        private async void RegisterUser(object sender, RoutedEventArgs e)
        {

            string responseText = await userDetails.getDetails(webURI.getEmployeeDetails, UserId.Text, null, null);

            await userDetails.getDetails(webURI.insertEmployeeDetails, responseText, sharedInformation.pushURI, "WP");
            appSettings.storeDeviceSettings("WP");
            appSettings.storeRegistrationSettings(responseText.ToString());
            sharedInformation.retrieveEmployeeDetails();
            sharedInformation.employeeData();
            sharedInformation.jsn = await userDetails.getDetails(webURI.getSupervisorDetails, UserId.Text, null, null);
            sharedInformation.retrieveSupervisorDetails(sharedInformation.jsn);
            await retrieveValues.getValues(webURI.getCheckData, sharedInformation.empData.EmpFn);
            Debugger.Break();

            this.Frame.Navigate(typeof(UserAccessPanel));

        }
    }
}
