using System.Collections.Generic;
using System.Linq;
using Doosy.Domain.Entities;
using Doosy.Domain.Requests.Filters;
using Doosy.Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace Doosy.Infrastructure.Repositories.QueryRepository
{
    public class PersonQueryRepository :  QueryRepository<Person, PersonFilter>, IQueryRepository<Person, PersonFilter>
    {
        public PersonQueryRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Person> Filter(PersonFilter filter)
        {
            IQueryable<Person> personQuery = context.Set<Person>();

            if (!string.IsNullOrEmpty(filter.Firstname))
                personQuery = personQuery.Where(x => x.Firstname.Contains(filter.Firstname));

            if (!string.IsNullOrEmpty(filter.Surname))
                personQuery = personQuery.Where(x => x.Surname.Contains(filter.Surname));

            return personQuery.Where(x => x.Status == EntityStatus.Active);
        }

        public override Person GetById(object id)
        {
            var itemId = id.ToString();
            var item = context.Set<Person>().Where(x => x.Id == itemId).FirstOrDefault();
            return item;
        }

    }
}

