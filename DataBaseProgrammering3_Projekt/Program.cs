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
            Console.Write("Indtast Telefon navn: ");
            string telefon = Console.ReadLine();


            //AddPhone();
            //ShowPhones();
            showPhone(telefon);
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

        static void showPhone(string phone)
        {
            using (var context = new ShopContext())
            {
                var test = context.Companies
                    .Include(x => x.Phone)
                    .Where(x => x.CompanyName == phone)
                    .ToList();

                foreach (var item in test)
                {
                    Console.WriteLine($"Firma: {item.CompanyName}");
                    foreach (var telefon in item.Phone)
                    {
                        Console.WriteLine($"\tTelefon: {telefon.PhoneName}");
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
