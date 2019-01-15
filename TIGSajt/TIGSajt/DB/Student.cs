using System;
using System.Collections.Generic;

namespace TIGSajt.DB
{
    public partial class Student
    {
        public Student()
        {
            StatisticsGuestStudent = new HashSet<Statistics>();
            StatisticsHomeStudent = new HashSet<Statistics>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public short Type { get; set; }

        public virtual ICollection<Statistics> StatisticsGuestStudent { get; set; }
        public virtual ICollection<Statistics> StatisticsHomeStudent { get; set; }
    }

    public enum eDbEntryType : short
    {
        Regular = 0,
        Test = 1
    }
}
