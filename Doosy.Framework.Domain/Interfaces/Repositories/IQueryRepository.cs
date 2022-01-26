using System.Collections.Generic;

namespace Doosy.Framework.Domain
{
    public interface IQueryRepository<Ent,F> where Ent: Entity where F :FilterRequest

    {
        IEnumerable<Ent> Filter(F filter);
        Ent GetById(object id);
    }
}
