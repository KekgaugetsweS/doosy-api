using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Doosy.Domain.Requests;
using Doosy.Framework.Domain;

namespace Doosy.Domain.Validators
{
    public class PersonValidator : ValidatorBase<PersonRequest>, IValidatorBase<PersonRequest>
    {
        protected override List<string> CustomValidatio(PersonRequest data)
        {
            var errors = new List<string>();

            var ctx = new ValidationContext(data);

            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(data, ctx, results, true))
            {
                foreach (var error in results)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }
    }
}
