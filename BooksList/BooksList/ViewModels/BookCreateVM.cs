using BooksList.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksList.ViewModels
{
    public class BookCreateVM
    {
        public Books books{ get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
        public IEnumerable<Category> Category { get; set; }
    }
}
