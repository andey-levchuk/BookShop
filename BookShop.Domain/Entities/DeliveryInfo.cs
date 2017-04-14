using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Domain.Entities
{
    public class DeliveryInfo
    {
        [Required(ErrorMessage = "Укажите Ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите контактный телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [Display(Name = "Страна:")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город:")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите улицу")]
        [Display(Name = "Улица:")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Укажите дом")]
        [Display(Name = "Дом:")]
        public string House { get; set; }
        [Required(ErrorMessage = "Укажите квартиру")]
        [Display(Name = "Квартира:")]
        public string Flat { get; set; }

        public bool GiftWrap { get; set; }
    }
}
