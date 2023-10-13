using System;
using System.Collections.Generic;

#nullable disable

namespace NewWebApp.Model
{
    public partial class GoodsDonation
    {
        public GoodsDonation()
        {
            GoodsAllocations = new HashSet<GoodsAllocation>();
            Purchases = new HashSet<Purchase>();
        }

        public int DonationId { get; set; }
        public string DonorName { get; set; }
        public DateTime? DonationDate { get; set; }
        public decimal? NumberOfItems { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int? UserDefinedCategoryId { get; set; }

        public virtual UserDefinedCategory UserDefinedCategory { get; set; }
        public virtual ICollection<GoodsAllocation> GoodsAllocations { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
