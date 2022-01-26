using System;
using Doosy.Framework.Domain;

namespace Doosy.Domain.Requests.Filters
{
    public class PersonFilter: FilterRequest
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }
}
