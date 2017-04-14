using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Domain.Entities;
using BookShop.Domain.Abstract;

namespace BookShop.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IBookRepository repository;
        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View(repository.Books);
        }

        public ActionResult Edit(int bookId)
        {
            Book book = repository.Books
                .FirstOrDefault(item => item.BookID == bookId);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book, HttpPostedFileBase image = null)
        {
            if(ModelState.IsValid)
            {
                if (image != null)
                {
                    book.ImageType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                repository.SaveBook(book);
                TempData["message"] = string.Format("Изменения в книге \"{0}\" были сохранены", book.BookName);
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Book());
        }
        [HttpPost]
        public ActionResult Delete(int bookId)
        {
            Book deleteBook = repository.DeleteBook(bookId);
            if(deleteBook != null)
            {
                TempData["message"] = string.Format("Книга \"{0}\" была удалена", deleteBook.BookName);
            }
            return RedirectToAction("Index");
        }
    }
}