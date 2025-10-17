using System;

namespace FinanceApp.Application.DTOs
{
    public class CurrencyDTO
    {
        public int CurrencyId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCurrencyDTO
    {
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UpdateCurrencyDTO
    {
        public string Description { get; set; }
        public string UpdatedBy { get; set; }
    }
}
