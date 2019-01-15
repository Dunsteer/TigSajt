using System;
using System.Collections.Generic;

namespace TIGSajt.DB
{
    public partial class Statistics
    {
        public long Id { get; set; }
        public long? HomeStudentId { get; set; }
        public long? GuestStudentId { get; set; }
        public int? HomePoints { get; set; }
        public int? GuestPoints { get; set; }
        public int? HomeScore { get; set; }
        public int? GuestScore { get; set; }
        public DateTime? Created { get; set; }

        public virtual Student GuestStudent { get; set; }
        public virtual Student HomeStudent { get; set; }
    }
}
