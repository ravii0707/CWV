// LoanApplicationService.cs
using CredWiseAdmin.Core.DTOs;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using CredWiseAdmin.Services.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CredWiseAdmin.Services.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _repository;
        private readonly IMapper _mapper;

        public LoanApplicationService(
            ILoanApplicationRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanApplicationResponseDto>> GetAllLoanApplicationsAsync()
        {
            var applications = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<LoanApplicationResponseDto>>(applications);
        }
    }
}