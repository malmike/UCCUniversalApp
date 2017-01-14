using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UCCUniversalApp.Model;

namespace UCCUniversalApp.ViewModels
{
    class SupervisorViewModel
    {
        public async Task<ObservableCollection<EmployeeCheckIn>> getCheckInData()
        {
            try
            {
                SQLiteAsyncConnection connection = new SQLiteAsyncConnection("EmployeeCheckIn.db");
                List<EmployeeCheckIn> employeeCIO = await connection.Table<EmployeeCheckIn>().Where(x => x.Approval == "Pending").ToListAsync();
                return new ObservableCollection<EmployeeCheckIn>(employeeCIO);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
