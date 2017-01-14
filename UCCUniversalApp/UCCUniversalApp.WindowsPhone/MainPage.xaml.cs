using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using System.Diagnostics;
using Windows.Networking.PushNotifications;
using UCCUniversalApp.Resources;
using UCCUniversalApp.ViewModels;
using Windows.UI.Popups;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using RawPushBackGroundTaskWP;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UCCUniversalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        OnlineURI webURI = new OnlineURI();
        SharedInformation sharedInformation = SharedInformation.getInstance();
        private GetUserDetails userDetails = new GetUserDetails();
        AppSettings appSettings = new AppSettings();
        private PushNotifications SharedPushComponent = new PushNotifications();
        MessageDialog msgbox;
        Task pushURITask;
        string pushNotificationURI;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            PushBGTask.Register();

            //if (appSettings.checkPushURISettings())
            //{
            //    pushNotificationURI = appSettings.retrievePushURISettings();
            //}

            bool isNetwork = NetworkInterface.GetIsNetworkAvailable();
            if (isNetwork)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    #region RAW
                    SharedPushComponent.deliverRawNotification += SharedPushComponent_deliverRawNotification;
                    #endregion
                    #region TOAST
                    SharedPushComponent.deliverToastNotification += SharedPushComponent_deliverToastNotification;
                    #endregion
                    SharedPushComponent.Initilialize();
                });


                if (appSettings.verifyRegistrationSettings())
                {
                    //pushURITask.Wait();


                    sharedInformation.retrieveEmployeeDetails();
                    sharedInformation.employeeData();

                    sharedInformation.jsn = await userDetails.getDetails(webURI.getSupervisorDetails, sharedInformation.empData.EmpFn, null);
                    sharedInformation.retrieveSupervisorDetails(sharedInformation.jsn);

                    //sharedInformation.pushURI = SharedPushComponent.pushURI;
                    //if (sharedInformation.pushURI != pushNotificationURI)
                    //{
                    //    appSettings.storePushURISettings(sharedInformation.pushURI);
                    //    await userDetails.getDetails(webURI.updatePushURI, sharedInformation.empData.Serial.ToString(), sharedInformation.pushURI);
                    //}

                    this.Frame.Navigate(typeof(UserAccessPanel));

                }
                else
                {
                    //pushURITask.Wait();
                    //sharedInformation.pushURI = SharedPushComponent.pushURI;
                    
                    //appSettings.storePushURISettings(sharedInformation.pushURI);
                    this.Frame.Navigate(typeof(RegistrationPage));
                }
            }
        }

        void SharedPushComponent_deliverToastNotification(Windows.Networking.PushNotifications.PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            Debugger.Break();
        }


        async private void SharedPushComponent_deliverRawNotification(PushNotificationChannel sender, RawNotification args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                #region RAW_IN_APP
                if (!PushBGTask.IsTaskRegistered())
                {
                  
                }
                #endregion
            });
            Debugger.Break();
        }


        #region ManageBGTask
        private void EnableBGTask_Click(object sender, RoutedEventArgs e)
        {
            //Register the applications backgroundtask
            PushBGTask.Register();
        }

        private void DisableBGTask_Click(object sender, RoutedEventArgs e)
        {
            PushBGTask.DisableTask();
        }
        #endregion

    }
}
