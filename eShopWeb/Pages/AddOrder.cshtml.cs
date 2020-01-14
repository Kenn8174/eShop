using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using eShopWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ServiceLayer;
using ServiceLayer.ShopService;

namespace eShopWeb.Pages
{
    public class AddOrderModel : PageModel
    {
        private readonly IShopService _shopService;

        public AddOrderModel(IShopService shopService)
        {
            _shopService = shopService;
        }

        public List<Basket> BasketList { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPostDeleteOrder(int id)
        {
            BasketList = JsonConvert.DeserializeObject<List<Basket>>(HttpContext.Session.GetString("Basket"));
            Basket exist = BasketList.Find(x => x.ProductID == id);
            BasketList.Remove(exist);
            HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(BasketList));

            return RedirectToPage("./Index");
        }

        public IActionResult OnPostAddOrder(int id)
        {
            BasketList = new List<Basket>();

            if (HttpContext.Session.GetString("Basket") == null)
            {
                BasketList.Add(new Basket
                {
                    ProductID = id,
                    Amount = 1
                });

                HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(BasketList));
            }

            else
            {
                BasketList = JsonConvert.DeserializeObject<List<Basket>>(HttpContext.Session.GetString("Basket"));
                Basket exist = BasketList.Find(i => i.ProductID == id);

                if (exist != null)
                {
                    BasketList.Remove(exist);
                    exist.Amount++;
                    BasketList.Add(exist);
                }

                else
                {
                    BasketList.Add(new Basket
                    {
                        ProductID = id,
                        Amount = 1
                    });
                }

                HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(BasketList));

            }

            return RedirectToPage("./Index");
        }
    }
}