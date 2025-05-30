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
        public IEnumerable<PaymentTransactionDto>? Transactions { get; set; }
    }

    public class PaymentTransactionDto
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

    public class LoanRepaymentWithTransactionsDto : LoanRepaymentDto
    {
        public List<PaymentTransactionResponseDto> Transactions { get; set; }
    }

    public class PaymentResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public PaymentTransactionResponseDto? Transaction { get; set; }
        public LoanRepaymentDto? Repayment { get; set; }
    }
}