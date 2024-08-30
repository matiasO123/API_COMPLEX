using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(p => p.name).NotEmpty().WithMessage("{PropertyName} cannot be empty").MaximumLength(80).WithMessage("{PropertyName} cannot be longer than {Maxlenght} characters!");

            RuleFor(p => p.birthDate).NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(p => p.phone_number).NotEmpty().WithMessage("{PropertyName} cannot be empty").MaximumLength(80).WithMessage("{PropertyName} cannot be longer than {Maxlenght} characters!");

            RuleFor(p => p.adress).NotEmpty().WithMessage("{PropertyName} cannot be empty").MaximumLength(80).WithMessage("{PropertyName} cannot be longer than {Maxlenght} characters!");

            RuleFor(p => p.email).NotEmpty().WithMessage("{PropertyName} cannot be empty").MaximumLength(80).WithMessage("{PropertyName} cannot be longer than {Maxlenght} characters!");

        }
    }
}
