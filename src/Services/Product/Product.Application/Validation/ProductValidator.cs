using FluentValidation;
using Product.Application.DTOs;

namespace Product.Application.Validation;
public class ProductValidator : AbstractValidator<ProductsDTO>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty()
               .Length(1, 40).WithMessage("Product title must be less than 200 characters.");
        RuleFor(p => p.Price).NotEmpty();
    }
}
