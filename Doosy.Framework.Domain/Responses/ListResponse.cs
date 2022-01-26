using System.Collections.Generic;

namespace Doosy.Framework.Domain
{

    public class ListResponse<T> : ResponseBase where T :class {

        public List<T> Data { get; set; }
    }
}
