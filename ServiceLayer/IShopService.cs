using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IShopService
    {
        Task<int> Commit();
        Task<List<Phone>> GetPhonesAsync(string SearchString, string FirmaNavn, string SortPhone/*, int CurrentPage, int PageSize*/);
        Task<List<Phone>> GetPhoneListAsync(int CurrentPage, int PageSize);
        Task<List<string>> GetPhoneFirma();
        Task<int> CreatePhone(Phone phone);
        Task<Phone> GetEditAsync(int? id);
        void CheckState(Phone phone);
        bool CheckExist(int id);
        //Task<List<Phone>> GetPaginatedResult(int currentPage, int pageSize = 3);
        Task<int> GetCount();
        Task<int> GetPageCount();
        Task<Phone> FindPhone(int? id);
        void RemovePhone(Phone phone);
    }
}
