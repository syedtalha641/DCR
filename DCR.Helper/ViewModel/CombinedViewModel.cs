﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCR.Helper.ViewModel
{
    public class CombinedViewModel
    {
        public LoginViewModel LoginViewModel { get; set; }
        public OTPViewModel OTPViewModel { get; set; }
        public PasswordUpdateViewModel PasswordUpdateViewModel { get; set; }


    }
}
