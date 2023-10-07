using Data.Entities;
using MFCore.MFServices.MFImplementations;

namespace Core.AppDashboard
{
    public class UserDashboard
    {
        //private readonly ITransactions _transactions;

        //public Dashboard(ITransactions transactions)
        //{
        //    _transactions = transactions;
        //}
        //public Dashboard()
        //{

        //}
        public UserDashboard() 
        { 

        }

        public void MyDashboard(User loggedInUser)
        {
            var transact = new Transactions();
            Console.Clear();
            Console.WriteLine($"Welcome {loggedInUser.FullName}");
            Console.WriteLine("Press 1 to Create Account");
            Console.WriteLine("Press 2 to Deposit");
            Console.WriteLine("Press 3 to Withdraw");
            Console.WriteLine("Press 4 to Transfer");
            Console.WriteLine("Press 5 to Check Balance");
            Console.WriteLine("Press 6 to view accounts");
            Console.WriteLine("Press 7 to get Account Statement");
            Console.WriteLine("Press 8 to logout");

            string choice = Console.ReadLine();
            if (choice == "1")
            {

                transact.CreateAccount(loggedInUser);
                MyDashboard(loggedInUser);
            }
            else if (choice == "2")
            {
                transact.Deposit(loggedInUser);
                MyDashboard(loggedInUser);
            }
            else if (choice == "3")
            {
                transact.Withdraw(loggedInUser);
                MyDashboard(loggedInUser);
            }
            else if (choice == "4")
            {
                transact.Transfer(loggedInUser);
                MyDashboard(loggedInUser);
            }
            else if (choice == "5")
            {
                transact.CheckBalance(loggedInUser);
                MyDashboard(loggedInUser);
            }
            else if (choice == "6")
            {
                transact.GetAllAccounts(loggedInUser);
                MyDashboard(loggedInUser);
            }
            else if (choice == "7")
            {
                transact.GetMyAccountStatement(loggedInUser);
                MyDashboard(loggedInUser);
            }
            else if (choice == "8")
            {
                var rep = new Auth();
                rep.LogOut();
            }
            else {
                Console.WriteLine("Invalid option");
                Console.ReadKey();
                MyDashboard(loggedInUser);

            }
        }
    }
}
