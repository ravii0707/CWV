//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CredWiseAdmin.Core.DTOs
//{
//    public class LoanRepaymentDto
//    {
//        public int RepaymentId { get; set; }
//        public int LoanApplicationId { get; set; }
//        public int InstallmentNumber { get; set; }
//        public DateTime DueDate { get; set; }
//        public decimal PrincipalAmount { get; set; }
//        public decimal InterestAmount { get; set; }
//        public decimal TotalAmount { get; set; }
//        public string Status { get; set; }
//        public DateTime CreatedAt { get; set; }
//    }

//    public class PaymentTransactionDto
//    {
//        [Required]
//        public int LoanApplicationId { get; set; }

//        //[Required]
//        //public int RepaymentId { get; set; }

//        [Required]
//        public decimal Amount { get; set; }

//        [Required]
//        public string PaymentMethod { get; set; }
//    }

//    public class PaymentTransactionResponseDto : PaymentTransactionDto
//    {
//        public int TransactionId { get; set; }
//        public int RepaymentId { get; set; }
//        public DateTime PaymentDate { get; set; }
//        public string TransactionStatus { get; set; }
//        public string TransactionReference { get; set; }
//    }
//}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredWiseAdmin.Core.DTOs
{
    public class LoanRepaymentDto
    {
        public int RepaymentId { get; set; }
        public int LoanApplicationId { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }

    public class PaymentTransactionDto
    {
        [Required(ErrorMessage = "Loan application ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application ID")]
        public int LoanApplicationId { get; set; }

        [Required(ErrorMessage = "Payment amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required")]
        [StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Transaction reference is required")]
        [StringLength(100, ErrorMessage = "Transaction reference cannot exceed 100 characters")]
        public string TransactionReference { get; set; } = Guid.NewGuid().ToString();
    }

    public class PaymentTransactionResponseDto
    {
        public int TransactionId { get; set; }
        public int LoanApplicationId { get; set; }
        public int RepaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionReference { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }

    public class PaymentResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public PaymentTransactionResponseDto? Transaction { get; set; }
        public LoanRepaymentDto? Repayment { get; set; }
    }
}