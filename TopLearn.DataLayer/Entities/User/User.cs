using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.User
{
    public class User
    {
        public User()
        {

        }

        [Key]
        public int UserId { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string Username { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "مقدار {0} نباید کم تر از {1} کاراکتر باشد")]

        public string Password { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل شما معتبر نیست")]
       
        public string Email { get; set; }
        
        [Display(Name = "کد فعال سازی")]
        [MaxLength(50, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string ActiveCode { get; set; }
        [Display(Name = "وضعیت")]
        
        public bool IsActive { get; set; }
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        [MinLength(11, ErrorMessage = "مقدار {0} نباید کم تر از {1} کاراکتر باشد")]
        [Phone(ErrorMessage = "فقط عدد می توانید وارد کنید")]

        public string UserAvatar { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }


        #region relations

        public List<UserRole> UserRoles { get; set; }

        #endregion
    }
}
