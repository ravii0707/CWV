# CredWise Admin Module - Implementation Guide

## Overview

The CredWise Admin module is a .NET Core-based application that manages loan and FD product configurations, application processing, user management, and email notifications. The application follows clean architecture principles and implements best practices for enterprise applications.

## Project Structure

The solution is organized into the following projects:

1. **CredWiseAdmin.API**

   - Controllers for handling HTTP requests
   - API configuration and middleware
   - Dependency injection setup
   - API documentation (Swagger)
   - User management endpoints
   - Email notification endpoints
   - Loan application processing endpoints
   - FD management endpoints

2. **CredWiseAdmin.Core**

   - Domain entities
   - DTOs (Data Transfer Objects)
   - Interfaces for repositories and services
   - Enums and constants
   - User-related models
   - Email-related models
   - Loan application models
   - FD application models

3. **CredWiseAdmin.Services**

   - Business logic implementation
   - Service interfaces
   - AutoMapper profiles
   - Validation logic
   - Email notification system
   - Email templates
   - User management services
   - Loan processing services
   - FD management services

4. **CredWiseAdmin.Repository**

   - Database context
   - Repository implementations
   - Entity configurations
   - Database migrations
   - User data access
   - Loan application data access
   - FD application data access

5. **CredWiseAdmin.Utils**
   - Common utility functions
   - Helper classes
   - Extension methods
   - Constants and configurations

## Key Features

1. **User Management**

   - User registration and authentication
   - Role-based access control (Admin/Customer)
   - User profile management
   - Password management
   - User status tracking

2. **Loan Management**

   - Multiple loan types (Personal, Home, Gold)
   - Loan application processing
   - Document verification
   - Loan approval workflow
   - Repayment schedule management
   - Payment tracking

3. **Fixed Deposit Management**

   - FD product configuration
   - FD application processing
   - Interest rate management
   - Maturity tracking
   - Transaction management

4. **Document Management**

   - Document upload and storage
   - Document verification
   - Bank statement processing
   - Document status tracking

5. **Email Notification System**

   - User registration notifications
   - Loan approval notifications
   - Loan rejection notifications
   - Payment confirmation notifications
   - FD maturity notifications
   - HTML email templates with responsive design
   - Secure email handling

6. **Logging and Monitoring**
   - Application logs
   - Decision logs
   - Transaction logs
   - Audit trails
   - Performance monitoring

## Database Schema

The application uses SQL Server with the following key tables:

1. **User Management**

   - Users
   - Logs

2. **Loan Management**

   - LoanProducts
   - LoanApplications
   - HomeLoanDetails
   - PersonalLoanDetails
   - GoldLoanDetails
   - LoanRepaymentSchedule
   - PaymentTransactions
   - LoanBankStatements
   - LoanProductDocuments
   - DecisionAppLogs

3. **Fixed Deposit Management**

   - FDTypes
   - FDApplications
   - FDTransactions

4. **Document Management**
   - LoanProductDocuments
   - LoanBankStatements

## Implementation Details

### 1. Core Layer (CredWiseAdmin.Core)

The core layer contains the domain entities and interfaces that define the business rules and data structures.

#### Key Entities:

- `User`: Represents a system user with role-based access
- `LoanApplication`: Represents a loan application with type-specific details
- `LoanProduct`: Represents a loan product configuration
- `FDApplication`: Represents a fixed deposit application
- `FDType`: Represents a fixed deposit product configuration
- `Document`: Represents uploaded documents and bank statements

#### Key Interfaces:

- `IUserRepository`: Repository interface for user management
- `IUserService`: Service interface for user operations
- `ILoanRepository`: Repository interface for loan operations
- `ILoanService`: Service interface for loan processing
- `IFDRepository`: Repository interface for FD operations
- `IFDService`: Service interface for FD management
- `IDocumentRepository`: Repository interface for document management
- `IUnitOfWork`: Unit of work pattern interface
- `IEmailService`: Service interface for email notifications

### 2. Repository Layer (CredWiseAdmin.Repository)

