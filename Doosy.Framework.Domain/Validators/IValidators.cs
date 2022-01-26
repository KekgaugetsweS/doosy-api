using System;
using System.Collections.Generic;

namespace Doosy.Framework.Domain
{
   
    public abstract class ValidatorBase<T> : IValidatorBase<T> where T : RequestBase
    {
        public ValidatorBase()
        {

        }

        public List<string> Validate(T data)
        {
            List<string> errorMessages = new List<string>();

            errorMessages = CustomValidatio(data);

            return errorMessages;
        }

        protected abstract List<string> CustomValidatio(T data);
        
    }
}
