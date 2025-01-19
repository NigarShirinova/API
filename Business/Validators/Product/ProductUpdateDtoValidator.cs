﻿using Business.Dtos.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Product
{
	public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
	{
		public ProductUpdateDtoValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.WithMessage("Ad vacibdir");

			RuleFor(x => x.Quantity)
				.GreaterThan(0)
				.WithMessage("Miqdar 0dan boyuk olmalidir");

			RuleFor(x => x.Price)
				.GreaterThan(0)
				.WithMessage("Qiymet 0dan boyuk olmalidir");

			RuleFor(x => x.Description)
				.MinimumLength(20)
				.MaximumLength(200)
				.WithMessage("Description 20-200 simvol arasinda olmalidir");

			RuleFor(x => x.Type)
				.IsInEnum()
				.WithMessage("Tip yalnisdir");
		}

		
		}
	}
