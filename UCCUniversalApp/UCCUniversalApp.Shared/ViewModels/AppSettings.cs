using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UCCUniversalApp.Model;
using Windows.Storage;
using Windows.Storage.Streams;

namespace UCCUniversalApp.ViewModels
{
    class AppSettings
    {
        public void storeRegistrationSettings(string json)
        {

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["employeeDetails"] = json;
        }

        //public async void storeRegistrationSettings(string json)
        //{

        //    var folder = ApplicationData.Current.LocalFolder;
        //    var employeeDetailsFolder = await folder.CreateFolderAsync("EmployeeDetailsFolder", CreationCollisionOption.OpenIfExists);

        //    var employeeDetailsFile = await employeeDetailsFolder.CreateFileAsync("employeeDetails.txt", CreationCollisionOption.ReplaceExisting);
        //    await FileIO.WriteTextAsync(employeeDetailsFile, json);  

        //}


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

        //public async Task<string> retrieveRegistrationSettings()
        //{
        //    var folder = ApplicationData.Current.LocalFolder;
        //    var employeeDetailsFolder = await folder.CreateFolderAsync("EmployeeDetailsFolder", CreationCollisionOption.OpenIfExists);

        //    var files = await employeeDetailsFolder.GetFilesAsync();
        //    var desiredFile = files.FirstOrDefault(x => x.Name == "employeeDetails.txt");

        //    var textContent = await FileIO.ReadTextAsync(desiredFile);

        //    return textContent;

        //}

        public bool verifyRegistrationSettings()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            try
            {
                if (localSettings.Values.ContainsKey("employeeDetails"))
                {
                    return true;
                }
                else
                {
                    return false;
                }   
              
            }
            catch
            {
                return false;
            }

        }

        //public async Task<bool> verifyRegistrationSettings()
        //{
        //    var folder = ApplicationData.Current.LocalFolder;
        //    var employeeDetailsFolder = await folder.CreateFolderAsync("EmployeeDetailsFolder", CreationCollisionOption.OpenIfExists);
        //    try
        //    {
        //        var files = await employeeDetailsFolder.GetFilesAsync();
        //        var desiredFile = files.FirstOrDefault(x => x.Name == "employeeDetails.txt");

        //        if (await FileIO.ReadTextAsync(desiredFile) != null)
        //            return true;
               
        //        return false;

        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}

        public void storeNavigationSettings(string navURI)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["navigationDetails"] = navURI;
        }

        public string retrieveNavigationSettings()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("navigationDetails"))
            {
                return localSettings.Values["navigationDetails"].ToString();

            }
            else
            {
                return null;
            }

        }

        public void storeDeviceSettings(string Device)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["Device"] = Device;
        }

        public string retrieveDeviceSettings()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("Device"))
            {
                return localSettings.Values["Device"].ToString();

            }
            else
            {
                return null;
            }

        }


        public bool checkNavSettings()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("navigationDetails"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void deleteNavigationSetting()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("navigationDetails"))
            {
                localSettings.Values.Remove("navigationDetails");
            }
        }



        public void storePushURISettings(string pushURI)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["pushURI"] = pushURI;
        }

        public string retrievePushURISettings()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("pushURI"))
            {
                return localSettings.Values["pushURI"].ToString();

            }
            else
            {
                return null;
            }

        }

        public bool checkPushURISettings()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("pushURI"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void deletePushURISetting()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("pushURI"))
            {
                localSettings.Values.Remove("pushURI");
            }
        }

        public void storePushArgs(string pushArgs)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["pushArgs"] = pushArgs;
        }

        public string retrievePushArgs()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("pushArgs"))
            {
                return localSettings.Values["pushArgs"].ToString();

            }
            else
            {
                return null;
            }

        }

        public bool checkPushArgsSettings()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("pushArgs"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
