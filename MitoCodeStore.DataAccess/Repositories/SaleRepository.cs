using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MitoCodeStore.Entities;
using MitoCodeStore.Entities.Complex;

namespace MitoCodeStore.DataAccess.Repositories
{
    public class SaleRepository : RepositoryContextBase<Sale>, ISaleRepository
    {
        public SaleRepository(MitoCodeStoreDbContext context) : base(context)
        {
        }

        public async Task<(ICollection<InvoiceInfo> collection, int total)> SelectAsync(string dni, int page, int rows)
        {
            var tupla = await ListCollection(p => p.Customer.NumberId == dni,
                page,
                rows);

            var collection = GetTuple(tupla);

            return (collection, tupla.total);
        }

        public async Task<(ICollection<InvoiceInfo> collection, int total)> SelectAsync(DateTime dateInit, DateTime dateEnd, int page, int rows)
        {
            var tupla = await ListCollection(p => p.Date <= dateInit
                && p.Date >= dateEnd,
                page,
                rows);

            var collection = GetTuple(tupla);

            return (collection, tupla.total);
        }
        
        public async Task<(ICollection<InvoiceInfo> collection, int total)> SelectByInvoiceNumberAsync(string invoiceNumber, int page, int rows)
        {
            var tupla = await ListCollection(p => p.InvoiceNumber == invoiceNumber,
                page,
                rows);

            var collection = GetTuple(tupla);

            return (collection, tupla.total);
        }

        private static List<InvoiceInfo> GetTuple((ICollection<Sale> collection, int total) tupla)
        {
            var collection = tupla.collection
                .Select(p => new InvoiceInfo
                {
                    Id = p.Id,
                    CustomerName = p.Customer.Name,
                    InvoiceNumber = p.InvoiceNumber,
                    SaleDate = p.Date,
                    TotalAmount = p.TotalAmount
                })
                .ToList();
            return collection;
        }

        public async Task<int> CreateAsync(Sale entity)
        {
            await Context.Set<Sale>().AddAsync(entity);
            Context.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task CreateSaleDetail(SaleDetail entity)
        {
            await Context.Set<SaleDetail>().AddAsync(entity);
            Context.Entry(entity).State = EntityState.Added;
        }

        public async Task CommitTransaction()
        {
            await Context.SaveChangesAsync();
        }
    }
}