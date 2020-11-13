using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksList.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string  Author { get; set; }
        public string  ShortContent { get; set; }
        public int PublicYear { get; set; }
        public int Amount { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
