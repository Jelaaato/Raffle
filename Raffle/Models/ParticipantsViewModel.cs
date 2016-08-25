using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raffle.Models
{
    public class ParticipantsViewModel
    {
        public IEnumerable<string> participants { get; set; }
        public Prizes prizes { get; set; }
        public Participants winner { get; set; }
        public string winner_name { get; set; }
    }
}