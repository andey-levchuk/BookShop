using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Domain.Entities;
using BookShop.Domain.Abstract;
using BookShop.WebUI.Models;

namespace BookShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        private IBookRepository repository;
        private IOrderHandler orderHandler;
        public BasketController(IBookRepository repo, IOrderHandler handler)
        {
            repository = repo;
            orderHandler = handler;
        }

        public ActionResult Index(Basket basket, string returnUrl)
        {
            return View(new BasketIndexViewModel {
                Basket = basket,
                ReturnUrl = returnUrl
            });
        }

        public ActionResult AddToBasket(Basket basket, int bookId, string returnUrl)
        {
            Book book = repository.Books
                .FirstOrDefault(x => x.BookID == bookId);
            if(book != null)
            {
                basket.AddBookType(1, book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult RemoveFromBasket(Basket basket, int bookId, string returnUrl)
        {
            Book book = repository.Books
                .FirstOrDefault(x => x.BookID == bookId);
            if (book != null)
            {
                basket.RemoveBookType(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Basket basket)
        {
            return PartialView(basket);
        }

        public ActionResult MakeOrder()
        {
            return View(new DeliveryInfo());
        }

        [HttpPost]
        public ActionResult MakeOrder(Basket basket, DeliveryInfo deliveryInfo)
        {
            if (basket.Types.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderHandler.HandleOrder(basket, deliveryInfo);
                basket.Clear();
                return View("Succeeded");
            }
            else
            {
                return View(deliveryInfo);
            }
        }

        /*public Basket GetBasket()
        {
            Basket basket = (Basket)Session["Basket"];
            if(basket == null)
            {
                basket = new Basket();
                Session["Basket"] = basket;
            }
            return basket;
        }*/
    }
}