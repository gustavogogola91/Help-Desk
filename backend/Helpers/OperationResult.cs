namespace backend.Helpers
{
    public class OperationResult
    {
        public OperationResult(bool isSucess, List<FluentValidation.Results.ValidationFailure> errorMessage, string? errorCode)
        {
            IsSucess = isSucess;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public bool IsSucess { get; set; }
        public List<FluentValidation.Results.ValidationFailure> ErrorMessage { get; set; }
        public string? ErrorCode { get; set; }
    }
}