using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Core.DTOs
{
    #region RegisterViewModel
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} نباید بدون مقدار باشد")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string Username { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} نباید بدون مقدار باشد")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل شما معتبر نیست")]

        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} نباید بدون مقدار باشد")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "مقدار {0} نباید کم تر از {1} کاراکتر باشد")]

        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "{0} نباید بدون مقدار باشد")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "مقدار {0} نباید کم تر از {1} کاراکتر باشد")]
        [Compare("Password", ErrorMessage = "کلمه های عبور همخوانی ندارند")]
        public string rePassword { get; set; }

    }
    #endregion

    #region LoginViewModel
    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} نباید بدون مقدار باشد")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل شما معتبر نیست")]

        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "مقدار {0} نباید کم تر از {1} کاراکتر باشد")]

        public string Password { get; set; }
        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
    }
    #endregion
    #region ForgotPasswordViewModel
    public class ForgotViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} نباید بدون مقدار باشد")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل شما معتبر نیست")]

        public string Email { get; set; }
    }


    #endregion
    #region ResetPassword
    public class ResetPasswordViewModel
    {
        public string ActiveCode { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} نباید بدون مقدار باشد")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "مقدار {0} نباید کم تر از {1} کاراکتر باشد")]

        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "{0} نباید بدون مقدار باشد")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "مقدار {0} نباید کم تر از {1} کاراکتر باشد")]
        [Compare("Password", ErrorMessage = "کلمه های عبور همخوانی ندارند")]
        public string rePassword { get; set; }
    }
    #endregion
}
