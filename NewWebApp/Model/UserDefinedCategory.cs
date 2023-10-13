using System;
using System.Collections.Generic;

#nullable disable

namespace NewWebApp.Model
{
    public partial class UserDefinedCategory
    {
        public UserDefinedCategory()
        {
            GoodsDonations = new HashSet<GoodsDonation>();
        }

        public int UserDefinedCategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<GoodsDonation> GoodsDonations { get; set; }
    }
}
