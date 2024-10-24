namespace RestSharpTest.Arguments.Holders;

internal class CardBodyValidationArgumentsHolder
{
    public required IDictionary<string, object> BodyParams { get; set; } // used in path params to store different types of params in one place
    public required string ErrorMessage { get; set; }
}