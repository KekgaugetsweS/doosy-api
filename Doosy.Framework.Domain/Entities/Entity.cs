using System;
namespace Doosy.Framework.Domain
{
    public class Entity
    {
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public EntityStatus Status { get; set; }
    }
}
