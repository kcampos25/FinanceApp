using FinanceApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceApp.Application.Interfaces
{
    public interface IDepositCertificateService
    {
        Task<IEnumerable<DepositCertificateDTO>> GetAllAsync();
        Task<DepositCertificateDTO> GetByIdAsync(int id);
        Task<DepositCertificateDTO> CreateAsync(CreateDepositCertificateDTO dto);
        Task UpdateAsync(int id, UpdateDepositCertificateDTO dto);
        Task<IEnumerable<DepositCertificateReadOnlyViewDTO>> GetDetailAsync();
        Task DeleteDepositCertificateAsync(int certificateId);
    }
}
