using eShopWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer;
using ServiceLayer.ShopService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopWeb.ViewComponents
{
    public class KurvCountViewComponent : ViewComponent
    {
        private readonly IShopService _shopService;

        public KurvCountViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        //public int id { get; set; }

        public IViewComponentResult Invoke()
        {
            List<Basket> baskets = new List<Basket>();
            List<BasketItem> basketItems = new List<BasketItem>();

            if (HttpContext.Session.Get("Basket") != null)
            {
                try
                {
                    baskets = JsonConvert.DeserializeObject<List<Basket>>(HttpContext.Session.GetString("Basket"));
                }
                catch (Exception)
                {
                }
                foreach (Basket item in baskets)
                {
                    basketItems.Add(new BasketItem
                    {
                        Kurven = item,
                        Butikken = _shopService.GetPhones().Where(ll => ll.PhoneID == item.ProductID).First()
                    });
                }
            }

            return View(basketItems);
        }
    }
}
