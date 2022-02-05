using System;
using System.Collections.Generic;

#nullable disable

namespace back.Models
{
    public partial class Btc
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Idate { get; set; }
    }
}
