using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MitoCodeStore.Entities;

namespace MitoCodeStore.DataAccess
{
    public class RepositoryContextBase<TEntityBase>
        where TEntityBase : EntityBase, new()
    {

        protected MitoCodeStoreDbContext Context;

        protected RepositoryContextBase(MitoCodeStoreDbContext context)
        {
            Context = context;
        }

        public virtual ICollection<TInfo> ListCollection<TInfo>(Expression<Func<TEntityBase, TInfo>> selector)
        where TInfo : class, new()
        {
            return Context.Set<TEntityBase>()
                .OrderBy(p => p.Id)
                .AsNoTracking()
                .Select(selector)
                .ToList();
        }

        public virtual async Task<(ICollection<TEntityBase> collection, int total)> ListCollection(
            Expression<Func<TEntityBase, bool>> predicate,
            int page,
            int rows)
        {
            var collection = await Context.Set<TEntityBase>()
                .Where(predicate).OrderBy(p => p.Id)
                .AsNoTracking()
                .Skip((page - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<TEntityBase>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection.ToList(), totalCount);

        }

        public virtual async Task<TEntityBase> Select(int id)
        {
            var entity = await Context.Set<TEntityBase>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (entity == null)
                throw new InvalidOperationException("El registro no existe!");

            return entity;
        }
    }
}