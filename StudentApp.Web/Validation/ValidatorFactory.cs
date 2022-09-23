using FluentValidation;
using SchoolApp.Web.Model;
using SchoolApp.Web.Models;
using System;
using System.Collections.Generic;

namespace SchoolApp.Web.Validation
{
    
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private static Dictionary<Type, IValidator> _validators = new Dictionary<Type, IValidator>();

        static ValidatorFactory()
        {

            _validators.Add(typeof(IValidator<StudentsViewModel>), new AddEditFulentValidation());
            _validators.Add(typeof(IValidator<DepartmentViewModel>), new AddEditFulentValidation());

        }

        /// <summary>
        /// Creates an instance of a validator with the given type.
        /// </summary>
        /// <param name="validatorType">Type of the validator.</param>
        /// <returns>The newly created validator</returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator;
            if (_validators.TryGetValue(validatorType, out validator))
                return validator;
            return validator;
        }
    }
}