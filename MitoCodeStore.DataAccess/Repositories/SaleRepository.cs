using Microsoft.EntityFrameworkCore;
using MitoCodeStore.Entities;
using MitoCodeStore.Entities.Complex;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MitoCodeStore.DataAccess.Repositories
{
    public class SaleRepository : RepositoryContextBase<Sale>, ISaleRepository
    {
        public SaleRepository(MitoCodeStoreDbContext context) : base(context)
        {
        }

        public async Task<(ICollection<InvoiceInfo> collection, int total)> SelectAsync(string dni,
            int page, int rows)
        {
            return await ListCollectionAsync(GetSelector(),
                p => p.Customer.NumberId == dni,
                page,
                rows);
        }

        public async Task<(ICollection<InvoiceInfo> collection, int total)> SelectAsync(DateTime dateInit, DateTime dateEnd, int page, int rows)
        {
            return await ListCollectionAsync(GetSelector(),
                p => dateInit <= p.Date
                     && dateEnd >= p.Date,
                page,
                rows);
        }

        public async Task<(ICollection<InvoiceInfo> collection, int total)> SelectByInvoiceNumberAsync(string invoiceNumber, int page, int rows)
        {
            return await ListCollectionAsync(GetSelector(),
                p => p.InvoiceNumber.Contains(invoiceNumber),
                page,
                rows);
        }

        private static Expression<Func<Sale, InvoiceInfo>> GetSelector()
        {
            return p => new InvoiceInfo
            {
                Id = p.Id,
                CustomerName = p.Customer.Name,
                InvoiceNumber = p.InvoiceNumber,
                SaleDate = p.Date,
                TotalAmount = p.TotalAmount
            };
        }

        public async Task<ICollection<InvoiceDetailInfo>> GetSaleDetails(int saleId)
        {
            var collection = Context.SaleDetails.FromSqlRaw("EXEC uspSelectSaleDetails {0}", saleId);
            return await collection.ToListAsync();
        }

        public async Task<ICollection<ReportByMonthInfo>> SelectReportByMonthAsync(int month)
        {
            var collection = Context.ReportByMonth.FromSqlRaw("EXEC uspReportByMonth {0}", month);

            return await collection.ToListAsync();
        }

        public async Task<Sale> CreateAsync(Sale entity)
        {
            var number = await Context.Set<Sale>().CountAsync();
            entity.InvoiceNumber = $"F{number:0000}";

            await Context.Database.BeginTransactionAsync();

            await Context.Set<Sale>().AddAsync(entity);
            Context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task CreateSaleDetail(SaleDetail entity)
        {
            await Context.Set<SaleDetail>().AddAsync(entity);
            Context.Entry(entity).State = EntityState.Added;
        }

        public async Task CommitTransaction()
        {
            await Context.SaveChangesAsync();
            await Context.Database.CommitTransactionAsync();
        }
    }
}