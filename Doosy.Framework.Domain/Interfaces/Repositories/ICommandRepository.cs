namespace Doosy.Framework.Domain
{
    public interface ICommandRepository<Ent> where Ent:Entity
    {
        void Create(Ent ent);
        void Update(Ent ent);
    }
}
