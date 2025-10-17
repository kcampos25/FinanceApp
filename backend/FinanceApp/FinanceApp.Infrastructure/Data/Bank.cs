using System;
using System.Collections.Generic;

#nullable disable

namespace FinanceApp.Infrastructure.Data
{
    public partial class Bank
    {
        public Bank()
        {
            Cards = new HashSet<Card>();
            DepositCertificates = new HashSet<DepositCertificate>();
        }

        public int BankId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<DepositCertificate> DepositCertificates { get; set; }
    }
}
