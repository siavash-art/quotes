using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quotes.Models
{
    public class QuoteModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Quote { get; set; }
        [Required]
        public string Category { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
