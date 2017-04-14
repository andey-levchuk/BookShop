using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Domain.Entities;

namespace BookShop.WebUI.Infrastructure.Binders
{
    public class BasketModelBinder : IModelBinder
    {
        private const string sessionKey = "Basket";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // To recieve object Basket from the session
            Basket basket = null;
            if (controllerContext.HttpContext.Session != null)
            {
                basket = (Basket)controllerContext.HttpContext.Session[sessionKey];
            }

            // To create object Basket if it is not detected in the session
            if (basket == null)
            {
                basket = new Basket();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = basket;
                }
            }

            return basket;
        }

    }
}