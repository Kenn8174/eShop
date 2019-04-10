using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ShopService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.ShopService
{
    public class ShopService : IShopService
    {
        private readonly ShopContext _shopcontext;

        public ShopService(ShopContext shopcontext)
        {
            _shopcontext = shopcontext;
        }


        public async Task<int> Commit()
        {
            await _shopcontext.SaveChangesAsync();
            return 0;
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
                telefoner = telefoner.Where(f => f.CompanyID == Convert.ToInt32(FirmaNavn));
            }

            if (!string.IsNullOrEmpty(SortPhone))
            {
                switch (SortPhone)
                {
                    case "PhoneName":
                        telefoner = telefoner.OrderBy(x => x.PhoneName);
                        break;

                    case "PhoneNameDESC":
                        telefoner = telefoner.OrderByDescending(x => x.PhoneName);
                        break;

                    case "CompanyName":
                        telefoner = telefoner.OrderBy(x => x.Company.CompanyName);
                        break;

                    case "CompanyNameDESC":
                        telefoner = telefoner.OrderByDescending(x => x.Company.CompanyName);
                        break;

                    case "Price":
                        telefoner = telefoner.OrderBy(x => x.Price);
                        break;

                    case "PriceDESC":
                        telefoner = telefoner.OrderByDescending(x => x.Price);
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

        //public async Task<List<string>> GetPhoneFirma()
        //{
        //    IQueryable<string> firmaQuery = from f in _shopcontext.Companies
        //                                    orderby f.CompanyName
        //                                    select f.CompanyName;

        //    return await firmaQuery.Distinct().ToListAsync();
        //}

        public async Task<IEnumerable<Company>> GetPhoneFirma()
        {
            return await _shopcontext.Companies.ToListAsync();
        }

        public async Task<int> CreatePhone(Phone phone)
        {
            await _shopcontext.Phones.AddAsync(phone);
            return 0;
        }

        public async Task<Phone> GetEditAsync(int? id)
        {
            return await _shopcontext.Phones.Include(x => x.Company).Include(x => x.Photo).FirstOrDefaultAsync(p => p.PhoneID == id);
        }

        public async Task UpdatePhone(Phone phone, int id)
        {
            //Virker ikke pga En til En relation med Photo!
            //_shopcontext.Attach(phone).State = EntityState.Modified;


            //Virker
            var edit = _shopcontext.Phones.Include(x => x.Company).Include(x => x.Photo).Where(x => x.PhoneID == id).First();

            edit.Price = phone.Price;
            edit.Photo.PhonePhoto = phone.Photo.PhonePhoto;
            edit.PhoneName = phone.PhoneName;
            edit.CompanyID = phone.CompanyID;

            await Commit();
        }

        public async Task<bool> CheckExist(int id)
        {
            return await _shopcontext.Phones.AnyAsync(p => p.PhoneID == id);
        }

        //public async Task<int> GetCount()
        //{
        //    var data = await _shopcontext.Orders.ToListAsync();
        //    return data.Count();
        //}

        public async Task<int> GetPageCount()
        {
            var data = await _shopcontext.Phones.ToListAsync();
            return data.Count();
        }

        public async Task<Phone> FindPhone(int? id)
        {
            return await _shopcontext.Phones.FindAsync(id);
        }

        public async Task RemovePhone(Phone phone)
        {
             _shopcontext.Phones.Remove(phone);
            await Commit();
        }


        //API

        public IQueryable<ShopDTO> GetPhones()
        {
            return (from p in _shopcontext.Phones
                    select new ShopDTO
                    {
                        PhoneID = p.PhoneID,
                        Price = p.Price,
                        PhoneName = p.PhoneName,
                        PhonePhoto = p.Photo.PhonePhoto,
                        CompanyName = p.Company.CompanyName
                    });
        }

        public async Task CreatePhonesAPI(Phone phone)
        {
            await _shopcontext.Phones.AddAsync(phone);
            await Commit();
        }

        public async Task UpdatePhoneAPI(Phone phone)
        {
            _shopcontext.Entry(phone).State = EntityState.Modified;
            await Commit();
        }
    }
}
