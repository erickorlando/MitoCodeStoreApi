using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MitoCodeStore.DataAccess;
using MitoCodeStore.Entities;

namespace MitoCodeStoreUnitTests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void CustomerListTest()
        {
            // AAA
            // Arrangement
            var options = new DbContextOptionsBuilder<MitoCodeStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MitoCodeStoreDbContext(options);

            Seed(context);

            // Act

            var customers = context.Customers.ToList();
            var expected = 100;

            // Assert
            Assert.AreEqual(expected, customers.Count);
        }

        [TestMethod]
        public void Customer_ShouldCreateAPrimaryKetAtInsertTest()
        {
            // Arrangement
            var options = new DbContextOptionsBuilder<MitoCodeStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MitoCodeStoreDbContext(options);

            Seed(context);

            // Act

            var expected = 101;
            var customer = new Customer
            {
                Name = "Tony Stark",
                BirthDate = new DateTime(1958, 12, 7),
                NumberId = "535466"
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            // Assert

            Assert.AreEqual(expected, customer.Id);
        }

        private void Seed(MitoCodeStoreDbContext context)
        {
            var list = new List<Customer>();

            for (int i = 0; i < 100; i++)
            {
                list.Add(new Customer
                {
                    Name = $"Customer {i:000}",
                    BirthDate = new DateTime(new Random().Next(1950, 2000),
                        4,
                        4),
                    NumberId = $"DNI N° {i:0000}"
                });
            }

            context.Customers.AddRange(list);
            context.SaveChanges();
        }
    }
}
