using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.exception
{
    public class ValidationException: Exception
    {

        public List<string> errors;
        public ValidationException() :base("One or more validation errors")
        {
            errors = new List<string>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                errors.Add(failure.ErrorMessage);
            }


        }
    }
}
