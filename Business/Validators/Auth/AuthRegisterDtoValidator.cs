using Business.Dtos.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Auth
{
	public class AuthRegisterDtoValidator : AbstractValidator<AuthRegisterDto>
	{
		public AuthRegisterDtoValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email vacibdir")
				 .EmailAddress().WithMessage("Email yalnisdir");

			RuleFor(x => x.Password.Length)
				.GreaterThanOrEqualTo(8)
				.WithMessage("Password en az 8 simvol olmalidir");

			RuleFor(x => x.Password)
				.Equal(x => x.ConfirmPassword)
				.WithMessage("Password ve ConfirmPassword uyusmur");
		}
	}
}
