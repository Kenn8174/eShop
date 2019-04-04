using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer;

namespace eShopWeb.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IShopService _shopservice;

        public AdminModel(IShopService shopservice)
        {
            _shopservice = shopservice;
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; } = 5;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<Phone> Phone { get; set; }

        public async Task OnGetAsync()
        {
            Count = await _shopservice.GetPageCount();
            Phone = await _shopservice.GetPhoneListAsync(CurrentPage, PageSize);
            //Phone = await _shopservice.GetPaginatedResult(CurrentPage, PageSize);
        }
    }
}