using Data.Entities;
using Data.Enum;
using MFCore.MFServices.MfInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MFCore.MFServices.MFImplementations
{
    public class Transactions : ITransactions
    {
        public static List<Account> AllAccounts = new List<Account>();

        public static List<AcctStatement> AllAccountStatements = new List<AcctStatement>() { };

        public void CreateAccount(User loggedInUser)
        {
            Console.WriteLine("Choose an Account type\n Press 1 for Current\n Press 2 for Savings");

            string choice = Console.ReadLine();

            var random = new Random();
            var AccNo = random.Next(1504070908, 2099999999).ToString(); //tells the range of random numbers you want to generate.

            if (choice == "1")
            {

                var createAccount = new Account()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = AccNo,
                    AccountType = AccountType.Current,
                    AccountBalance = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = loggedInUser.Id
                };
                AllAccounts.Add(createAccount);
                Console.WriteLine("Your Current account was created successfully!");
                Console.ReadKey();
            }
            else if (choice == "2")
            {
                decimal amount;

                do
                {
                    Console.WriteLine("Please enter an initial deposit of at least 1000 naira");
                } while (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 1000);

                var createAccount = new Account()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = AccNo,
                    AccountType = AccountType.Savings,
                    AccountBalance = amount,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = loggedInUser.Id
                };
                AllAccounts.Add(createAccount);
                Console.WriteLine("Your Savings account was created successfully");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                CreateAccount(loggedInUser);
            }
        }
        public void Withdraw(User loggedInUser)
        {
            Console.WriteLine("Please, enter your destination account number");
            string accountNumber = Console.ReadLine();

            var accountExist = AllAccounts.Find(account => account.UserId == loggedInUser.Id && account.AccountNumber == accountNumber);

            if (accountExist is not null)
            {
                decimal amount;
                do
                {
                    Console.WriteLine("Enter an amount to withdraw");
                    string amountInput = Console.ReadLine();

                    if (decimal.TryParse(amountInput, out amount) && amount > 0)
                    {
                        if (amount <= accountExist.AccountBalance)
                        {
                            decimal minBalance = accountExist.AccountType == AccountType.Savings ? 1000 : 0;

                            if (accountExist.AccountBalance - amount >= minBalance)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Withdrawal not allowed. Minimum balance requirement: {minBalance}");

                            }
                        }
                        else
                        {
                            Console.WriteLine("Insufficient balance. Please enter a valid amount.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Please enter a valid positive number.");
                    }
                } while (true);

                accountExist.AccountBalance -= amount;

                Console.WriteLine($"{amount} successfully withdrawn from account {accountNumber}");
                Create_a_WithDrawalStatement(loggedInUser, accountExist.AccountType, accountExist.AccountNumber, amount, accountExist.AccountBalance);
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("Account not found or an error occurred.");
                Console.ReadKey();
            }
        }

        public void Deposit(User loggedInUser)
        {
            Console.WriteLine("Please enter the destination account number");
            string accountNumber = Console.ReadLine();
            var accountExist = AllAccounts.Find(account => account.UserId == loggedInUser.Id && account.AccountNumber == accountNumber);
            if (accountExist != null)
            {
                Console.WriteLine("Enter an Amount to Deposit");
                string amountInput = Console.ReadLine();

                if (decimal.TryParse(amountInput, out decimal depositAmount))
                {
                    if (depositAmount > 0)
                    {
                        accountExist.AccountBalance += depositAmount;
                        Console.WriteLine($"Successfully deposited {depositAmount} to account number: {accountNumber}");
                        Create_a_DepositStatement(loggedInUser, accountExist.AccountType, accountExist.AccountNumber, depositAmount, accountExist.AccountBalance);

                    }
                    else
                    {
                        Console.WriteLine("Invalid deposit amount. Please enter a positive amount.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric amount.");
                }
            }
            else
            {
                Console.WriteLine("Account not found or you don't have permission to deposit into this account.");
            }

            Console.ReadKey();
        }

        public void Transfer(User loggedInUser)
        {
            Console.WriteLine("Enter sender's account number");
            string senderAcc = Console.ReadLine();
            var sender = AllAccounts.SingleOrDefault(account => account.UserId == loggedInUser.Id && account.AccountNumber == senderAcc);

            Console.WriteLine("Enter receiver's account number");
            string receiverAcc = Console.ReadLine();
            var receiver = AllAccounts.SingleOrDefault(account => account.UserId == loggedInUser.Id && account.AccountNumber == receiverAcc);

            if (sender != null && receiver != null)
            {
                Console.WriteLine("Enter amount to transfer");
                string amountInput = Console.ReadLine();

                if (decimal.TryParse(amountInput, out decimal transferAmount))
                {
                    if (transferAmount > 0 && transferAmount <= sender.AccountBalance)
                    {
                        sender.AccountBalance -= transferAmount;
                        Create_a_senderTransferStatement(loggedInUser, sender.AccountType, sender.AccountNumber, receiver.AccountNumber, transferAmount, sender.AccountBalance);
                        
                        receiver.AccountBalance += transferAmount;
                        Create_a_receiverTransferStatement(loggedInUser, receiver.AccountType, sender.AccountNumber, receiver.AccountNumber, transferAmount, receiver.AccountBalance);

                        Console.WriteLine("Transaction Successful");

                    }
                    else
                    {
                        Console.WriteLine("Invalid transfer amount. Make sure the amount is within your available balance.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric amount.");
                }
            }
            else
            {
                Console.WriteLine("One or both of the accounts were not found.");
            }

            Console.ReadKey();
        }

        public void CheckBalance(User loggedInUser)
        {
            Console.WriteLine("Enter account number");
            string accountNumber = Console.ReadLine();


            if (int.TryParse(accountNumber, out int accNo))
            {
                var accountExist = AllAccounts.Find(account => account.UserId == loggedInUser.Id && account.AccountNumber == accNo.ToString());

                if (accountExist != null)
                {
                    Console.WriteLine($"Your Account Balance is {accountExist.AccountBalance}");
                }
                else
                {
                    Console.WriteLine("Account not found or you don't have permission to access this account.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric account number.");
            }

            Console.ReadKey();
        }

        public void GetAllAccounts(User loggedInUser)
        {
            var loggedInUserAccounts = AllAccounts.Where(account => account.UserId == loggedInUser.Id).ToList();

            Console.WriteLine("===========================================================================================|");
            Console.WriteLine("|        Name          |      ACCOUNT NUMBER   |    ACCOUNT TYPE     |    ACCOUNT BALANCE  |");
            Console.WriteLine("===========================================================================================|");

            foreach (var account in loggedInUserAccounts)
            {
                Console.WriteLine($"| {loggedInUser.FullName,-20} | {account.AccountNumber,-20} | {account.AccountType,-20} | {account.AccountBalance,-20}|");
                Console.WriteLine("|======================|======================|======================|=====================|");
            }
            Console.ReadKey();
        }


        public void Create_a_DepositStatement(User loggedInUser, AccountType accType, string AccountNo, decimal amount, decimal balance)
        {
            var depositStatement = new AcctStatement()
            {
                Date = DateTime.UtcNow,
                AccountOwner = loggedInUser.FullName,
                AccountNo = AccountNo,
                AccountType = accType,
                Amount = amount,
                Balance = balance,
                CashFlow = CashFlow.Credit,
                Description = $"Deposit occured",
                Id = Guid.NewGuid().ToString(),
                UserId = loggedInUser.Id

            };
            AllAccountStatements.Add(depositStatement);
        }


        public void Create_a_WithDrawalStatement(User loggedInUser, AccountType accType, string AccountNo, decimal amount, decimal balance)
        {
            var withdrawalStatement = new AcctStatement()
            {
                Date = DateTime.UtcNow,
                AccountOwner = loggedInUser.FullName,
                AccountNo = AccountNo,
                AccountType = accType,
                Amount = amount,
                Balance = balance,
                CashFlow = CashFlow.Debit,
                Description = $"Withdrawal occured",
                Id = Guid.NewGuid().ToString(),
                UserId = loggedInUser.Id

            };
            AllAccountStatements.Add(withdrawalStatement);
        }



        public void Create_a_senderTransferStatement(User loggedInUser, AccountType accType, string senderAccountNo, string receiverAccountNo, decimal amount, decimal balance)
        {

            var createdDebitStatement = new AcctStatement()
            {
                Date = DateTime.UtcNow,
                AccountOwner = loggedInUser.FullName,
                AccountNo = senderAccountNo,
                AccountType = accType,
                Amount = amount,
                Balance = balance,
                CashFlow = CashFlow.Debit,
                Description = $"Transfered {amount} to {receiverAccountNo}",
                Id = Guid.NewGuid().ToString(),
                UserId = loggedInUser.Id
            };
            AllAccountStatements.Add(createdDebitStatement);
        }


        public void Create_a_receiverTransferStatement(User loggedInUser, AccountType accType, string senderAccountNo, string receiverAccountNo, decimal amount, decimal balance)
        {

            var createdCreditStatement = new AcctStatement()
            {
                Date = DateTime.UtcNow,
                AccountOwner = loggedInUser.FullName,
                AccountNo = receiverAccountNo,
                AccountType = accType,
                Amount = amount,
                Balance = balance,
                CashFlow = CashFlow.Credit,
                Description = $"Received {amount} from {senderAccountNo}",
                Id = Guid.NewGuid().ToString(),
                UserId = loggedInUser.Id
            };

            AllAccountStatements.Add(createdCreditStatement);

        }




        public void GetMyAccountStatement(User loggedInUser)
        {
            Console.WriteLine("Enter an account Number");
            string accNoInput = Console.ReadLine();

            if (!int.TryParse(accNoInput, out int accNo))
            {
                Console.WriteLine("Invalid account number. Please enter a valid numeric account number.");
                Console.ReadKey();
                return;
            }

            var myAccountStatements = AllAccountStatements.Where(statement => statement.UserId == loggedInUser.Id && statement.AccountNo == accNo.ToString()).ToList();

            if (myAccountStatements.Count == 0)
            {
                Console.WriteLine("No account statements found for the provided account number.");
                Console.ReadKey();
                return;
            }

            var firstStatement = myAccountStatements.FirstOrDefault();
            if (firstStatement != null)
            {
                Console.WriteLine("|----------------------|----------------------|----------------------|----------------------|----------------------|");
                Console.WriteLine($"| Account Owner: {firstStatement.AccountOwner,-47} Account Number: {firstStatement.AccountNo,-41} Account Type: {firstStatement.AccountType,-44}|");
                Console.WriteLine("|----------------------|----------------------|----------------------|----------------------|----------------------|");
            }

            Console.WriteLine("|----------------------|----------------------|----------------------|----------------------|----------------------|");
            Console.WriteLine("|         DATE         |      CASH FLOW       |       AMOUNT         |    DESCRIPTION       |       BALANCE        |");
            Console.WriteLine("|----------------------|----------------------|----------------------|----------------------|----------------------|");

            foreach (var statement in myAccountStatements)
            {
                Console.WriteLine($"| {statement.Date,-20}| {statement.CashFlow,-20} | {statement.Amount,-20} | {statement.Description,-20} | {statement.Balance,-20} |");
                Console.WriteLine("|----------------------|----------------------|----------------------|----------------------|----------------------|");
            }

            Console.ReadKey();
        }
    }
}
