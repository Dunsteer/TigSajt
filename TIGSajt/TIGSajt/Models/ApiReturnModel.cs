using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TIGSajt.Models
{
    public class ApiReturnModel
    {
        public bool OK { get; set; }
        public eErrorCode? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    public enum eErrorCode
    {
        Ok = 0,
        NotFound = -1,
        InvalidData = -2
    }
}
