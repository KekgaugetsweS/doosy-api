using System.ComponentModel.DataAnnotations;

namespace Doosy.Framework.Domain
{
    public class RequestBase
    {
        [Required]
        public string UserId { get; set; }

        public string Id { get; set; }
    }
}
