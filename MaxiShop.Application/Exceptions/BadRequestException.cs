using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public IDictionary<string, string[]> ValidationsErrors { get; set; }

        public BadRequestException(string message) : base(message)
        {
            
        }

        public BadRequestException(string message,ValidationResult validationResult) : base(message)
        {
            ValidationsErrors = validationResult.ToDictionary();
        }
    }
}
