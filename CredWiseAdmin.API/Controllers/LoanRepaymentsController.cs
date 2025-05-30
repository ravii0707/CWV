using CredWiseAdmin.Core.DTOs;
using CredWiseAdmin.Core.Exceptions;
using CredWiseAdmin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CredWiseAdmin.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRepaymentsController : ControllerBase
    {
        private readonly ILoanRepaymentService _loanRepaymentService;
        private readonly ILogger<LoanRepaymentsController> _logger;

        public LoanRepaymentsController(ILoanRepaymentService loanRepaymentService, ILogger<LoanRepaymentsController> logger)
        {
            _loanRepaymentService = loanRepaymentService;
            _logger = logger;
        }

        [HttpGet("loan/{loanApplicationId}")]
        public async Task<ActionResult<IEnumerable<LoanRepaymentDto>>> GetRepaymentsByLoanId(int loanApplicationId)
        {
            var repayments = await _loanRepaymentService.GetRepaymentsByLoanIdAsync(loanApplicationId);
            return Ok(repayments);
        }

        [HttpPost("payment")]
        public async Task<ActionResult<PaymentTransactionResponseDto>> ProcessPayment([FromBody] PaymentTransactionDto paymentDto)
        {
            var transaction = await _loanRepaymentService.ProcessPaymentAsync(paymentDto);
            return Ok(transaction);
        }

        [HttpPost("apply-penalty/{repaymentId}")]
        public async Task<IActionResult> ApplyPenalty(int repaymentId)
        {
            try
            {
                var response = await _loanRepaymentService.ApplyPenaltyAsync(repaymentId);

                if (response.Success == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error applying penalty to repayment {RepaymentId}", repaymentId);
                return StatusCode(500, new ApiResponse<bool>
                {
                    Success = false,
                    Message = "An error occurred while applying penalty",
                    Errors = new List<ApiError> { new ApiError {
                Code = "SERVER_ERROR",
                Description = ex.Message
            }}
                });
            }
        }

        [HttpGet("user/{userId}/pending")]
        public async Task<ActionResult<IEnumerable<LoanRepaymentDto>>> GetPendingRepayments(int userId)
        {
            var repayments = await _loanRepaymentService.GetPendingRepaymentsAsync(userId);
            return Ok(repayments);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<LoanRepaymentDto>>> GetOverdueRepayments()
        {
            var repayments = await _loanRepaymentService.GetOverdueRepaymentsAsync();
            return Ok(repayments);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<LoanRepaymentDto>>> GetAllRepayments()
        {
            try
            {
                var repayments = await _loanRepaymentService.GetAllRepaymentsAsync();
                return Ok(repayments);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        // Add to LoanRepaymentsController.cs
        [HttpPost("generate-emi-plan")]
        public async Task<ActionResult<IEnumerable<LoanRepaymentDto>>> GenerateEmiPlan([FromBody] EmiPlanDto emiPlanDto)
        {
            try
            {
                var emiPlan = await _loanRepaymentService.GenerateAndSaveEmiPlanAsync(emiPlanDto);
                return Ok(emiPlan);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while generating the EMI plan." });
            }
        }
    }
}
