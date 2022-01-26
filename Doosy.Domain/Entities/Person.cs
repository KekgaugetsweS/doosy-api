using Doosy.Domain.Enum;
using Doosy.Framework.Domain;
using System.ComponentModel.DataAnnotations;

namespace Doosy.Domain.Entities
{
    public class Person:Entity
    {

        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
    }
}
