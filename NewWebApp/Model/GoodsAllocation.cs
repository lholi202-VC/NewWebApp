using System;
using System.Collections.Generic;

#nullable disable

namespace NewWebApp.Model
{
    public partial class GoodsAllocation
    {
        public int AllocationId { get; set; }
        public int DisasterId { get; set; }
        public int GoodsDonationId { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? AllocationDate { get; set; }

        public virtual Disaster Disaster { get; set; }
        public virtual GoodsDonation GoodsDonation { get; set; }
    }
}
