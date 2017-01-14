using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCCUniversalApp.Model
{

    [Table("EmployeeCheckIn")]
    public class EmployeeCheckIn
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int ID { get; set; }
        public string EmpName { get; set; }
        public int CheckID { get; set; }
        public string CheckType { get; set; }
        public string Location { get; set; }
        public string Approval { get; set; }

    }

    public class EmpRootObject
    {
        public EmployeeCheckIn data { get; set; }
    }

    public class EmpMultipleRootObject
    {
        public List<EmployeeCheckIn> data { get; set; }
    }
}