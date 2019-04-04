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


        public async Task<List<Phone>> GetPhonesAsync(string SearchString, string FirmaNavn, string SortPhone/*, int CurrentPage, int PageSize*/)
        {
            //var telefoner = _shopcontext.Phones
            //    .Include(x => x.Company)
            //    .Include(x => x.Photo);

            var telefoner = from t in _shopcontext.Phones.Include(x => x.Company).Include(x => x.Photo) select t;

            if (!string.IsNullOrEmpty(SearchString))
            {
                telefoner = telefoner.Where(c => c.PhoneName.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(FirmaNavn))
            {
                telefoner = telefoner.Where(f => f.Company.CompanyName == FirmaNavn);
            }

            if (!string.IsNullOrEmpty(SortPhone))
            {
                switch (SortPhone)
                {
                    case "PhoneName":
                        telefoner = telefoner.OrderBy(x => x.PhoneName);
                        break;

                    case "CompanyName":
                        telefoner = telefoner.OrderBy(x => x.Company.CompanyName);
                        break;

                    case "Price":
                        telefoner = telefoner.OrderBy(x => x.Price);
                        break;
                }
            }

            //telefoner = telefoner.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

            return await telefoner.ToListAsync();
        }

        public async Task<List<Phone>> GetPhoneListAsync(int CurrentPage, int PageSize)
        {
            //var telefoner = from t in _shopcontext.Phones.Include(x => x.Company).Include(x => x.Photo) select t;
            var telefoner = await _shopcontext.Phones.Include(x => x.Company).Include(x => x.Photo).ToListAsync();
            telefoner = telefoner.OrderBy(x => x.PhoneID).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            return telefoner;
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

        //public async Task<int> AddOrder()
        //{
        //    await _shopcontext.Orders.AddAsync();
        //    return 0;
        //}

        public async Task<Phone> GetEditAsync(int? id)
        {
            return await _shopcontext.Phones.Include(x => x.Company).FirstOrDefaultAsync(p => p.PhoneID == id);
        }

        public async Task<int> GetCount()
        {
            var data = await _shopcontext.Orders.ToListAsync();
            return data.Count();
        }

        //public async Task<List<Phone>> GetPaginatedResult(int currentPage, int pageSize = 3)
        //{
        //    var data = await GetPhoneListAsync();
        //    //var data = await _shopcontext.Phones.ToListAsync();
        //    return data.OrderBy(x => x.PhoneID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        //}

        public async Task<int> GetPageCount()
        {
            //var data = await GetPhoneListAsync();
            var data = await _shopcontext.Phones.ToListAsync();
            return data.Count();
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
