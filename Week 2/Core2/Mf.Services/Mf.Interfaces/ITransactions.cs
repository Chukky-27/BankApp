using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFCore.MFServices.MfInterfaces
{
    public interface ITransactions
    {
        public void CreateAccount(User loggedInUser);
        public void Withdraw(User loggedInUser);
        public void Deposit(User loggedInUser);
        public void Transfer(User loggedInUser);
        public void CheckBalance(User loggedInUser);

    }
}
