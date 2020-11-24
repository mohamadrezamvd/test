using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Genarator;
using TopLearn.Core.Security;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Service
{
   public class UserServices   :IUserService
    {
        private readonly TopLearnContext _db;

        public UserServices(TopLearnContext db)
        {
            _db = db;
        }
        public bool IsExistUsername(string username)
        {
            return _db.Users.Any(u => u.Username == username);
        }

        public bool IsExistEmail(string Email)
        {
            return _db.Users.Any(u=>u.Email==Email);
        }

        public int AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user.UserId;
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = HashCodeHelper.HashPassword(login.Password);
            string email = FixText.FixEmail(login.Email);
            return _db.Users.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
        }

        public bool ActiveAccount(string ActiveCode)
        {
            var user = _db.Users.SingleOrDefault(u => u.ActiveCode == ActiveCode);
            if (user==null|user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            _db.SaveChanges();
            return true;
        }

        public User GetUserByEmail(string email)
        {
          return   _db.Users.SingleOrDefault(u => u.Email == email);
            
        }

        public User GetUserByActiveCode(string activecode)
        {
             User user = _db.Users.SingleOrDefault(u => u.ActiveCode == activecode);
            return user;
        }

        public void UpdateUser(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }
    }
}
