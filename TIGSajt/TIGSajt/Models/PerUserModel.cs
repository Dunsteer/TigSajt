using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TIGSajt.Models
{
    public class PerUserModel
    {
        public string HomeStudentId { get; internal set; }
        public string GuestStudentId { get; internal set; }
        public string HomeStudentName { get; internal set; }
        public string GuestStudentName { get; internal set; }
        public string Win { get; internal set; }
        public string Draw { get; internal set; }
        public string Lost { get; internal set; }
    }
}
