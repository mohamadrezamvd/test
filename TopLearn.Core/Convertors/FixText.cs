﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TopLearn.Core.Convertors
{
   public class FixText
    {
        public static string FixEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
