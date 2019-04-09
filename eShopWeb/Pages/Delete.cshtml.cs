using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using eShopWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ServiceLayer;
using ServiceLayer.ShopService;

namespace eShopWeb.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IShopService _shopservice;
        private readonly IMemoryCache _cache;

        public DeleteModel(IShopService shopservice, IMemoryCache cache)
        {
            _shopservice = shopservice;
            _cache = cache;
        }

        [BindProperty]
        public Phone Phone { get; set; }
        public List<Basket> BasketList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Phone = await _shopservice.GetEditAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            if (Phone == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            if (HttpContext.Session.Get("Basket") != null)
            {
                BasketList = JsonConvert.DeserializeObject<List<Basket>>(HttpContext.Session.GetString("Basket"));
                Basket exist = BasketList.Find(i => i.ProductID == id);

                BasketList.Remove(exist);

                HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(BasketList));
            }


            if (id == null)
            {
                return NotFound();
            }

            Phone = await _shopservice.FindPhone(id);

            if (Phone != null)
            {
                await _shopservice.RemovePhone(Phone);
            }

            _cache.Remove("PhoneKey");
            return RedirectToPage("./Index");
        }
    }
}