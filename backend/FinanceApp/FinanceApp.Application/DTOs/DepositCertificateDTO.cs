using System;

namespace FinanceApp.Application.DTOs
{
    public class DepositCertificateDTO
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
    }

    public class CreateDepositCertificateDTO
    {
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
    }

    public class UpdateDepositCertificateDTO
    {
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
        public string UpdatedBy { get; set; }
    }

    public class DepositCertificateReadOnlyViewDTO
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
