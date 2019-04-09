﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ServiceLayer;
using ServiceLayer.ShopService;

namespace eShopWeb.Pages
{
    public class EditModel : PageModel
    {
        private readonly IShopService _shopservice;
        private readonly IMemoryCache _cache;

        public EditModel(IShopService shopservice, IMemoryCache cache)
        {
            _shopservice = shopservice;
            _cache = cache;
        }

        [BindProperty]
        public Phone Phone { get; set; }
        public SelectList Firma { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            Firma = new SelectList(await _shopservice.GetPhoneFirma(), nameof(Company.CompanyID), nameof(Company.CompanyName));
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

            try
            {
                await _shopservice.UpdatePhone(Phone, Phone.PhoneID);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_shopservice.CheckExist(Phone.PhoneID) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            _cache.Remove("PhoneKey");
            return RedirectToPage("/Index");
        }
    }
}