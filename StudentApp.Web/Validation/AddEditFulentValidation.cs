using FluentValidation;
using SchoolApp.Web.Models;

namespace SchoolApp.Web.Validation
{
    public class AddEditFulentValidation : AbstractValidator<StudentsViewModel>
    {
        public AddEditFulentValidation()

        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Location is required");
            RuleFor(x => x.FatherName).NotEmpty().WithMessage("FatherName is required");
            RuleFor(x => x.MotherName).NotEmpty().WithMessage("MotherName is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required");
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("DepartmentId is required");
            RuleFor(x => x.Class).NotEmpty().WithMessage("Class is required");
            RuleFor(x => x.Section).NotEmpty().WithMessage("Section is required");
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive is required");



        }
    }
}