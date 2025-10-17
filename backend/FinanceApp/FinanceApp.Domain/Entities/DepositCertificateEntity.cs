using System;

namespace FinanceApp.Domain.Entities
{
    public class DepositCertificateEntity
    {
        public int CertificateId { get; set; }
        public int BankId { get; set; }
        public int CurrencyId { get; set; }
        public string Owner_name { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public decimal Amount { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime Expiration_date { get; set; }
        public decimal Interest_amount { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public void ValidateRules()
        {
            if (BankId <= 0)
                throw new ArgumentException("Bank is required.");

            if (CurrencyId <= 0)
                throw new ArgumentException("Currency is required.");

            if (CurrencyId < 0)
                throw new ArgumentException("Currency is required.");

            if (Amount < 0)
                throw new ArgumentException("Amount is required.");

            if (Interest_amount < 0)
                throw new ArgumentException("Interest Amount is required.");

            if (Start_date == default(DateTime))
            {
                throw new ArgumentException("Start date is required.");
            }

            if (Expiration_date == default(DateTime))
            {
                throw new ArgumentException("Expiration date is required.");
            }

            if (Start_date > Expiration_date)
                throw new ArgumentException("The start date cannot be later than the expiration date.");
        }
    }
}
