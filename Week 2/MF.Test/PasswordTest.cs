using MFCore.MFServices.MFImplementations.MFBankValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Test
{
    public class PasswordTest
    {
        [Theory]
        [InlineData(false, "cartusky2010")]
        [InlineData(false, "cart@2010")]
        [InlineData(false, "cart@gmail.com")]
        [InlineData(false, "cart")]
        [InlineData(true, "Cart20!#")]

        public void IsValidPassword_TestCases(bool expected, string password)
        {
            var actual = AuthValidations.IsValidPassword(password);
            Assert.Equal(expected, actual);
        }
    }
}
