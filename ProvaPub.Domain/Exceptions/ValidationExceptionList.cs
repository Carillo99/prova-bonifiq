

using FluentValidation.Results;

namespace ProvaPub.Domain.Exceptions
{
    public class ValidationExceptionList : Exception
    {
        public List<Exception> ValidationExceptions { get; set; }
        public ValidationExceptionList()
        {
            ValidationExceptions = new List<Exception>();
        }

        public ValidationExceptionList(string errorMessage)
        {
            ValidationExceptions = new List<Exception>
            {
                new Exception(errorMessage)
            };
        }

        public ValidationExceptionList(ValidationResult validationResult)
        {
            ValidationExceptions = new List<Exception>();

            foreach (var err in validationResult.Errors)
            {
                ValidationExceptions.Add(new Exception(err.ErrorMessage));
            }
        }
    }
}
