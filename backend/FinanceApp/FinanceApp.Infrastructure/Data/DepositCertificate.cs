using System;
using System.Collections.Generic;

#nullable disable

namespace FinanceApp.Infrastructure.Data
{
    public partial class DepositCertificate
    {
        public int CertificateId { get; set; }
        public int BankId { get; set; }
        public int CurrencyId { get; set; }
        public string OwnerName { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
