using System;
using System.Collections.Generic;

#nullable disable

namespace NewWebApp.Model
{
    public partial class MonetaryDonation
    {
        public int DonationId { get; set; }
        public string DonorName { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DonationDate { get; set; }
    }
}
