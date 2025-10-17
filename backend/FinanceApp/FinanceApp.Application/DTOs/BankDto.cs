using System;
using System.Collections.Generic;
namespace FinanceApp.Application.DTOs
{
    public class BankDTO
    {
        public int BankId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateBankDTO
    {
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UpdateBankDTO
    {
        public string Description { get; set; }
        public string UpdatedBy { get; set; }
    }
}
