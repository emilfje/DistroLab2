using System;
using System.ComponentModel.DataAnnotations;

namespace Distro2.Models
{
    //Model for the message object in the message database.
    public class Message
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Body { get; set; }
        [Required]
        public Boolean IsRead { get; set; }

        [Display(Name = "Time Stamp")]
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        public string Receiver { get; set; }
    }
}
