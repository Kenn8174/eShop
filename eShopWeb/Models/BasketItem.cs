using ServiceLayer.ShopService.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopWeb.Models
{
    public class BasketItem
    {
        public Basket Kurven { get; set; }
        public ShopDTO Butikken { get; set; }
    }
}
