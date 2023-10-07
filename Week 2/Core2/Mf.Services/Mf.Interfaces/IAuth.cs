using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFCore.MFServices.MfInterfaces
{
    public interface IAuth
    {
        public void SignUp();
        public User LogIn();
        public void LogOut();
    }
}
