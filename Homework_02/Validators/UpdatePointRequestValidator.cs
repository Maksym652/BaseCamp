using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Api.Requests;

namespace WebApp.Api.Validators
{
    public class UpdatePointRequestValidator : AbstractValidator<UpdatePointRequest>
    {
        public UpdatePointRequestValidator()
        {
            this.RuleFor(x => x.Mark).GreaterThan(0);
            this.RuleFor(x => x.Mark).LessThanOrEqualTo(100);
            this.RuleFor(x => x.Task).MaximumLength(100);
        }
    }
}
