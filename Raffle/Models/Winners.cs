//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Raffle.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Winners
    {
        public System.Guid winner_id { get; set; }
        public System.Guid event_id { get; set; }
        public System.Guid prize_id { get; set; }
        public string prize_name { get; set; }
        public System.Guid participant_id { get; set; }
        public string winner_name { get; set; }
        public string winner_department { get; set; }
        public Nullable<System.DateTime> raffled_datetime { get; set; }
    }
}
