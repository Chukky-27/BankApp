using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Account : BaseEntity
    {
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public decimal AccountBalance { get; set; }
        public Guid UserId { get; set; }
    }
}
