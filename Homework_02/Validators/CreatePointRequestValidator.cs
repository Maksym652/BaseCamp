using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Api.Requests;

namespace WebApp.Api.Validators
{
    public class CreatePointRequestValidator : AbstractValidator<CreatePointRequest>
    {
        public CreatePointRequestValidator()
        {
            this.RuleFor(x => x.Mark).GreaterThan(0);
            this.RuleFor(x => x.Mark).LessThanOrEqualTo(100);
            this.RuleFor(x => x.StudentId).GreaterThan(0);
            this.RuleFor(x => x.Subject).MaximumLength(100);
            this.RuleFor(x => x.Task).MaximumLength(100);
        }
    }
}
