using AutoMapper;
using CredWiseAdmin.Core.DTOs;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredWiseAdmin.Services.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mappings
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()));
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<UpdateUserDto, User>();

            // Loan product mappings
            CreateMap<LoanProduct, LoanProductResponseDto>();
            CreateMap<PersonalLoanDetail, PersonalLoanDetailDto>();
            CreateMap<HomeLoanDetail, HomeLoanDetailDto>();
            CreateMap<GoldLoanDetail, GoldLoanDetailDto>();


            CreateMap<LoanApplication, LoanApplicationResponseDto>()
               .ForMember(dest => dest.LoanType, opt => opt.MapFrom(src => src.LoanProduct.LoanType))
               .ForMember(dest => dest.InterestRate, opt => opt.MapFrom(src => src.InterestRate))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
               .ForMember(dest => dest.LoanApplicationId, opt => opt.MapFrom(src => src.LoanApplicationId))
               .ForMember(dest => dest.LoanProductId, opt => opt.MapFrom(src => src.LoanProduct.LoanProductId));


            // Loan bank statement mappings
            CreateMap<LoanBankStatement, BankStatementResponseDto>();
            CreateMap<UploadBankStatementDto, LoanBankStatement>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            
            // Repayment plan mappings
            CreateMap<RepaymentPlanDTO, LoanRepaymentSchedule>()
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DueDate)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending")) // Default status
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"));

            CreateMap<LoanRepaymentSchedule, RepaymentPlanDTO>()
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate.ToDateTime(TimeOnly.MinValue)));

            // FD mappings
            CreateMap<FDTypeDto, Fdtype>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            CreateMap<Fdtype, FDTypeResponseDto>();
            CreateMap<FDApplicationDto, Fdapplication>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            CreateMap<Fdapplication, FDApplicationResponseDto>();

            // Loan enquiry mappings
            CreateMap<LoanEnquiryDto, LoanEnquiry>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
