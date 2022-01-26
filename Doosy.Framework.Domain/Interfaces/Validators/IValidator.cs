using System.Collections.Generic;

namespace Doosy.Framework.Domain
{
    public interface IValidatorBase<T> where T : RequestBase
    {
        List<string> Validate(T data);
    }
}
