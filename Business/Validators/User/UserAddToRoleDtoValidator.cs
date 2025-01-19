using Business.Dtos.UserRole;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.User
{
	public class UserAddToRoleDtoValidator : AbstractValidator<UserAddToRoleDto>
	{
		public UserAddToRoleDtoValidator()
		{
			RuleFor(x => x.UserId)
				.NotEmpty()
				.WithMessage("User vacibdir");

			RuleFor(x => x.RoleId)
				.NotEmpty()
				.WithMessage("Role vacibdir");
		}
	}
}
