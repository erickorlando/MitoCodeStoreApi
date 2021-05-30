using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MitoCodeStore.DataAccess;
using MitoCodeStore.Entities;

namespace MitoCodeStorexUnitTest
{
    public class DbContextUnitTest : IDisposable
    {
        protected readonly MitoCodeStoreDbContext _context;

        public DbContextUnitTest()
        {
            var options = new DbContextOptionsBuilder<MitoCodeStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new MitoCodeStoreDbContext(options);

            _context.Database.EnsureCreated();

            Seed(_context);
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

        public void Dispose()
        {
            _context?.Database.EnsureDeleted();
            _context?.Dispose();
        }
    }
}