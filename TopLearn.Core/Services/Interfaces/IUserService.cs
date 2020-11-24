using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public interface IUserService
    {
        bool IsExistUsername(string username);
        bool IsExistEmail(string Email);
        int AddUser(User user);
        User LoginUser(LoginViewModel login);
        bool ActiveAccount(string ActiveCode);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activecode);
        void UpdateUser(User user);

    }
}
