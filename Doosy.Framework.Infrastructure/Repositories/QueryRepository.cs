using System.Collections.Generic;
using Doosy.Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace Doosy.Infrastructure.Repositories
{
    public abstract class QueryRepository<T, F> : IQueryRepository<T, F> where T : Entity where F : FilterRequest
    {
        protected DbContext context;

        public QueryRepository(DbContext context)
        {
            this.context = context;
        }
        public IEnumerable<T> Filter(F filter)
        {
            return context.Set<T>();
        }
        public virtual T GetById(object id)
        {
            var item = context.Set<T>().Find(id);
            return item;
        }
    }
}
