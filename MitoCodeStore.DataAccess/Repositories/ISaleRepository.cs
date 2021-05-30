﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.Entities;
using MitoCodeStore.Entities.Complex;

namespace MitoCodeStore.DataAccess.Repositories
{
    public interface ISaleRepository
    {
        Task<(ICollection<InvoiceInfo> collection, int total)> SelectAsync(string dni, int page, int rows);
        Task<(ICollection<InvoiceInfo> collection, int total)> SelectAsync(DateTime dateInit, DateTime dateEnd, int page, int rows);
        Task<(ICollection<InvoiceInfo> collection, int total)> SelectByInvoiceNumberAsync(string invoiceNumber, int page, int rows);
        Task<int> CreateAsync(Sale entity);
        Task CreateSaleDetail(SaleDetail entity);
        Task CommitTransaction();
    }
}