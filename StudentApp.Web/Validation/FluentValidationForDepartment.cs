using FluentValidation;
using SchoolApp.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Web.Validation
{
    public class FluentValidationForDepartment : AbstractValidator<DepartmentViewModel>
    {
        public FluentValidationForDepartment()
        {
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage("DepartmentName is required");
            RuleFor(x => x.DepartmentDescription).NotEmpty().WithMessage("DepartmentDescription is required");
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive is required");
        }
    }
}