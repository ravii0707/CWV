// LoanApplicationRepository.cs
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CredWiseAdmin.Repository.Repositories
{
    public class LoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly AppDbContext _context;

        public LoanApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanApplication>> GetAllAsync()
        {
            return await _context.LoanApplications
                .Include(x => x.LoanProduct)
                .Include(x => x.GoldLoanApplications)
                .Include(x => x.HomeLoanApplications)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}