namespace EmailSender.Api.ProblemDetail
{
    public record FieldError(string field, string[] errors) { }
}
