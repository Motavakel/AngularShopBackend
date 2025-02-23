using Application.Features.Account.Commands.RegisterUser;
using FluentValidation;

namespace Application.Features.Account.Validators;

public class RegisterCommandValidators : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidators()
    {
        RuleFor(x => x.DisplayName).NotEmpty().WithMessage("وارد کردن نام و نام خانوداگی الزامی است");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("وارد کردن شماره همراه الزامی است")
            .Matches(@"^\d{11}$").WithMessage("شماره همراه باید شامل 11 رقم باشد");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("وارد کردن رمز ورود الزامی است")
            .MinimumLength(6).WithMessage("رمز عبور باید حداقل ۶ کاراکتر باشد.");
    }
}
