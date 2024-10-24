using RestSharp;
using System.Net;

namespace RestSharpTest.Arguments.Holders;

internal class CardIdValidationArgumentsHolder
{
    public required IEnumerable<Parameter> PathParams { get; set; } // used in path params to store different types of params in one place
    public required string ErrorMessage { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}