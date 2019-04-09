using DataLayer.Models;
using ServiceLayer.ShopService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ShopService
{
    public interface IShopService
    {
        Task<int> Commit();
        Task<List<Phone>> GetPhonesAsync(string SearchString, string FirmaNavn, string SortPhone/*, int CurrentPage, int PageSize*/);
        Task<List<Phone>> GetPhoneListAsync(int CurrentPage, int PageSize);
        //Task<List<string>> GetPhoneFirma();
        Task<IEnumerable<Company>> GetPhoneFirma();
        Task<int> CreatePhone(Phone phone);
        Task<Phone> GetEditAsync(int? id);
        Task UpdatePhone(Phone phone, int id);
        bool CheckExist(int id);
        //Task<List<Phone>> GetPaginatedResult(int currentPage, int pageSize = 3);
        Task<int> GetCount();
        Task<int> GetPageCount();
        Task<Phone> FindPhone(int? id);
        void RemovePhone(Phone phone);


        //API
        IQueryable<ShopDTO> GetPhones();
        void CreatePhonesAPI(Phone phone);
        void UpdatePhoneAPI(Phone phone);
    }
}
