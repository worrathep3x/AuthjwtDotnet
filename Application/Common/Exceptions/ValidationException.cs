using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class ValidationException : Exception
{
    public string ErrorMessage { get; set; }

    public ValidationException() : base("One or more validation failures have occurred.")
    {
        ErrorMessage = string.Empty;
    }

    public ValidationException(List<ValidationFailure> failures) : this()
    {
        var msgList = failures.Select(m => $"{m.ErrorCode}: {m.ErrorMessage}").ToList();
        if (msgList.Any())
            ErrorMessage = string.Join(", ", msgList);
    }
}
