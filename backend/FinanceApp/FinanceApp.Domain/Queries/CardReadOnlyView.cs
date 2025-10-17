using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Queries
{
    public class CardReadOnlyView
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
