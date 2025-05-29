// ILoanApplicationRepository.cs
using CredWiseAdmin.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CredWiseAdmin.Repository.Interfaces
{
    public interface ILoanApplicationRepository
    {
        Task<IEnumerable<LoanApplication>> GetAllAsync();
    }
}