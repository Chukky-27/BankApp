using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AcctStatement
    {
        public Guid UserId;

        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string AccountNo { get; set; }
        public AccountType AccountType { get; set; }
        public string Description { get; set; }
        public CashFlow CashFlow { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string AccountOwner { get; set; }
        public Guid UserID { get; set; }
    }
}
