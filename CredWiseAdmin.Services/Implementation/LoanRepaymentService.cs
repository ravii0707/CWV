using AutoMapper;
using CredWiseAdmin.Core.DTOs;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Core.Exceptions;
using CredWiseAdmin.Repository.Interfaces;
using CredWiseAdmin.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CredWiseAdmin.Services.Implementation
{
    public class LoanRepaymentService : ILoanRepaymentService
    {
        private readonly ILoanRepaymentRepository _loanRepaymentRepository;
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LoanRepaymentService> _logger;

        public LoanRepaymentService(
            ILoanRepaymentRepository loanRepaymentRepository,
            IPaymentTransactionRepository paymentTransactionRepository,
            IMapper mapper,
            ILogger<LoanRepaymentService> logger)
        {
            _loanRepaymentRepository = loanRepaymentRepository;
            _paymentTransactionRepository = paymentTransactionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //public async Task<ApiResponse<IEnumerable<LoanRepaymentDto>>> GetRepaymentsByLoanIdAsync(int loanApplicationId)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Fetching repayments for loan application {LoanApplicationId}", loanApplicationId);

        //        var repayments = await _loanRepaymentRepository.GetByLoanApplicationIdAsync(loanApplicationId);

        //        return ApiResponse<IEnumerable<LoanRepaymentDto>>.CreateSuccess(
        //            _mapper.Map<IEnumerable<LoanRepaymentDto>>(repayments),
        //            repayments.Any() ? "Repayments retrieved successfully" : "No repayments found"
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error fetching repayments for loan application {LoanApplicationId}", loanApplicationId);
        //        return ApiResponse<IEnumerable<LoanRepaymentDto>>.CreateError(
        //            "Failed to retrieve repayments",
        //            new List<ApiError> { new ApiError {
        //                Code = "REPAYMENT_RETRIEVAL_ERROR",
        //                Description = ex.Message
        //            }}
        //        );
        //    }
        //}

        public async Task<ApiResponse<IEnumerable<LoanRepaymentDto>>> GetRepaymentsByLoanIdAsync(int loanApplicationId)
        {
            try
            {
                _logger.LogInformation("Fetching repayments for loan application {LoanApplicationId}", loanApplicationId);

                var repayments = await _loanRepaymentRepository.GetByLoanApplicationIdAsync(loanApplicationId);

                // Map to DTO including transactions
                var repaymentDtos = _mapper.Map<IEnumerable<LoanRepaymentDto>>(repayments);

                // If you want to include transaction details in the response
                foreach (var dto in repaymentDtos)
                {
                    var repayment = repayments.FirstOrDefault(r => r.RepaymentId == dto.RepaymentId);
                    if (repayment != null && repayment.PaymentTransactions != null)
                    {
                        dto.Transactions = _mapper.Map<IEnumerable<PaymentTransactionDto>>(repayment.PaymentTransactions);
                    }
                }

                return ApiResponse<IEnumerable<LoanRepaymentDto>>.CreateSuccess(
                    repaymentDtos,
                    repayments.Any() ? "Repayments retrieved successfully" : "No repayments found"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching repayments for loan application {LoanApplicationId}", loanApplicationId);
                return ApiResponse<IEnumerable<LoanRepaymentDto>>.CreateError(
                    "Failed to retrieve repayments",
                    new List<ApiError> { new ApiError {
                Code = "REPAYMENT_RETRIEVAL_ERROR",
                Description = ex.Message
            }}
                );
            }
        }

        public async Task<ApiResponse<PaymentResultDto>> ProcessPaymentAsync(PaymentTransactionDto paymentDto)
        {
            try
            {
                _logger.LogInformation("Processing payment for loan application {LoanApplicationId}", paymentDto.LoanApplicationId);

                // Validate input
                if (paymentDto.Amount <= 0)
                    throw new ValidationException("Payment amount must be greater than zero");

                // Get next pending repayment
                var repayment = (await _loanRepaymentRepository.GetByLoanApplicationIdAsync(paymentDto.LoanApplicationId))
                    .OrderBy(r => r.InstallmentNumber)
                    .FirstOrDefault(r => r.Status == "Pending");

                if (repayment == null)
                    throw new NotFoundException("No pending repayments found");

                // Validate payment amount
                if (paymentDto.Amount < repayment.TotalAmount)
                    throw new ValidationException($"Payment amount must be at least {repayment.TotalAmount}");

                // Create transaction
                var transaction = new PaymentTransaction
                {
                    LoanApplicationId = paymentDto.LoanApplicationId,
                    RepaymentId = repayment.RepaymentId,
                    Amount = paymentDto.Amount,
                    PaymentMethod = paymentDto.PaymentMethod,
                    PaymentDate = DateTime.UtcNow,
                    TransactionStatus = "Completed",
                    TransactionReference = paymentDto.TransactionReference,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };

                await _paymentTransactionRepository.AddAsync(transaction);

                // Update repayment
                repayment.Status = "Paid";
                repayment.ModifiedAt = DateTime.UtcNow;
                repayment.ModifiedBy = "System";
                await _loanRepaymentRepository.UpdateAsync(repayment);

                return ApiResponse<PaymentResultDto>.CreateSuccess(
                    new PaymentResultDto
                    {
                        Transaction = _mapper.Map<PaymentTransactionResponseDto>(transaction),
                        Repayment = _mapper.Map<LoanRepaymentDto>(repayment)
                    },
                    "Payment processed successfully"
                );
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Payment validation failed");
                return ApiResponse<PaymentResultDto>.CreateError(
                    ex.Message,
                    new List<ApiError> { new ApiError {
                        Code = "VALIDATION_ERROR",
                        Description = ex.Message
                    }}
                );
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Repayment not found");
                return ApiResponse<PaymentResultDto>.CreateError(
                    ex.Message,
                    new List<ApiError> { new ApiError {
                        Code = "NOT_FOUND_ERROR",
                        Description = ex.Message
                    }}
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment processing error");
                return ApiResponse<PaymentResultDto>.CreateError(
                    "An error occurred while processing payment",
                    new List<ApiError> { new ApiError {
                        Code = "PROCESSING_ERROR",
                        Description = ex.Message
                    }}
                );
            }
        }

        public async Task<ApiResponse<bool>> ApplyPenaltyAsync(int repaymentId)
        {
            try
            {
                _logger.LogInformation("Applying penalty to repayment {RepaymentId}", repaymentId);

                var repayment = await _loanRepaymentRepository.GetByIdAsync(repaymentId);
                if (repayment == null)
                    throw new NotFoundException($"Repayment {repaymentId} not found");

                repayment.TotalAmount += 500; // ₹500 penalty
                repayment.ModifiedAt = DateTime.UtcNow;
                repayment.ModifiedBy = "System";
                await _loanRepaymentRepository.UpdateAsync(repayment);

                return ApiResponse<bool>.CreateSuccess(true, "Penalty applied successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error applying penalty");
                return ApiResponse<bool>.CreateError(
                    "Failed to apply penalty",
                    new List<ApiError> { new ApiError {
                        Code = "PENALTY_ERROR",
                        Description = ex.Message
                    }}
                );
            }
        }

        public async Task<ApiResponse<IEnumerable<LoanRepaymentDto>>> GetPendingRepaymentsAsync(int userId)
        {
            try
            {
                _logger.LogInformation("Fetching pending repayments for user {UserId}", userId);

                var repayments = await _loanRepaymentRepository.GetPendingRepaymentsAsync(userId);

                return ApiResponse<IEnumerable<LoanRepaymentDto>>.CreateSuccess(
                    _mapper.Map<IEnumerable<LoanRepaymentDto>>(repayments),
                    repayments.Any() ? "Pending repayments retrieved" : "No pending repayments"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching pending repayments");
                return ApiResponse<IEnumerable<LoanRepaymentDto>>.CreateError(
                    "Failed to retrieve pending repayments",
                    new List<ApiError> { new ApiError {
                        Code = "PENDING_REPAYMENTS_ERROR",
                        Description = ex.Message
                    }}
                );
            }
        }

        public async Task<ApiResponse<IEnumerable<LoanRepaymentDto>>> GetOverdueRepaymentsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching overdue repayments");

                var repayments = await _loanRepaymentRepository.GetOverdueRepaymentsAsync();

                return ApiResponse<IEnumerable<LoanRepaymentDto>>.CreateSuccess(
                    _mapper.Map<IEnumerable<LoanRepaymentDto>>(repayments),
                    repayments.Any() ? "Overdue repayments retrieved" : "No overdue repayments"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching overdue repayments");
                return ApiResponse<IEnumerable<LoanRepaymentDto>>.CreateError(
                    "Failed to retrieve overdue repayments",
                    new List<ApiError> { new ApiError {
                        Code = "OVERDUE_REPAYMENTS_ERROR",
                        Description = ex.Message
                    }}
                );
            }
        }

        public async Task<ApiResponse<IEnumerable<LoanRepaymentDto>>> GetAllRepaymentsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all repayments");

                var repayments = await _loanRepaymentRepository.GetAllRepaymentsAsync();

                return ApiResponse<IEnumerable<LoanRepaymentDto>>.CreateSuccess(
                    _mapper.Map<IEnumerable<LoanRepaymentDto>>(repayments),
                    repayments.Any() ? "All repayments retrieved" : "No repayments found"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all repayments");
                return ApiResponse<IEnumerable<LoanRepaymentDto>>.CreateError(
                    "Failed to retrieve repayments",
                    new List<ApiError> { new ApiError {
                        Code = "ALL_REPAYMENTS_ERROR",
                        Description = ex.Message
                    }}
                );
            }
        }



        // In LoanRepaymentService.cs
        //public async Task<IEnumerable<LoanRepaymentSchedule>> GenerateAndSaveEmiPlanAsync(EmiPlanDto emiPlanDto)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Generating and saving EMI plan for loan ID: {LoanId}", emiPlanDto.LoanId);

        //        // Validate input
        //        if (emiPlanDto.LoanAmount <= 0)
        //            throw new BadRequestException("Loan amount must be greater than zero");

        //        if (emiPlanDto.TenureInMonths <= 0)
        //            throw new BadRequestException("Tenure must be greater than zero");

        //        if (emiPlanDto.InterestRate <= 0)
        //            throw new BadRequestException("Interest rate must be greater than zero");

        //        // Calculate EMI
        //        decimal monthlyInterestRate = emiPlanDto.InterestRate / 100 / 12;
        //        decimal emi = emiPlanDto.LoanAmount * monthlyInterestRate *
        //                     (decimal)Math.Pow(1 + (double)monthlyInterestRate, emiPlanDto.TenureInMonths) /
        //                     (decimal)(Math.Pow(1 + (double)monthlyInterestRate, emiPlanDto.TenureInMonths) - 1);

        //        var repaymentSchedules = new List<LoanRepaymentSchedule>();
        //        decimal remainingPrincipal = emiPlanDto.LoanAmount;
        //        DateTime dueDate = emiPlanDto.StartDate;

        //        for (int i = 1; i <= emiPlanDto.TenureInMonths; i++)
        //        {
        //            decimal interestComponent = remainingPrincipal * monthlyInterestRate;
        //            decimal principalComponent = emi - interestComponent;

        //            // Adjust for last installment
        //            if (i == emiPlanDto.TenureInMonths)
        //            {
        //                principalComponent = remainingPrincipal;
        //                emi = principalComponent + interestComponent;
        //            }

        //            var repayment = new LoanRepaymentSchedule
        //            {
        //                LoanApplicationId = emiPlanDto.LoanId,
        //                InstallmentNumber = i,
        //                DueDate = DateOnly.FromDateTime(dueDate),
        //                PrincipalAmount = principalComponent,
        //                InterestAmount = interestComponent,
        //                TotalAmount = emi,
        //                Status = "Pending",
        //                CreatedAt = DateTime.UtcNow,
        //                ModifiedAt = DateTime.UtcNow,
        //                CreatedBy = "System",
        //                ModifiedBy = "System",
        //                IsActive = true
        //            };

        //            // Save each repayment to database
        //            await _loanRepaymentRepository.AddAsync(repayment);
        //            repaymentSchedules.Add(repayment);

        //            remainingPrincipal -= principalComponent;
        //            dueDate = dueDate.AddMonths(1);
        //        }

        //        _logger.LogInformation("Successfully generated and saved EMI plan for loan ID: {LoanId}", emiPlanDto.LoanId);
        //        return repaymentSchedules;
        //    }
        //    catch (CustomException)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error generating EMI plan for loan ID: {LoanId}", emiPlanDto.LoanId);
        //        throw new ServiceException("An error occurred while generating the EMI plan. Please try again later.");
        //    }
        //}


        public async Task<IEnumerable<LoanRepaymentSchedule>> GenerateAndSaveEmiPlanAsync(EmiPlanDto emiPlanDto)
        {
            try
            {
                _logger.LogInformation("Generating and saving EMI plan for loan ID: {LoanId}", emiPlanDto.LoanId);

                // Validate input
                if (emiPlanDto.LoanAmount <= 0)
                    throw new BadRequestException("Loan amount must be greater than zero");

                if (emiPlanDto.TenureInMonths <= 0)
                    throw new BadRequestException("Tenure must be greater than zero");

                if (emiPlanDto.InterestRate <= 0)
                    throw new BadRequestException("Interest rate must be greater than zero");

                // Calculate EMI
                decimal monthlyInterestRate = emiPlanDto.InterestRate / 100 / 12;
                decimal emi = emiPlanDto.LoanAmount * monthlyInterestRate *
                             (decimal)Math.Pow(1 + (double)monthlyInterestRate, emiPlanDto.TenureInMonths) /
                             (decimal)(Math.Pow(1 + (double)monthlyInterestRate, emiPlanDto.TenureInMonths) - 1);

                var repaymentSchedules = new List<LoanRepaymentSchedule>();
                decimal remainingPrincipal = emiPlanDto.LoanAmount;
                DateTime dueDate = emiPlanDto.StartDate;

                // Create all repayment schedules first
                for (int i = 1; i <= emiPlanDto.TenureInMonths; i++)
                {
                    decimal interestComponent = remainingPrincipal * monthlyInterestRate;
                    decimal principalComponent = emi - interestComponent;

                    // Adjust for last installment
                    if (i == emiPlanDto.TenureInMonths)
                    {
                        principalComponent = remainingPrincipal;
                        emi = principalComponent + interestComponent;
                    }

                    var repayment = new LoanRepaymentSchedule
                    {
                        LoanApplicationId = emiPlanDto.LoanId,
                        InstallmentNumber = i,
                        DueDate = DateOnly.FromDateTime(dueDate),// Using just the date part
                        PrincipalAmount = principalComponent,
                        InterestAmount = interestComponent,
                        TotalAmount = emi,
                        Status = "Pending",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "System",
                        ModifiedAt = DateTime.UtcNow,
                        ModifiedBy = "System"
                    };

                    repaymentSchedules.Add(repayment);
                    remainingPrincipal -= principalComponent;
                    dueDate = dueDate.AddMonths(1);
                }

                // Save all repayments to database in a transaction
                foreach (var repayment in repaymentSchedules)
                {
                    await _loanRepaymentRepository.AddAsync(repayment);
                }

                // Refresh the list to get the actual RepaymentIds from database
                var savedRepayments = (await _loanRepaymentRepository.GetByLoanApplicationIdAsync(emiPlanDto.LoanId))
                    .OrderBy(r => r.InstallmentNumber)
                    .ToList();

                _logger.LogInformation("Successfully generated and saved EMI plan for loan ID: {LoanId}", emiPlanDto.LoanId);
                return savedRepayments;
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating EMI plan for loan ID: {LoanId}", emiPlanDto.LoanId);
                throw new ServiceException("An error occurred while generating the EMI plan. Please try again later.");
            }
        }
    }
}