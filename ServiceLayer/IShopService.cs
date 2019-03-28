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
    }
}
