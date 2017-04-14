using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShop.Domain.Entities;

namespace BookShop.WebUI.Models
{
    public class BasketIndexViewModel
    {
        public Basket Basket { get; set; }
        public string ReturnUrl { get; set; }
    }
}