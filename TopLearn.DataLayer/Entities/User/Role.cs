using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.User
{
    public class Role
    {
        public Role()
        {

        }

        [Key]
        public int RoleId { get; set; }
        [Display(Name = "")]
        [Required(ErrorMessage = "لطفا {} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string RoleTitle { get; set; }

        #region relations

        public List<UserRole> UserRoles { get; set; }

        #endregion
    }
}
