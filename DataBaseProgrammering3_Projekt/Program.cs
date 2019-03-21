using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseProgrammering3_Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            AddPhone();
            ShowPhones();
        }

        static void ShowPhones()
        {
            using (var context = new ShopContext())
            {
                var phone = context.Companies
                    .Include(i => i.Phone)
                    .ToList();

                foreach (var comp in phone)
                {
                    Console.WriteLine($"Company: {comp.CompanyName}");
                    foreach (var telefon in comp.Phone)
                    {
                        Console.WriteLine($"\tPhone name: {telefon.PhoneName} - Price: {telefon.Price}");
                    }
                }
            }
        }

        static void AddPhone()
        {
            using (var context = new ShopContext())
            {
                var addPhone = new Company
                {
                    CompanyName = "OnePlus",
                    Phone = new List<Phone>
                    {
                        new Phone {PhoneName = "6T", Price = 3599.99M},
                        new Phone {PhoneName = "6", Price = 3199.99M},
                        new Phone {PhoneName = "5T", Price = 2499.99M},
                        new Phone {PhoneName = "5", Price = 1999.99M}
                    }
                };

                context.Companies.Add(addPhone);
                context.SaveChanges();
            }
        }
    }
}
