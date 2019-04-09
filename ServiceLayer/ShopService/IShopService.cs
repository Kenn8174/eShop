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
        /// <summary>
        /// SaveChanges Async
        /// </summary>
        /// <returns></returns>
        Task<int> Commit();

        /// <summary>
        /// Henter telefoner, med søgning, og filtrering
        /// </summary>
        /// <param name="SearchString"></param>
        /// <param name="FirmaNavn"></param>
        /// <param name="SortPhone"></param>
        /// <returns></returns>
        Task<List<Phone>> GetPhonesAsync(string SearchString, string FirmaNavn, string SortPhone/*, int CurrentPage, int PageSize*/);

        /// <summary>
        /// Henter telefoner til admin siden, og bruger paging
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        Task<List<Phone>> GetPhoneListAsync(int CurrentPage, int PageSize);

        //Task<List<string>> GetPhoneFirma();

        /// <summary>
        /// Henter alle Firmaer
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Company>> GetPhoneFirma();

        /// <summary>
        /// Opretter en telefon
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<int> CreatePhone(Phone phone);

        /// <summary>
        /// Henter telefon med bestemt ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Phone> GetEditAsync(int? id);

        /// <summary>
        /// Redigere telefon med bestemt ID
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task UpdatePhone(Phone phone, int id);

        /// <summary>
        /// Checker om telefonen existere
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> CheckExist(int id);

        //Task<List<Phone>> GetPaginatedResult(int currentPage, int pageSize = 3);
        
        //Task<int> GetCount();

        /// <summary>
        /// Henter antal af telefoner
        /// </summary>
        /// <returns></returns>
        Task<int> GetPageCount();

        /// <summary>
        /// Finder en telefon med bestemt ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Phone> FindPhone(int? id);

        /// <summary>
        /// Sletter en telefon
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task RemovePhone(Phone phone);


        //API
        IQueryable<ShopDTO> GetPhones();
        Task CreatePhonesAPI(Phone phone);
        Task UpdatePhoneAPI(Phone phone);
    }
}
