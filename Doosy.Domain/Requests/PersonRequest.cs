using System;
using System.ComponentModel.DataAnnotations;
using Doosy.Domain.Enum;
using Doosy.Framework.Domain;

namespace Doosy.Domain.Requests
{
    public class PersonRequest: RequestBase
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}
