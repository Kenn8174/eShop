using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopWeb.ViewComponents
{
    public class OrderCountViewComponent : ViewComponent
    {
        private readonly IShopService _shopService;

        public OrderCountViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count = await _shopService.GetCount();

            return View(count);
        }
    }
}
