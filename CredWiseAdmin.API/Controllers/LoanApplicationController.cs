// LoanApplicationController.cs
using CredWiseAdmin.Core.DTOs;
using CredWiseAdmin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CredWiseAdmin.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILoanApplicationService _loanApplicationService;

        public LoanApplicationController(ILoanApplicationService loanApplicationService)
        {
            _loanApplicationService = loanApplicationService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<LoanApplicationResponseDto>>> GetAllLoanApplications()
        {
            var result = await _loanApplicationService.GetAllLoanApplicationsAsync();
            return Ok(result);
        }
    }
}