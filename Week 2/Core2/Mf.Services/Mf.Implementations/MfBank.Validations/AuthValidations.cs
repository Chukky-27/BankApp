using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MFCore.MFServices.MFImplementations.MFBankValidations
{
    public class AuthValidations
    {


        public static string GetValidFullName()
        {
            string fullName;
            do
            {
                Console.WriteLine("Please enter your Full Name (First and Last Name, starting with a capital letter)");
                fullName = Console.ReadLine().Trim();

                if (!IsValidFullName(fullName))
                {
                    Console.WriteLine("Invalid full name format. Full name should start with a capital letter for both first and last names.");
                }

            } while (!IsValidFullName(fullName));

            return fullName;
        }


        public static bool IsValidFullName(string fullName)
        {
            string[] nameParts = fullName.Split(' ');
            if (nameParts.Length > 1 )
            {
                foreach (string part in nameParts)
                {
                    if (string.IsNullOrWhiteSpace(part) || !char.IsUpper(part[0]) || part.Any(char.IsDigit))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }





        public static string GetValidEmail()
        {
            string email;
            do
            {
                Console.WriteLine("Enter your Email");
                email = Console.ReadLine().Trim();
            } while (!IsValidEmail(email));

            return email;
        }




        public static bool IsValidEmail(string email)
        {
            // Use regular expression for basic email validation
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            return Regex.IsMatch(email, emailPattern);
        }





        public static string GetValidPassword()
        {
            string password;
            do
            {
                Console.WriteLine("Enter your Password (at least 8 characters, including upper and lower case letters, and a digit)");
                password = Console.ReadLine();
            } while (!IsValidPassword(password));

            return password;
        }





        public static bool IsValidPassword(string password)
        {
            // Use regular expression for password validation
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{8,}$";

            return Regex.IsMatch(password, passwordPattern);
        }



    }
}
