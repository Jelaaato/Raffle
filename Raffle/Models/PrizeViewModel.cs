using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raffle.Models
{
    public class PrizeViewModel
    {
        public Guid event_id { get; set; }
        [Required]
        public string prize_name { get; set; }
        [Required]
        public int quantity { get; set; }
        public bool include_all { get; set; }
        public IQueryable<PrizeDTO> prizes { get; set; }
    }

    public class PrizeDTO
    {
        public Guid prize_id { get; set; }
        public string prize_name { get; set; }
        public int? prizeout_count { get; set; }
        public int prize_count { get; set; }
    }
}