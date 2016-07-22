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
        public IEnumerable<Prizes> prizes { get; set; }
    }
}