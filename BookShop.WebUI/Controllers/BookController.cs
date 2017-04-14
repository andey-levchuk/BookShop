using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Domain.Abstract;
using BookShop.Domain.Entities;
using BookShop.WebUI.Models;

namespace BookShop.WebUI.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;
        int booksForPage = 4;

        public BookController(IBookRepository rep)
        {
            repository = rep;
        }

        public ActionResult List(string category, int page = 1)
        {
            BookListViewModel model = new BookListViewModel {
                Books = repository.Books
                    .Where(x => category == null || x.Category == category)
                    .OrderBy(book => book.BookID)
                    .Skip(booksForPage * (page - 1))
                    .Take(booksForPage),
                PageInfo = new PageInfo
                {
                    BooksQuantity = category == null ?
                        repository.Books.Count() : repository.Books.Where(book => book.Category == category).Count(),
                    BooksOnPage = booksForPage,
                    CurrentPage = page
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int bookId)
        {
            Book book = repository.Books
                .FirstOrDefault(item => item.BookID == bookId);

            if (book != null)
            {
                return File(book.ImageData, book.ImageType);
            }
            else
            {
                return null;
            }
        }
    }
}