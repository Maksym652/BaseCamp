namespace WebApp.Api.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentValidation;
    using WebApp.Api.Requests;
    using WebApp.Core.Models;

    public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentRequestValidator()
        {
            this.RuleFor(x => x.Name).Length(0, 100);
            this.RuleFor(x => x.Group).GreaterThan(0);
            this.RuleFor(x => x.Specialty).GreaterThan(0);
        }
    }
}
