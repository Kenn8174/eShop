using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.ShopService;
using System;
using System.Linq;

namespace eShopUnitTest.Utilities
{
    public class InMemoryContext
    {
        //public static DbContextOptions<ShopContext> TestDbContextOptions()
        //{
        //    // Create a new service provider to create a new in-memory database.
        //    var serviceProvider = new ServiceCollection()
        //        .AddEntityFrameworkInMemoryDatabase()
        //        .BuildServiceProvider();

        //    // Create a new options instance using an in-memory database and 
        //    // IServiceProvider that the context should resolve all of its services from.
        //    var builder = new DbContextOptionsBuilder<ShopContext>()
        //        .UseInMemoryDatabase("InMemoryDb")
        //        .UseInternalServiceProvider(serviceProvider);

        //    return builder.Options;
        //}
    }
}
