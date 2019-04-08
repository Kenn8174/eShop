using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer;
using ServiceLayer.ShopService;

namespace eShopWeb.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IShopService _shopservice;

        public DeleteModel(IShopService shopservice)
        {
            _shopservice = shopservice;
        }

        [BindProperty]
        public Phone Phone { get; set; }

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
            if (id == null)
            {
                return NotFound();
            }

            Phone = await _shopservice.FindPhone(id);

            if (Phone != null)
            {
                _shopservice.RemovePhone(Phone);
                await _shopservice.Commit();
            }

            return RedirectToPage("./Index");
        }
    }
}