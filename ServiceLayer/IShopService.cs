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
        Task<List<Phone>> GetPhonesAsync();
        Task<List<string>> GetPhoneFirma();
        Task<int> CreatePhone(Phone phone);
        Task<Phone> GetEditAsync(int? id);
        void CheckState(Phone phone);
        bool CheckExist(int id);

        Task<Phone> FindPhone(int? id);
        void RemovePhone(Phone phone);
    }
}
