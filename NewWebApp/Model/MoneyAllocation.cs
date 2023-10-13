using System;
using System.Collections.Generic;

#nullable disable

namespace NewWebApp.Model
{
    public partial class MoneyAllocation
    {
        public int AllocationId { get; set; }
        public int DisasterId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? AllocationDate { get; set; }

        public virtual Disaster Disaster { get; set; }
    }
}
