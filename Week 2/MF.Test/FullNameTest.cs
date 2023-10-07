using MFCore.MFServices.MFImplementations.MFBankValidations;

namespace MF.Test
{
    public class FullNameTest
    {
        [Fact]
        public void ValidFullName_Test()
        {
            // AAA

            // Arrange
            //var validation = new AuthValidations();
            var fullname = "John Doe";

            // Act
            var actual = AuthValidations.IsValidFullName(fullname);
            var expected = true;

            // Assert
            Assert.Equal(expected, actual);
        }

        //[Fact]
        //public void InvalidFullName_Test()
        //{
        //    // AAA

        //    // Arrange
        //    //var validation = new AuthValidations();
        //    var fullname = "JohnDoe";

        //    // Act
        //    var actual = AuthValidations.IsValidFullName(fullname);
        //    var expected = false;

        //    // Assert
        //    Assert.Equal(expected, actual);
        //}

        [Theory]
        [InlineData(true, "John Doe")]
        [InlineData(false, "John doe")]
        [InlineData(false, "john Doe")]
        [InlineData(false, "JohnDoe")]
        [InlineData(false, "123 Doe")]
        [InlineData(false, "John123 Doe")]
        [InlineData(false, "John123! Doe!")]
        public void FullName_TestCases(bool expected, string fullname)
        {
            // Act
            var actual = AuthValidations.IsValidFullName(fullname);
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}