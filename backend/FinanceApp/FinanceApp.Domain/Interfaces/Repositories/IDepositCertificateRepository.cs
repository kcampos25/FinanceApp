using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.Repositories
{
    public interface IDepositCertificateRepository
    {
        Task<IEnumerable<DepositCertificateEntity>> GetAllAsync();
        Task<IEnumerable<DepositCertificateReadOnlyView>> GetDetailAsync();
        Task<DepositCertificateEntity> GetByIdAsync(int id);
        Task<DepositCertificateEntity> AddAsync(DepositCertificateEntity certificate);
        Task UpdateAsync(DepositCertificateEntity certificate);
        Task DeleteDepositCertificateAsync(int certificateId);
    }
}
