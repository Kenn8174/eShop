using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;

namespace eShopWeb.Pages
{
    public class EditModel : PageModel
    {
        private readonly IShopService _shopservice;

        public EditModel(IShopService shopservice)
        {
            _shopservice = shopservice;
        }

        [BindProperty]
        public Phone Phone { get; set; }
        public SelectList Firma { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            Firma = new SelectList(await _shopservice.GetPhoneFirma());
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _shopservice.CheckState(Phone);

            try
            {
                await _shopservice.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shopservice.CheckExist(Phone.PhoneID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Index");
        }
    }
}