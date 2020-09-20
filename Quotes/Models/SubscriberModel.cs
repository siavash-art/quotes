using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Models
{
    public class SubscriberModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
