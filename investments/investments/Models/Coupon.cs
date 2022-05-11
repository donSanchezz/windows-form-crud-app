using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace investments.Models
{
    public partial class Coupon
    {
        [DisplayName("ID")]
        public int CouponId { get; set; }

        [DisplayName("ISIN")]
        public string? IsinCode { get; set; }

        [DisplayName("Payment Date")]
        public DateTime? PaymentDate { get; set; }
        public DateTime? RecordDate { get; set; }
        public int? StatusId { get; set; }



        [NotMapped]
        public string Description { get; set; } = null!;

        [DisplayName("Status")]
        [NotMapped]
        public string StatusName { get; set; } = null!;
    }
}
