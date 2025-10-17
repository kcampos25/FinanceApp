using System;
using System.Collections.Generic;

#nullable disable

namespace FinanceApp.Infrastructure.Data
{
    public partial class Card
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

        public virtual Bank Bank { get; set; }
        public virtual CardType CardType { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
