using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer;

namespace eShopWeb.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IShopService _shopservice;

        public CreateModel(IShopService shopservice)
        {
            _shopservice = shopservice;
        }

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

            await _shopservice.CreatePhone(Phone);

            await _shopservice.Commit();

            return RedirectToPage("/Index");
        }

    }
}