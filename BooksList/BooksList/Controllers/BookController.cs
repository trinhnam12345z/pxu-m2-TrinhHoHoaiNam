using BooksList.Models;
using BooksList.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksList.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _appDbContext;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var book = _appDbContext.Books.Include(p => p.Category).ToList();
            return View(book);
        }
        [HttpPost]

        public async Task<IActionResult> Index(string BookSearch)
        {
            ViewData["GetBook"] = BookSearch;
            var empquery = from x in _appDbContext.Books select x;
            if (!String.IsNullOrEmpty(BookSearch))
            {
                empquery = empquery.Where(x => x.Name.Contains(BookSearch));
            }
            return View(await empquery.AsNoTracking().ToListAsync());

        }

        public IActionResult Create()
        {
            BookCreateVM productVM = new BookCreateVM()
            {
                books = new Books(),
                CategorySelectList = _appDbContext.Categories.Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                })
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(Books books)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Books.Add(books);
                _appDbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(books);
        }
        public IActionResult Edit(int? id)
        {
            var book = _appDbContext.Books.Find(id);

            var selectList = new List<SelectListItem>();

            BookCreateVM bookVM = new BookCreateVM()
            {
                books = _appDbContext.Books.FirstOrDefault(item => item.Id == id),
                CategorySelectList = _appDbContext.Categories.Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                }),

            };

            return View(bookVM);
        }

        [HttpPost]
        public IActionResult Edit(BookCreateVM bookCreateVM)
        {

            string webRootPath = _webHostEnvironment.WebRootPath;

            var objProduct = _appDbContext.Books.AsNoTracking().FirstOrDefault(bo => bo.Id == bookCreateVM.books.Id);


            _appDbContext.Books.Update(bookCreateVM.books);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var books = _appDbContext.Books.Find(id);
            if (books == null) return NotFound();

            return View(books);
        }

        [HttpPost]
        public IActionResult DeleteBook(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var books = _appDbContext.Books.Find(id);
            if (books == null) return NotFound();

            _appDbContext.Books.Remove(books);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
