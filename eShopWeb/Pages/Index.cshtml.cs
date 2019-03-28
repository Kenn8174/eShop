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
    public class IndexModel : PageModel
    {
        private readonly IShopService _shopservice;

        public IndexModel(IShopService shopservice)
        {
            _shopservice = shopservice;
        }

        public List<Phone> Phone { get; set; }

        public async Task OnGetAsync()
        {
            Phone = await _shopservice.GetPhonesAsync();
        }
    }
}
