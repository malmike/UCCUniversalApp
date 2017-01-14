using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace UCCUniversalApp.Model
{


    class EmployeeCheckInDataContext
    {
        public EmployeeCheckIn emp { get; private set; }
        public List<EmployeeCheckIn> empList { get; private set; }
        public List<int> checkValues { get; private set; }
        public string testData { get; private set; }


        /// <summary>
        /// Retrieve values from JSON file containing employee check in/ out information
        /// </summary>
        /// <param name="json"></param>
        public void storeElements(String json)
        {
            try
            {
                var rootObject = JsonConvert.DeserializeObject<EmpRootObject>(json);
                this.emp = rootObject.data;
                addItem(emp);
            }

            catch (Exception)
            {
                return;
            }
        }
        public void storeMultipleElements(String json)
        {
            try
            {
                var multiRootObject = JsonConvert.DeserializeObject<EmpMultipleRootObject>(json);
                this.empList = multiRootObject.data;
                addListItems(empList);
            }

            catch (Exception)
            {
                return;
            }
        }



        /// <summary>
        /// Using SQLite Database
        /// </summary>
        /// <param name="SQLite"></param>
        /// <returns></returns>


        public async Task<bool> DoesDbExist(string DatabaseName)
        {
            bool dbexist;
            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(DatabaseName);
                dbexist = true;
            }
            catch
            {
                dbexist = false;
            }

            return dbexist;
        }
        

        public async void CreateDatabase()
        {
            bool dbExist = await DoesDbExist("EmployeeCheckIn.db");

            if (!dbExist)
            {
                SQLiteAsyncConnection connection = new SQLiteAsyncConnection("EmployeeCheckIn.db");
                await connection.CreateTableAsync<EmployeeCheckIn>();
            }
        }


        public async Task UpdateApproval(int CheckID, string approval)
        {
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("EmployeeCheckIn.db");
            var employeeDetail = await connection.Table<EmployeeCheckIn>().Where(x => x.CheckID == CheckID).FirstOrDefaultAsync();
            if (employeeDetail != null)
            {
                employeeDetail.Approval = approval;
                await connection.UpdateAsync(employeeDetail);
            }
        }


        public async void DropDatabase()
        {
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("EmployeeCheckIn.db");
            await connection.DropTableAsync<EmployeeCheckIn>();
        }


        public async void addItem(EmployeeCheckIn checkIn)
        {
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("EmployeeCheckIn.db");
            await connection.InsertAsync(checkIn);
        }


        public async void addListItems(List<EmployeeCheckIn> checkIn)
        {
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("EmployeeCheckIn.db");
            await connection.InsertAllAsync(checkIn);
        }

        public async Task<List<EmployeeCheckIn>> retrieveCheckID()
        {
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("EmployeeCheckIn.db");
            var employeeDetail = await connection.Table<EmployeeCheckIn>().Where(x => x.Approval == "Pending").ToListAsync();
            List<EmployeeCheckIn> employees = employeeDetail;
            return employees;
        }




        /// <summary>
        /// Fetching and deleting data from local storage related to employee check in/ out information
        /// </summary>
        /// <param name="Employee CheckIn Retrieve Local Storage"></param>
        /// <returns></returns>

        
        public string retrieveCheckInSettings(string name)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(name))
            {
                return localSettings.Values[name].ToString();
            }

            else
            {
                return null;
            }

        }

        public int retrieveNumberSettings()
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

        public void dropCheckInSettings(string name)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(name))
            {
                localSettings.Values.Remove(name);
            }
        }

        public void dropNumberSettings()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("number"))
            {
                localSettings.Values.Remove("number");
            }
        }

    }
}





//////                                                                              /////////
//////WRITING AND RETRIEVEING FROM A FILE IN LOCAL STORAGE USING JSON SERIALIZATION /////////
//////                                                                              /////////
//////                                                                              /////////
/*
 * private const string JSONFILENAME = "data.json";
 * 
private async Task writeToFile(List<EmployeeCheckIn> checkIn)
        {
            Debugger.Break();
            var serializer = new DataContractJsonSerializer(typeof(List<EmployeeCheckIn>));
            Debugger.Break();
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync
                         (JSONFILENAME, CreationCollisionOption.ReplaceExisting);

            Debugger.Break();
            using (var rtStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                Debugger.Break();
                using (Stream inputStream = rtStream.AsStream())
                {
                    Debugger.Break();
                    serializer.WriteObject(inputStream, checkIn);
                    Debugger.Break();
                }
                Debugger.Break();
            }

            //await FileIO.WriteLinesAsync(inputStream, checkIn);      

        }

        public async Task readFromFile()
        {
            string content = String.Empty;
            List<EmployeeCheckIn> checkIn = new List<EmployeeCheckIn>();
            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync
                             (JSONFILENAME);
            using (StreamReader reader = new StreamReader(myStream))
            {
                content = await reader.ReadToEndAsync();
            }
            this.testData = content;
        }

*/

//////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////