using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using ServiceLayer;

namespace eShopWeb.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IShopService _shopservice;
        private readonly IMemoryCache _cache;

        public CreateModel(IShopService shopservice, IMemoryCache cache)
        {
            _shopservice = shopservice;
            _cache = cache;
        }

        [TempData]
        public string TempTest { get; set; }

        [BindProperty]
        public Phone Phone { get; set; }

        public SelectList Firma { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Firma = new SelectList(await _shopservice.GetPhoneFirma());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _cache.Remove("PhoneKey");

            await _shopservice.CreatePhone(Phone);

            await _shopservice.Commit();

            TempTest = Phone.PhoneName + " Blev oprettet";

            return RedirectToPage("/Index");
        }

    }
}