using Core.Session;
using Data.Entities;
using MFCore.MFServices.MfInterfaces;
using MFCore.MFServices.MFImplementations.MFBankValidations;
using MFCore.MFServices;
using Core.AppDashboard;

namespace MFCore.MFServices.MFImplementations
{
    public class Auth : IAuth
    {


        public static List<User> users = new List<User>() { };


        public void SignUp()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("=============>SIGNUP PORTAL<===============");
            Console.ResetColor();
            string fullName = AuthValidations.GetValidFullName();
            string email = AuthValidations.GetValidEmail();
            string password = AuthValidations.GetValidPassword();

            var createdUser = new User()
            {
                Id = Guid.NewGuid(),
                FullName = fullName,
                Email = email,
                Password = password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            users.Add(createdUser);
            Console.WriteLine("Registration was successful");
            Console.Clear();
            LogIn();
        }



        public User LogIn()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("|=============||LOGIN PORTAL||==============|");
            Console.ResetColor();
            string email = AuthValidations.GetValidEmail();
            string password = AuthValidations.GetValidPassword();
            var userExist = users.Find(user => user.Email == email && user.Password == password);

            if (userExist != null)
            {
                Console.WriteLine("Logged in successfully");
                UserSession.loggedInUser = userExist;
                UserDashboard dash = new UserDashboard();
                dash.MyDashboard(UserSession.loggedInUser);
            }
            else
            {
                Console.WriteLine("Invalid Credentials");
                LogIn();
            }
            return userExist;

        }


        public void LogOut()
        {
            UserSession.loggedInUser = null;
            Console.WriteLine("Logged Out ");
        }





    }
}
