using System;

namespace FinanceApp.Domain.Queries
{
    public class DepositCertificateReadOnlyView
    {
        public int CertificateId { get; set; }
        public string Bank { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Interest_amount { get; set; }
        public DateTime Expiration_date { get; set; }

        public bool? IsActive { get; set; }
    }
}
