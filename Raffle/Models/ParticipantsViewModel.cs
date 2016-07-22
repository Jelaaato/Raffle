using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raffle.Models
{
    public class ParticipantsViewModel
    {
        public IEnumerable<string> participants { get; set; }
    }

    //public class PrizeViewModel
    //{
    //    public string PrizeName { get; set; }
    //    public int Quantity { get; set; }
    //}
}