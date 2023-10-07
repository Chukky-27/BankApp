using MFCore.MFServices.MFImplementations.MFBankValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Test
{
    public class EmailTest
    {
        [Theory]
        [InlineData(true, "cart@gmail.com")]
        [InlineData(true, "Cart@gmail.com")]
        [InlineData(true, "cart27@gmail.com")]
        [InlineData(false, "cart!@gmail.com")]
        [InlineData(false, "cartgmail.com")]
        [InlineData(false, "cart @gmail.com")]
        public void IsValidEmail_TestCases(bool expected, string email)
        {
            var actual = AuthValidations.IsValidEmail(email);
            Assert.Equal(expected, actual);
        }
    }
}
