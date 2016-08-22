using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raffle.Models
{
    public class PasscodeModel
    {
        [Required]
        public int passcode { get; set; }
    }
}