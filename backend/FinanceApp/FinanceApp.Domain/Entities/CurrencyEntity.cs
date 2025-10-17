using System;

namespace FinanceApp.Domain.Entities
{
    public class CurrencyEntity
    {
        public int CurrencyId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void ValidateForCreate()
        {
            if (string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("Description is required.");
            if (Description.Length > 100)
                throw new ArgumentException("Description max length is 20.");
            if (string.IsNullOrWhiteSpace(CreatedBy))
                throw new ArgumentException("CreatedBy is required.");
        }
        public void Update(string description, string updatedBy)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.");
            if (description.Length > 100)
                throw new ArgumentException("Description max length is 20.");
            Description = description;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