The repository layer handles data access and persistence.

#### Key Components:

- `ApplicationDbContext`: Entity Framework Core context
- `GenericRepository<T>`: Base repository implementation
- `UnitOfWork`: Manages transactions and repository instances
- `UserRepository`: Handles user data access
- `LoanRepository`: Handles loan application data access
- `FDRepository`: Handles FD application data access
- `DocumentRepository`: Handles document storage and retrieval

### 3. Service Layer (CredWiseAdmin.Services)

The service layer implements business logic and orchestrates operations.

#### Key Services:

- `UserService`: Manages user operations
- `LoanService`: Manages loan processing
- `FDService`: Manages FD operations
- `DocumentService`: Handles document processing
- `EmailService`: Handles email notifications
- `PaymentService`: Manages payment processing
- `DecisionService`: Handles loan approval decisions

#### Loan Processing:

- Application validation
- Document verification
- Credit assessment
- Approval workflow
- Repayment schedule generation
- Payment tracking

#### Fixed Deposit Management:

- FD product configuration
- Application processing
- Interest calculation
- Maturity tracking
- Transaction management

#### Document Management:

- Document upload and storage
- Bank statement processing
- Document verification
- Status tracking
- Secure storage

#### Email Notification System:

The email notification system is implemented in the `EmailService` class and includes:

1. **User Registration Email**

   - Sends welcome email with login credentials
   - Includes login URL and password
   - Styled with professional HTML template
   - Security recommendations

2. **Loan Approval Email**

   - Notifies users of approved loan applications
   - Includes loan application ID
   - Provides next steps and support contact
   - Clear call-to-action buttons

3. **Loan Rejection Email**

   - Notifies users of rejected loan applications
   - Includes rejection reason
   - Provides support contact information
   - Offers guidance for future applications
   - Professional and empathetic tone

4. **Payment Confirmation Email**

   - Confirms successful payment
   - Includes transaction ID and date
   - Provides transaction details
   - Includes record-keeping reminder
   - Clear transaction summary

5. **FD Maturity Email**
   - Notifies users of upcoming FD maturity
   - Includes maturity date and amount
   - Provides renewal options
   - Clear instructions for next steps

#### Email Templates:

All email templates are stored in the `EmailTemplates` directory and include:

- Responsive HTML design
- Professional styling
- Clear typography
- Color-coded status indicators
- Security notices
- Footer with important information
- Mobile-friendly layout
- Accessible design

### 4. API Layer (CredWiseAdmin.API)

The API layer exposes endpoints for client applications.

#### Key Components:

- `UsersController`: Handles user management operations
- `LoanController`: Handles loan application operations
- `FDController`: Handles FD application operations
- `DocumentController`: Handles document operations
- `PaymentController`: Handles payment operations
- `ErrorHandlingMiddleware`: Global error handling
- `ServiceCollectionExtensions`: Dependency injection setup
- `EmailsController`: Handles email-related operations

#### User Management Endpoints:

- POST /api/admin/users/register
- POST /api/admin/users/login
- GET /api/admin/users/{id}
- PUT /api/admin/users/{id}
- PUT /api/admin/users/{id}/deactivate

#### Loan Management Endpoints:

- GET /api/admin/loans
- POST /api/admin/loans
- GET /api/admin/loans/{id}
- PUT /api/admin/loans/{id}
- POST /api/admin/loans/{id}/documents
- GET /api/admin/loans/{id}/status
- POST /api/admin/loans/{id}/approve
- POST /api/admin/loans/{id}/reject

#### FD Management Endpoints:

- GET /api/admin/fds
- POST /api/admin/fds
- GET /api/admin/fds/{id}
- PUT /api/admin/fds/{id}
- GET /api/admin/fds/{id}/transactions
- POST /api/admin/fds/{id}/maturity

#### Document Management Endpoints:

- POST /api/admin/documents/upload
- GET /api/admin/documents/{id}
- PUT /api/admin/documents/{id}/verify
- GET /api/admin/documents/loan/{loanId}

