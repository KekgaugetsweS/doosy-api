using System.Collections.Generic;

namespace Doosy.Framework.Domain
{
    public class ResponseBase
    {

        public ResponseBase()
        {
            Messages = new List<string>();
        }
        public bool IsSuccessful { get; set; }
        public List<string> Messages { get; set; }
    }
}
