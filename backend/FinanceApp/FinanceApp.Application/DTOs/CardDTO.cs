using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.DTOs
{
    public class CardDTO
    {
        public int CardId { get; set; }
        public int BankId { get; set; }
        public int CurrencyId { get; set; }
        public int CardTypeId { get; set; }
        public string OwnerName { get; set; }
        public string Comment { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? CutOffDay { get; set; }
        public int? PaymentDay { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCardDTO
    {
        public int BankId { get; set; }
        public int CurrencyId { get; set; }
        public int CardTypeId { get; set; }
        public string OwnerName { get; set; }
        public string Comment { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? CutOffDay { get; set; }
        public int? PaymentDay { get; set; }
        public string CreatedBy { get; set; }

    }

    public class UpdateCardDTO
    {
        public int BankId { get; set; }
        public int CurrencyId { get; set; }
        public int CardTypeId { get; set; }
        public string OwnerName { get; set; }
        public string Comment { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? CutOffDay { get; set; }
        public int? PaymentDay { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class CardReadOnlyViewDTO
    {
        public int CardId { get; set; }
        public string Bank { get; set; }
        public string Currency { get; set; }
        public string CardType { get; set; }
        public string OwnerName { get; set; }
        public string Comment { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CutOffDay { get; set; }
        public string PaymentDay { get; set; }

    }
}
