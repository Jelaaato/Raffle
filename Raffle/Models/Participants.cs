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
    
    public partial class Participants
    {
        public System.Guid participant_id { get; set; }
        public Nullable<System.Guid> event_id { get; set; }
        public string employee_number { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string department_name { get; set; }
        public string position_name { get; set; }
        public Nullable<bool> winner_flag { get; set; }
        public Nullable<bool> registered_flag { get; set; }
        public string display_name { get; set; }
        public Nullable<System.DateTime> registered_datetime { get; set; }
        public Nullable<bool> delete_flag { get; set; }
        public Nullable<bool> manual_reg_flag { get; set; }
        public Nullable<bool> winnerAgain_flag { get; set; }
    }
}
