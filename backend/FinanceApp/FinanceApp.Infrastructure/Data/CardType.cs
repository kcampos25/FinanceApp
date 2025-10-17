using System;
using System.Collections.Generic;

#nullable disable

namespace FinanceApp.Infrastructure.Data
{
    public partial class CardType
    {
        public CardType()
        {
            Cards = new HashSet<Card>();
        }

        public int CardTypeId { get; set; }
        public string CardTypeCode { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
