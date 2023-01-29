using System;
using System.Collections.Specialized;
using FluentValidation.Results;

namespace HotelManagement.Exception
{
    public class ValidationException : ApplicationException
    {
		public NameValueCollection Errors { get; private set; }
        
		public ValidationException(string message) : base(message) { Errors = new NameValueCollection(); }
        public ValidationException(string message, NameValueCollection errors) : base(message) { Errors = errors; }
        public ValidationException(string message, List<ValidationFailure> validationErrors) : base(message)
        {
            var errors = new NameValueCollection();
            foreach(var validationError in validationErrors)
            {
                errors.Add(validationError.ErrorMessage, validationError.PropertyName);
            }
            Errors = errors;
        }
    }
}