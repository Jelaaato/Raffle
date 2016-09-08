using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raffle.Models
{
    public class PrizeViewModel
    {
        public Guid event_id { get; set; }
        public string prize_name { get; set; }
        public int quantity { get; set; }
        public IQueryable<PrizeDTO> prize { get; set; }
    }

    public class PrizeDTO
    {
        public Guid prize_id { get; set; }
        public string distinct_prize_name { get; set; }
        public int count { get; set; }
        public int? prizeout_count { get; set; }
    }
}