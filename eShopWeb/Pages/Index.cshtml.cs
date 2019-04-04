using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ServiceLayer;

namespace eShopWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IShopService _shopservice;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public IndexModel(IShopService shopservice, IConfiguration configuration, IMemoryCache cache)
        {
            _shopservice = shopservice;
            _configuration = configuration;
            _cache = cache;
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; } = 5;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<Phone> Phone { get; set; }

        public Phone PhoneCompanies { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FirmaNavn { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortPhone { get; set; }

        public SelectList Firma { get; set; }

        public async Task<IActionResult> OnGetAsync(/*[FromServices] IDateTime dateTime*/)
        {
            //ViewData["Message"] = $"Current server time: {dateTime.Now}";
            //var settings = _configuration.GetSection("AppSettings").Get<AppSettings>();
            //ViewData["test"] = _configuration["AppSettings:Test"];


            //Phone = await _shopservice.GetPhonesAsync(SearchString, FirmaNavn, SortPhone/*, CurrentPage, PageSize*/);
            //Firma = new SelectList(await _shopservice.GetPhoneFirma());

            if (string.IsNullOrEmpty(FirmaNavn) && string.IsNullOrEmpty(SortPhone) && string.IsNullOrEmpty(SearchString))
            {

                Phone = await _cache.GetOrCreate("PhoneKey", async entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(2);
                    return await _shopservice.GetPhonesAsync(SearchString, FirmaNavn, SortPhone);
                });

            }
            else
            {
                _cache.Remove("PhoneKey");
                Phone = await _shopservice.GetPhonesAsync(SearchString, FirmaNavn, SortPhone);
            }

            Firma = await _cache.GetOrCreate("FirmaKey", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(2);
                return new SelectList(await _shopservice.GetPhoneFirma());
            });


            return Page();
        }

        //public async Task<IActionResult> OnGetAddAsync()
        //{
        //    await _shopservice.Commit();
        //    return Page();
        //}
    }
}
