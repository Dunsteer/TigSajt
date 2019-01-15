using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TIGSajt.Models
{
    public class StudentModel
    {
        public string Student { get; set; }
        public string Score { get; set; }
        public string Points { get; set; }
        public string StudentId { get; set; }
        public string Lost { get; internal set; }
        public string Draw { get; internal set; }
        public string Win { get; internal set; }
    }
}
