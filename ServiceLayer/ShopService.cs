using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<string>> GetPhoneFirma()
        {
            IQueryable<string> firmaQuery = from f in _shopcontext.Companies
                                            orderby f.CompanyName
                                            select f.CompanyName;

            return await firmaQuery.Distinct().ToListAsync();
        }

        public async Task<int> CreatePhone(Phone phone)
        {
            await _shopcontext.Phones.AddAsync(phone);
            return 0;
        }

        public async Task<Phone> GetEditAsync(int? id)
        {
            return await _shopcontext.Phones.Include(x => x.Company).FirstOrDefaultAsync(p => p.PhoneID == id);
        }

        public async Task<int> Commit()
        {
            await _shopcontext.SaveChangesAsync();
            return 0;
        }

        public void CheckState(Phone phone)
        {
             _shopcontext.Attach(phone).State = EntityState.Modified;
        }

        public bool CheckExist(int id)
        {
            return  _shopcontext.Phones.Any(p => p.PhoneID == id);
        }

        public async Task<Phone> FindPhone(int? id)
        {
            return await _shopcontext.Phones.FindAsync(id);
        }

        public void RemovePhone(Phone phone)
        {
            _shopcontext.Phones.Remove(phone);
        }
    }
}
