// ILoanApplicationService.cs
using CredWiseAdmin.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CredWiseAdmin.Services.Interfaces
{
    public interface ILoanApplicationService
    {
        Task<IEnumerable<LoanApplicationResponseDto>> GetAllLoanApplicationsAsync();
    }
}