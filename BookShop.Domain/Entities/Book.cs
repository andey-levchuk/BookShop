using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Domain.Entities
{
    public class Book
    {
        public int BookID { get; set; }
        [Display(Name = "Название книги")]
        [Required]
        public string BookName { get; set; }
        [Display(Name = "Автор")]
        [Required]
        public string Author { get; set; }
        [Display(Name = "Описание")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Категория")]
        [Required]
        public string Category { get; set; }
        [Display(Name = "Цена, руб.")]
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        public byte[] ImageData { get; set; }
        [Display(Name = "Картинка")]
        public string ImageType { get; set; }
    }
}