#### Payment Management Endpoints:

- POST /api/admin/payments
- GET /api/admin/payments/{id}
- GET /api/admin/payments/loan/{loanId}
- POST /api/admin/payments/verify

#### Email Testing Endpoints:

- POST /api/admin/users/test-send-credentials
- POST /api/admin/emails/send-registration
- POST /api/admin/emails/send-loan-approval
- POST /api/admin/emails/send-loan-rejection
- POST /api/admin/emails/send-payment-confirmation
- POST /api/admin/emails/send-fd-maturity

## Best Practices

1. **Error Handling**

   - Global exception handling middleware
   - Custom exception types
   - Structured error responses
   - Detailed error logging
   - Transaction rollback on errors

2. **Validation**

   - FluentValidation for DTOs
   - Custom validation attributes
   - Service-level validation
   - Input sanitization
   - Business rule validation

3. **Logging**

   - Structured logging with Serilog
   - Correlation IDs for request tracking
   - Appropriate log levels
   - Audit logging for user actions
   - Performance metrics logging

4. **Security**

   - JWT-based authentication
   - Role-based authorization
   - Secure configuration management
   - Secure email handling
   - Password hashing with BCrypt
   - Input validation and sanitization
   - Document encryption
   - API rate limiting

5. **Performance**

   - Async/await throughout
   - Caching implementation
   - Optimized database queries
   - Non-blocking email sending
   - Efficient document handling
   - Connection pooling
   - Query optimization

6. **Testing**
   - Unit tests for services
   - Integration tests for repositories
   - API functional tests
   - Email template testing
   - Performance testing
   - Security testing

## Configuration

### Database

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=CredWiseDB;Trusted_Connection=True;"
  }
}
```

### Email Configuration

```json
{
  "Email": {
    "Host": "smtp.example.com",
    "Port": 587,
    "Username": "noreply@credwise.com",
    "Password": "your-smtp-password",
    "From": "noreply@credwise.com",
    "DisplayName": "CredWise Admin"
  },
  "AppSettings": {
    "LoginUrl": "https://app.credwise.com/login",
    "SupportEmail": "support@credwise.com",
    "SupportPhone": "+1 (800) 123-4567"
  }
}
```

### Authentication

```json
{
  "Jwt": {
    "Secret": "your-secret-key",
    "Issuer": "credwise.com",
    "Audience": "credwise-users",
    "ExpiryInMinutes": 60
  }
}
```

### Document Storage

```json
{
  "DocumentStorage": {
    "BasePath": "C:\\CredWise\\Documents",
    "MaxFileSize": 10485760,
    "AllowedExtensions": [".pdf", ".jpg", ".jpeg", ".png"],
    "EncryptionKey": "your-encryption-key"
  }
}
```

### Logging

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    },
    "Serilog": {
      "MinimumLevel": "Information",
      "WriteTo": [
        {
          "Name": "File",
          "Args": {
            "path": "logs/log-.txt",
            "rollingInterval": "Day"
          }
        }
      ]
    }
  }
}
```

## Development Guidelines

1. **Code Organization**

   - Follow clean architecture principles
   - Use dependency injection
   - Implement proper separation of concerns
   - Maintain consistent file structure
   - Follow SOLID principles

2. **Naming Conventions**

   - Use PascalCase for public members
   - Use camelCase for private members
   - Use meaningful names
   - Follow C# naming conventions
   - Use consistent prefixes/suffixes

3. **Documentation**

   - XML comments for public APIs
   - README files for each project
   - API documentation with Swagger
   - Keep email templates well-documented
   - Maintain clear configuration documentation
   - Document database schema changes

4. **Email Template Guidelines**

   - Use responsive design
   - Include clear call-to-actions
   - Maintain consistent branding
   - Ensure accessibility
   - Test across email clients
   - Follow email best practices
   - Include unsubscribe options

5. **Version Control**
   - Use feature branches
   - Write meaningful commit messages
   - Review code before merging
   - Keep commits focused and atomic
   - Follow Git flow workflow
   - Maintain clean history

