using FinanceApp.Domain.Enums;
using System;

namespace FinanceApp.Domain.Entities
{
    public class CardEntity
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

        public void ValidateRules()
        {

            if (BankId <= 0)
                throw new ArgumentException("Bank is required.");

            if (CurrencyId <= 0)
                throw new ArgumentException("Currency is required.");

            if (CardTypeId <= 0)
                throw new ArgumentException("Card Type is required.");

            if (string.IsNullOrWhiteSpace(OwnerName))
                throw new ArgumentException("Owner Name  is required.");

            if (string.IsNullOrWhiteSpace(Comment))
                throw new ArgumentException("Comment is required.");

            if (OwnerName.Length > 100)
                throw new ArgumentException("Owner Name max length: 100.");

            if (Comment.Length > 250)
                throw new ArgumentException("Owner Name max length: 100.");

            if (IssueDate == default(DateTime))
            {
                throw new ArgumentException("Issue Date is required.");
            }

            if (ExpirationDate == default(DateTime))
            {
                throw new ArgumentException("Expiration date is required.");
            }

            if (IssueDate > ExpirationDate)
                throw new ArgumentException("The Issue Date cannot be later than the expiration date.");

            if (Enum.IsDefined(typeof(CardTypeEnum), CardTypeId))
            {
                CardTypeEnum cardType = (CardTypeEnum)CardTypeId;
                if (cardType == CardTypeEnum.Credit && CutOffDay <= 0)
                    throw new ArgumentException("cut-off day must be greater than zero");

                if (cardType == CardTypeEnum.Credit && PaymentDay <= 0)
                    throw new ArgumentException("Payment Day must be greater than zero");
            }
            else
            {
                throw new ArgumentException("Invalid Card Type Id ");
            }



          
        }
    }
}
