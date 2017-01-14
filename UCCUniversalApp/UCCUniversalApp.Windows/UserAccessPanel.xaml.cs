using RawPushBGTask;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UCCUniversalApp.Model;
using UCCUniversalApp.Resources;
using UCCUniversalApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    public sealed partial class UserAccessPanel : Page
    {
        SharedInformation sharedInfo = SharedInformation.getInstance();
        InsertUserDetails insDetails = new InsertUserDetails();
        OnlineURI webURI = new OnlineURI();
        AppSettings appSettings = new AppSettings();
        SupervisorViewModel sup = new SupervisorViewModel();
        bool supProve = false;

        EmployeeCheckInDataContext context = new EmployeeCheckInDataContext();
        UpdateEmployeeApproval approval = new UpdateEmployeeApproval();



        public UserAccessPanel()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.VisibilityChanged += OnVisibilityChanged;
 
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            AppSettings appSettings = new AppSettings();
            lvEmployee.ItemsSource = sharedInfo.employeeDetails;
            supervisorName.Text = sharedInfo.supervisorDetails.FirstName + "    " + sharedInfo.supervisorDetails.LastName;


            int i = context.retrieveNumberSettings();
            if (i != 0)
            {
                for (int x = 0; x < i; x++)
                {
                    context.storeElements(context.retrieveCheckInSettings("CheckIn " + x));
                    context.dropCheckInSettings("CheckIn " + x);
                }
                context.dropNumberSettings();
            }

            await fillSupervisor();

        }

        private async Task fillSupervisor()
        {
            ObservableCollection<EmployeeCheckIn> ch = await sup.getCheckInData();
            if (ch != null)
            {
                lvCheckIn.ItemsSource = ch;
            }
        }

        private void checkingIn(object sender, RoutedEventArgs e)
        {
            string employeeName = sharedInfo.empData.FirstName + " " + sharedInfo.empData.LastName;
            insDetails.InsertDetails(webURI.employeeCheckIn, sharedInfo.empData.EmpFn, employeeName, sharedInfo.supervisorDetails.EmpFn, locationBox.Text, reasonBox.Text, "CheckIn");
            locationBox.Text = "";
            reasonBox.Text = "";
        }

        private void checkingOut(object sender, RoutedEventArgs e)
        {
            string employeeName = sharedInfo.empData.FirstName + " " + sharedInfo.empData.LastName;
            insDetails.InsertDetails(webURI.employeeCheckIn, sharedInfo.empData.EmpFn, employeeName, sharedInfo.supervisorDetails.EmpFn, locationBox.Text, reasonBox.Text, "CheckOut");
            locationBox.Text = "";
            reasonBox.Text = "";
        }


        private async void OnVisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            if (e.Visible)
            {
                PushBGTask.DisableTask();
                int i = context.retrieveNumberSettings();
                if (i != 0)
                {
                    for (int x = 0; x < i; x++)
                    {
                        context.storeElements(context.retrieveCheckInSettings("CheckIn " + x));
                        context.dropCheckInSettings("CheckIn " + x);
                    }
                    context.dropNumberSettings();

                }
                await fillSupervisor();
            }
            else
            {
                PushBGTask.Register();            
            }
            
        }



        private void Selection_Change(object sender, RoutedEventArgs e)
        {
            lvCheckIn.SelectionMode = ListViewSelectionMode.Multiple;
            acceptanceAppBar();
            
        }

        private async void Refresh_Data(object sender, RoutedEventArgs e)
        {
            await fillSupervisor();
        }

        private void acceptanceAppBar()
        {
            bottomCommandBar.PrimaryCommands.Clear();
            AppBarButton acceptButton = new AppBarButton();
            acceptButton.Icon = new SymbolIcon(Symbol.Accept);
            acceptButton.Label = "Accept";
            acceptButton.Click += Accept_Click;
            bottomCommandBar.PrimaryCommands.Insert(0, acceptButton);
            AppBarButton denyButton = new AppBarButton();
            denyButton.Icon = new SymbolIcon(Symbol.Cancel);
            denyButton.Label = "Deny";
            denyButton.Click += Deny_Click;
            bottomCommandBar.PrimaryCommands.Insert(1, denyButton);
            AppBarButton returnButton = new AppBarButton();
            returnButton.Icon = new SymbolIcon(Symbol.Clear);
            returnButton.Label = "CloseSelection";
            returnButton.Click += Return_Click;
            bottomCommandBar.PrimaryCommands.Insert(2, returnButton);
        }

        private void defaultAppBar()
        {
            bottomCommandBar.PrimaryCommands.Clear();
            AppBarButton selectButton = new AppBarButton();
            selectButton.Icon = new SymbolIcon(Symbol.SelectAll);
            selectButton.Label = "Select";
            selectButton.Click += Selection_Change;
            bottomCommandBar.PrimaryCommands.Insert(0, selectButton);
            AppBarButton refreshButton = new AppBarButton();
            refreshButton.Icon = new SymbolIcon(Symbol.Refresh);
            refreshButton.Label = "Refresh";
            refreshButton.Click += Refresh_Data;
            bottomCommandBar.PrimaryCommands.Insert(1, refreshButton);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            lvCheckIn.SelectionMode = ListViewSelectionMode.Single;
            defaultAppBar();
        }

        private async void Deny_Click(object sender, RoutedEventArgs e)
        {
            List<Object> employeeList = lvCheckIn.SelectedItems.ToList();

            foreach (EmployeeCheckIn value in employeeList)
            {
                await context.UpdateApproval(value.CheckID, "DENIED");
            }

            approval.ChangeApproval(webURI.changeApproval, employeeList, employeeList.Count(), "DENIED");
            await fillSupervisor();
            lvCheckIn.SelectionMode = ListViewSelectionMode.Single;
            defaultAppBar();
        }

        private async void Accept_Click(object sender, RoutedEventArgs e)
        {
            List<Object> employeeList = lvCheckIn.SelectedItems.ToList();

            foreach (EmployeeCheckIn value in employeeList)
            {
                await context.UpdateApproval(value.CheckID, "ALLOWED");
            }

            approval.ChangeApproval(webURI.changeApproval, employeeList, employeeList.Count(), "ALLOWED");

            await fillSupervisor();
            lvCheckIn.SelectionMode = ListViewSelectionMode.Single;
            defaultAppBar();
            
        }
    }
}
