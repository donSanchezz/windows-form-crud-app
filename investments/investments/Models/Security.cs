using System;
using System.Collections.Generic;

namespace investments.Models
{
    public partial class Security
    {
        public string IsinCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int StatusId { get; set; }
    }
}
