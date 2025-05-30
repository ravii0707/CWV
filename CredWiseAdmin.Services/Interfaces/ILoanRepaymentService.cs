using CredWiseAdmin.Core.DTOs;
using CredWiseAdmin.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CredWiseAdmin.Services.Interfaces
{
    public interface ILoanRepaymentService
    {
        Task<ApiResponse<IEnumerable<LoanRepaymentDto>>> GetRepaymentsByLoanIdAsync(int loanApplicationId);
        Task<ApiResponse<PaymentResultDto>> ProcessPaymentAsync(PaymentTransactionDto paymentDto);
        Task<ApiResponse<bool>> ApplyPenaltyAsync(int repaymentId);
        Task<ApiResponse<IEnumerable<LoanRepaymentDto>>> GetPendingRepaymentsAsync(int userId);
        Task<ApiResponse<IEnumerable<LoanRepaymentDto>>> GetOverdueRepaymentsAsync();
        Task<ApiResponse<IEnumerable<LoanRepaymentDto>>> GetAllRepaymentsAsync();
        //Task<IEnumerable<LoanRepaymentDto>> GenerateEmiPlanAsync(EmiPlanDto emiPlanDto);
        Task<IEnumerable<LoanRepaymentSchedule>> GenerateAndSaveEmiPlanAsync(EmiPlanDto emiPlanDto);
    }
}