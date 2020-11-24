using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Genarator;
using TopLearn.Core.Security;
using TopLearn.Core.Services;
using TopLearn.DataLayer.Entities.User;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using TopLearn.Core.Senders;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        PersianCalendar persianDate = new PersianCalendar();
        private IUserService _userService;
        private IViewRenderService _viewRenderService;

        public AccountController(IUserService userService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
        }
        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {   //agar etebar sangi haye man anjam dade nashode bod return kon view
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (_userService.IsExistUsername(register.Username))
            {
                ModelState.AddModelError("username", "نام کاربری معتبر نمی باشد");
                return View(register);

            }
            if (_userService.IsExistEmail(FixText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }
            User user = new User();
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            user.Email = FixText.FixEmail(register.Email);
            user.Username = register.Username;
            user.IsActive = false;
            user.Password = HashCodeHelper.HashPassword(register.Password);
            user.RegisterDate = DateTime.Now;
            user.UserAvatar = "Defult.jpg";

            _userService.AddUser(user);
            #region SendActivationEmail

            string body = _viewRenderService.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعال سازی تاپ لرن", body);
           



            #endregion
            return View("SuccessRegister", user);
        }
        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {    //etelaate dariyafti az karbar
                    var claims = new List<Claim>() {
                      new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                      new Claim(ClaimTypes.Name,user.Username)

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    //Login negah dashtan karbar
                    var properties = new AuthenticationProperties
                    {
                        //agar tik mara be khater besparid ro zade bod 
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);
                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد!");
                }

            }
            ModelState.AddModelError("Email", "کاربری با این مشخصات یافت نشد");
            return View();
        }
        #endregion

        #region ActiveAccount
        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }
        #endregion
        #region LogOut

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        #endregion
        #region ForgotPassword
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotViewModel forgotView)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotView);
            }
            string fixemail = FixText.FixEmail(forgotView.Email);
            User user = _userService.GetUserByEmail(fixemail);
            if (user==null)
            {
                ModelState.AddModelError("Email", "کاربری یافت نشد");
                return View(forgotView);
            }
            string body = _viewRenderService.RenderToStringAsync("_ForgotPasswordEmail", user);
            SendEmail.Send(user.Email, "تغییر کلمه عبور", body);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion
        #region ResetPassword   
        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            }) ;
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            var user = _userService.GetUserByActiveCode(resetPassword.ActiveCode);
            if (user==null)
            {
                return NotFound();
            }
            user.Password = HashCodeHelper.HashPassword(resetPassword.Password);
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            _userService.UpdateUser(user);
            return Redirect("/Login");
        }
        #endregion
    }
}
