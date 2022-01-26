using System;
using Doosy.Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace Doosy.Infrastructure.Repositories
{
    public  class CommandRepository<T> : ICommandRepository<T> where T : Entity
    {
        DbContext context;

        public CommandRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(T item)
        {
            
            item.DateCreated = DateTime.Now;
            item.Status = EntityStatus.Active;

            this.context.Set<T>().Add(item);
            this.context.SaveChanges();
        }

        public void Update(T item)
        {
            this.context.Set<T>().Update(item);
            this.context.SaveChanges();
        }
    }
}
