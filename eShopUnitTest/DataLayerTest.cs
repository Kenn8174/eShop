using DataLayer;
using eShopUnitTest.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace eShopUnitTest
{
    [TestClass]
    public class DataLayerTest
    {
        [TestMethod]
        public void A_Test_Phone_Equal_3()
        {
            using (var db = new ShopContext(SqlContext.TestDbContextOptions()))
            {
                //Assign

                //Act
                var phone = db.Phones.Include(x => x.Company).Include(x => x.Photo).ToList();

                //Assert
                Assert.AreEqual(3, phone.Count());
            }
        }

        [TestMethod]
        public void B_Test_Phone_Company_Contain()
        {
            using (var db = new ShopContext(SqlContext.TestDbContextOptions()))
            {
                //Assign


                //Act
                var phone = db.Companies.ToList();

                //Assert
                Assert.AreEqual("Apple", phone.First().CompanyName);
            }
        }
    }
}
