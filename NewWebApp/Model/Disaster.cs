using System;
using System.Collections.Generic;

#nullable disable

namespace NewWebApp.Model
{
    public partial class Disaster
    {
        internal decimal AvailableMoney;

        public Disaster()
        {
            GoodsAllocations = new HashSet<GoodsAllocation>();
            MoneyAllocations = new HashSet<MoneyAllocation>();
            Purchases = new HashSet<Purchase>();
        }

        public int DisasterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string AidTypes { get; set; }

        public virtual ICollection<GoodsAllocation> GoodsAllocations { get; set; }
        public virtual ICollection<MoneyAllocation> MoneyAllocations { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
