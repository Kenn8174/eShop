using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ShopService : IShopService
    {
        private readonly ShopContext _shopcontext;

        public ShopService(ShopContext shopcontext)
        {
            _shopcontext = shopcontext;
        }


        public async Task<List<Phone>> GetPhonesAsync()
        {
            var telefoner = _shopcontext.Phones
                .Include(x => x.Company)
                .Include(x => x.Photo);

            return await telefoner.ToListAsync();
        }

        public async Task<int> Commit()
        {
            await _shopcontext.SaveChangesAsync();
            return 0;
        }
    }
}
