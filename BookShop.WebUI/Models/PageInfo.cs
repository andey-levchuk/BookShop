using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.WebUI.Models
{
    public class PageInfo
    {
        public int BooksQuantity { get; set; }
        public int BooksOnPage { get; set; }
        public int CurrentPage { get; set; }
        public int PagesQuantity
        {
            get 
            { return (int)Math.Ceiling((decimal)BooksQuantity / BooksOnPage); }
        }
    }
}