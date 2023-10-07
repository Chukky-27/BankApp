using MFCore.MFServices.MfInterfaces;

namespace Core
{
    internal class MF_HomePage
    {
        private readonly IAuth _rep;

        public MF_HomePage(IAuth rep)
        {
            _rep = rep;
        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(">>>>>>>>>>>>>>>> WELCOME TO PATCALIS MFBANK >>>>>>>>>>>>>>>>\n");
            Console.ResetColor();

            

            string choice = string.Empty;

            do
            {
                {
                    Console.WriteLine("Press 1 to Register");
                    Console.WriteLine("Press 2 to Login");
                    Console.WriteLine("Press 3 to Exit\na");
                    Console.WriteLine("Enter a number");

                    choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        _rep.SignUp();
                    }
                    else if (choice == "2")
                    {
                        _rep.LogIn();
                    }
                    else if (choice == "3")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("You've entered an invalid input");
                        Console.ReadKey();

                    }
                }
                } while (choice != "3") ;

            

        }
    }
}
