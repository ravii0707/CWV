// CredWiseAdmin.Core/DTOs/ApiResponse.cs
namespace CredWiseAdmin.Core.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<ApiError> Errors { get; set; } = new List<ApiError>(); // Added this line

        public static ApiResponse<T> CreateSuccess(T data, string message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message ?? "Operation completed successfully",
                Data = data,
                Errors = new List<ApiError>() // Added this line
            };
        }

        public static ApiResponse<T> CreateError(string message, List<ApiError> errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = errors ?? new List<ApiError>() // Added this line
            };
        }
    }

    public class ApiError // Added this class
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}