using RestSharp;

namespace RestSharpTest.Arguments.Holders;

internal class AuthValidationArgumentsHolder
{
    public required IEnumerable<Parameter> AuthParams { get; set; } // The required modifier indicates that the field or property it's applied to must be initialized
    public required string ErrorMessage { get; set; }
}