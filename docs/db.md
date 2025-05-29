create database CredWiseDB;
-- Create Database CredWiseDB
USE [CredWiseDB]
GO
/****** Object:  Table [dbo].[DecisionAppLogs]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DecisionAppLogs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LoanApplicationId] [int] NOT NULL,
	[DecisionInput] [nvarchar](max) NULL,
	[DecisionOutput] [nvarchar](max) NULL,
	[ProcessedAt] [datetime] NULL,
	[ProcessingTime] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FDApplications]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FDApplications](
	[FDApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FDTypeId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Duration] [int] NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[MaturityDate] [datetime] NULL,
	[MaturityAmount] [decimal](18, 2) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[FDApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FDTransactions]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FDTransactions](
	[FDTransactionId] [int] IDENTITY(1,1) NOT NULL,
	[FDApplicationId] [int] NOT NULL,
	[TransactionType] [nvarchar](20) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[TransactionStatus] [nvarchar](20) NOT NULL,
	[TransactionReference] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[FDTransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FDTypes]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FDTypes](
	[FDTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[MinAmount] [decimal](18, 2) NOT NULL,
	[MaxAmount] [decimal](18, 2) NOT NULL,
	[Duration] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[FDTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GoldLoanApplications]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoldLoanApplications](
	[GoldLoanAppId] [int] IDENTITY(1,1) NOT NULL,
	[LoanApplicationId] [int] NOT NULL,
	[GoldWeight] [decimal](10, 2) NOT NULL,
	[GoldPurity] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GoldLoanAppId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GoldLoanDetails]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoldLoanDetails](
	[LoanProductId] [int] NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[TenureMonths] [int] NOT NULL,
	[ProcessingFee] [decimal](18, 2) NOT NULL,
	[GoldPurityRequired] [nvarchar](20) NOT NULL,
	[RepaymentType] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeLoanApplications]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeLoanApplications](
	[HomeLoanAppId] [int] IDENTITY(1,1) NOT NULL,
	[LoanApplicationId] [int] NOT NULL,
	[PropertyAddress] [varchar](500) NOT NULL,
	[DownPaymentPercentage] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HomeLoanAppId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeLoanDetails]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeLoanDetails](
	[LoanProductId] [int] NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[TenureMonths] [int] NOT NULL,
	[ProcessingFee] [decimal](18, 2) NOT NULL,
	[DownPaymentPercentage] [decimal](5, 2) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanApplications]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanApplications](
	[LoanApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[DOB] [date] NOT NULL,
	[Aadhaar] [nvarchar](12) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Income] [decimal](18, 2) NOT NULL,
	[EmploymentType] [nvarchar](50) NOT NULL,
	[LoanProductId] [int] NOT NULL,
	[RequestedAmount] [decimal](18, 2) NOT NULL,
	[RequestedTenure] [int] NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[DecisionDate] [datetime] NULL,
	[DecisionReason] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Aadhaar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanBankStatements]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanBankStatements](
	[BankStatementId] [int] IDENTITY(1,1) NOT NULL,
	[LoanApplicationId] [int] NOT NULL,
	[DocumentName] [nvarchar](100) NOT NULL,
	[DocumentPath] [nvarchar](500) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[RejectionReason] [nvarchar](255) NULL,
	[VerifiedBy] [int] NULL,
	[VerifiedAt] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[BankStatementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanEnquiries]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanEnquiries](
	[EnquiryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[LoanAmountRequired] [decimal](18, 2) NOT NULL,
	[LoanPurpose] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EnquiryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanProductDocuments]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanProductDocuments](
	[LoanProductDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[LoanProductId] [int] NOT NULL,
	[DocumentName] [nvarchar](100) NOT NULL,
	[DocumentContent] [varbinary](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[LoanApplicationId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanProductDocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanProducts]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanProducts](
	[LoanProductId] [int] IDENTITY(1,1) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[MaxLoanAmount] [decimal](18, 2) NOT NULL,
	[LoanType] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanRepaymentSchedule]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanRepaymentSchedule](
	[RepaymentId] [int] IDENTITY(1,1) NOT NULL,
	[LoanApplicationId] [int] NOT NULL,
	[InstallmentNumber] [int] NOT NULL,
	[DueDate] [date] NOT NULL,
	[PrincipalAmount] [decimal](18, 2) NOT NULL,
	[InterestAmount] [decimal](18, 2) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[RepaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[Level] [nvarchar](20) NOT NULL,
	[UserType] [nvarchar](50) NULL,
	[UserId] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NOT NULL,
	[ApiEndpoint] [nvarchar](255) NULL,
	[ApiMethod] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentTransactions]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentTransactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[LoanApplicationId] [int] NOT NULL,
	[RepaymentId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[TransactionStatus] [nvarchar](20) NOT NULL,
	[TransactionReference] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalLoanDetails]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalLoanDetails](
	[LoanProductId] [int] NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[TenureMonths] [int] NOT NULL,
	[ProcessingFee] [decimal](18, 2) NOT NULL,
	[MinSalaryRequired] [decimal](18, 2) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/28/2025 7:20:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DecisionAppLogs] ADD  DEFAULT (getdate()) FOR [ProcessedAt]
GO
ALTER TABLE [dbo].[DecisionAppLogs] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[DecisionAppLogs] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[FDApplications] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FDApplications] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[FDTransactions] ADD  DEFAULT (getdate()) FOR [TransactionDate]
GO
ALTER TABLE [dbo].[FDTransactions] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FDTransactions] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[FDTypes] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FDTypes] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[GoldLoanDetails] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[GoldLoanDetails] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[HomeLoanDetails] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[HomeLoanDetails] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanApplications] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoanApplications] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanBankStatements] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[LoanBankStatements] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoanBankStatements] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanEnquiries] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanProductDocuments] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoanProductDocuments] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanProducts] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoanProducts] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanRepaymentSchedule] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[LoanRepaymentSchedule] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoanRepaymentSchedule] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[PaymentTransactions] ADD  DEFAULT (getdate()) FOR [PaymentDate]
GO
ALTER TABLE [dbo].[PaymentTransactions] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PaymentTransactions] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[PersonalLoanDetails] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PersonalLoanDetails] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[DecisionAppLogs]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[FDApplications]  WITH CHECK ADD FOREIGN KEY([FDTypeId])
REFERENCES [dbo].[FDTypes] ([FDTypeId])
GO
ALTER TABLE [dbo].[FDApplications]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[FDTransactions]  WITH CHECK ADD FOREIGN KEY([FDApplicationId])
REFERENCES [dbo].[FDApplications] ([FDApplicationId])
GO
ALTER TABLE [dbo].[GoldLoanApplications]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[GoldLoanDetails]  WITH CHECK ADD FOREIGN KEY([LoanProductId])
REFERENCES [dbo].[LoanProducts] ([LoanProductId])
GO
ALTER TABLE [dbo].[HomeLoanApplications]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[HomeLoanDetails]  WITH CHECK ADD FOREIGN KEY([LoanProductId])
REFERENCES [dbo].[LoanProducts] ([LoanProductId])
GO
ALTER TABLE [dbo].[LoanApplications]  WITH CHECK ADD FOREIGN KEY([LoanProductId])
REFERENCES [dbo].[LoanProducts] ([LoanProductId])
GO
ALTER TABLE [dbo].[LoanApplications]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[LoanBankStatements]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[LoanBankStatements]  WITH CHECK ADD FOREIGN KEY([VerifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[LoanProductDocuments]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[LoanProductDocuments]  WITH CHECK ADD FOREIGN KEY([LoanProductId])
REFERENCES [dbo].[LoanProducts] ([LoanProductId])
GO
ALTER TABLE [dbo].[LoanRepaymentSchedule]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[PaymentTransactions]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[PaymentTransactions]  WITH CHECK ADD FOREIGN KEY([RepaymentId])
REFERENCES [dbo].[LoanRepaymentSchedule] ([RepaymentId])
GO
ALTER TABLE [dbo].[PersonalLoanDetails]  WITH CHECK ADD FOREIGN KEY([LoanProductId])
REFERENCES [dbo].[LoanProducts] ([LoanProductId])
GO
ALTER TABLE [dbo].[FDApplications]  WITH CHECK ADD CHECK  (([Status]='Rejected' OR [Status]='Closed' OR [Status]='Matured' OR [Status]='Active' OR [Status]='Pending'))
GO
ALTER TABLE [dbo].[FDTransactions]  WITH CHECK ADD CHECK  (([TransactionType]='Refund' OR [TransactionType]='PrematureWithdrawal' OR [TransactionType]='MaturityPayout' OR [TransactionType]='InterestPayout' OR [TransactionType]='Deposit'))
GO
ALTER TABLE [dbo].[FDTransactions]  WITH CHECK ADD CHECK  (([TransactionStatus]='Pending' OR [TransactionStatus]='Failed' OR [TransactionStatus]='Success'))
GO
ALTER TABLE [dbo].[LoanApplications]  WITH CHECK ADD CHECK  (([EmploymentType]='Salaried' OR [EmploymentType]='Self-Employed'))
GO
ALTER TABLE [dbo].[LoanApplications]  WITH CHECK ADD CHECK  (([Status]='Rejected' OR [Status]='Approved' OR [Status]='Decision Pending' OR [Status]='Documents Collected' OR [Status]='In Processing' OR [Status]='Initial Review'))
GO
ALTER TABLE [dbo].[LoanBankStatements]  WITH CHECK ADD CHECK  (([Status]='Rejected' OR [Status]='Verified' OR [Status]='Pending'))
GO
ALTER TABLE [dbo].[LoanProducts]  WITH CHECK ADD CHECK  (([LoanType]='GOLD' OR [LoanType]='PERSONAL' OR [LoanType]='HOME'))
GO
ALTER TABLE [dbo].[LoanRepaymentSchedule]  WITH CHECK ADD CHECK  (([Status]='Overdue' OR [Status]='Paid' OR [Status]='Pending'))
GO
ALTER TABLE [dbo].[PaymentTransactions]  WITH CHECK ADD CHECK  (([TransactionStatus]='Pending' OR [TransactionStatus]='Failed' OR [TransactionStatus]='Success'))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Role]='Customer' OR [Role]='Admin'))
GO
select * from Users;
select * from LoanProducts;
select * from HomeLoanDetails;
select * from PersonalLoanDetails;
select * from GoldLoanDetails;
select * from LoanApplications;
select * from LoanRepaymentSchedule
select * from LoanEnquiries;
select * from Logs;
select * from LoanProductDocuments;
select * from FDApplications;
select * from FDTransactions;
select * from [dbo].[__EFMigrationsHistory]
SELECT * FROM GoldLoanApplications;

 SELECT * FROM LoanApplications WHERE LoanApplicationId = 2

 -- Check for missing users
SELECT * FROM LoanApplications WHERE UserId NOT IN (SELECT UserId FROM Users);

-- Check for missing loan products
SELECT * FROM LoanApplications WHERE LoanProductId NOT IN (SELECT LoanProductId FROM LoanProducts);

SELECT COUNT(*) FROM LoanApplications;

-- Check applications with their relationships
SELECT 
    la.LoanApplicationId,
    la.Status,
    la.UserId,
    la.LoanProductId,
    CASE WHEN u.UserId IS NOT NULL THEN 'Found' ELSE 'Not Found' END as UserExists,
    CASE WHEN lp.LoanProductId IS NOT NULL THEN 'Found' ELSE 'Not Found' END as LoanProductExists
FROM LoanApplications la
LEFT JOIN Users u ON la.UserId = u.UserId
LEFT JOIN LoanProducts lp ON la.LoanProductId = lp.LoanProductId;


-- Applications with missing users
SELECT * FROM LoanApplications WHERE UserId NOT IN (SELECT UserId FROM Users);

-- Applications with missing loan products
SELECT * FROM LoanApplications WHERE LoanProductId NOT IN (SELECT LoanProductId FROM LoanProducts);



SELECT * FROM LoanApplications WHERE UserId NOT IN (SELECT UserId FROM Users);
SELECT * FROM LoanApplications WHERE LoanProductId NOT IN (SELECT LoanProductId FROM LoanProducts);