using System;
using System.Collections.Generic;

#nullable disable

namespace NewWebApp.Model
{
    public partial class Purchase
    {
        public int PurchaseId { get; set; }
        public int DisasterId { get; set; }
        public int GoodsDonationId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? AmountSpent { get; set; }

        public virtual Disaster Disaster { get; set; }
        public virtual GoodsDonation GoodsDonation { get; set; }
        public List<Disaster> ActiveDisasters { get; internal set; }
        public List<GoodsDonation> GoodsDonations { get; internal set; }
    }
}