## Security Considerations

### Authentication

- JWT-based authentication
- Secure password storage with BCrypt
- Token refresh mechanism
- Session management
- Multi-factor authentication support

### Authorization

- Role-based access control
- Resource-level permissions
- API endpoint security
- User role validation
- Action-based permissions

### Data Protection

- Encrypted sensitive data
- Secure configuration storage
- Audit logging
- Data backup encryption
- Email security
- Document encryption
- Secure file storage

## Next Steps

1. **Planned Features**

   - Enhanced user profile management
   - Additional email templates
   - Advanced reporting
   - Audit trail system
   - Real-time notifications
   - Mobile app integration

2. **Improvements**

   - Performance optimization
   - Enhanced error handling
   - Additional security measures
   - Extended testing coverage
   - API versioning
   - Caching improvements

3. **Documentation**
   - API documentation updates
   - User guides
   - Deployment guides
   - Troubleshooting guides
   - Security guidelines
   - Performance tuning guide

## Deployment

### Prerequisites

- .NET 6.0 SDK
- SQL Server 2019 or later
- Required third-party DLLs in the Libs folder
- SMTP server access
- Document storage location
- SSL certificates

### Build Process

1. Run database migrations:
   ```bash
   dotnet ef database update
   ```
2. Build the solution:
   ```bash
   dotnet build
   ```
3. Run tests:
   ```bash
   dotnet test
   ```

### Deployment Steps

1. Deploy database migrations
2. Configure environment variables
3. Deploy application files
4. Configure IIS or other hosting environment
5. Set up SSL certificates
6. Configure logging and monitoring
7. Set up document storage
8. Configure email settings

## Monitoring and Maintenance

### Health Checks

- Database connectivity
- External service dependencies
- Application status
- Memory usage
- CPU utilization
- Document storage access
- Email service status

### Performance Monitoring

- Response times
- Resource usage
- Error rates
- Database query performance
- API endpoint metrics
- Document processing times
- Email delivery rates

### Backup and Recovery

- Daily database backups
- Configuration backups
- Log file rotation
- Disaster recovery plan
- Backup verification process
- Document storage backup
- Email template backup

## Support and Troubleshooting

### Common Issues

1. Database Connection Problems

   - Check connection string
   - Verify SQL Server is running
   - Check network connectivity
   - Verify permissions

2. Authentication Failures

   - Verify API keys
   - Check token expiration
   - Validate user permissions
   - Check JWT configuration

3. Performance Issues

   - Monitor database queries
   - Check resource utilization
   - Review application logs
   - Analyze slow endpoints

4. Document Processing Issues
   - Check storage permissions
   - Verify file size limits
   - Check supported formats
   - Monitor disk space

### Logging and Diagnostics

- Application logs in `./logs` directory
- Database logs in SQL Server
- System event logs
- IIS logs (if applicable)
- Document processing logs
- Email delivery logs

### Contact Information

- Development Team: dev@credwise.com
- Database Administrators: dba@credwise.com
- System Administrators: sysadmin@credwise.com
- Support Team: support@credwise.com

## API Documentation

### Swagger UI

Access the API documentation at `/swagger` endpoint when running the application.

### Key Endpoints

1. Loan Types

   - GET /api/loantypes
   - POST /api/loantypes
   - PUT /api/loantypes/{id}
   - DELETE /api/loantypes/{id}

2. FD Types

   - GET /api/fdtypes
   - POST /api/fdtypes
   - PUT /api/fdtypes/{id}
   - DELETE /api/fdtypes/{id}

3. Applications
   - GET /api/applications
   - POST /api/applications
   - PUT /api/applications/{id}
   - GET /api/applications/{id}/status

## Security Considerations

### Authentication

- JWT-based authentication
- Token refresh mechanism
- Secure password policies

### Authorization

- Role-based access control
- Resource-level permissions
- API endpoint security

### Data Protection

- Encrypted sensitive data
- Secure configuration storage
- Audit logging
- Data backup encryption
