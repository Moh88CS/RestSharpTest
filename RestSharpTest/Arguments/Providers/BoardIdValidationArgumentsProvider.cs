using RestSharp;
using RestSharpTest.Arguments.Holders;
using System.Collections;
using System.Net;

namespace RestSharpTest.Arguments.Providers;

internal class BoardIdValidationArgumentsProvider : IEnumerable // Exposes an enumerator, which supports a simple iteration over a non-generic collection. // read on yields
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new BoardIdValidationArgumentsHolder
            {
                ErrorMessage = "invalid id",
                StatusCode = HttpStatusCode.BadRequest,
                PathParams =
                [
                    Parameter.CreateParameter("id", "invalid", ParameterType.UrlSegment)
                ]
            } 
        };
        yield return new object[]
        {
            new BoardIdValidationArgumentsHolder
            {
                ErrorMessage = "The requested resource was not found.",
                StatusCode = HttpStatusCode.NotFound,
                PathParams =
                [
                    Parameter.CreateParameter("id", "670bfaf62656fd3e8f5e7540", ParameterType.UrlSegment)
                ]
            }
        };
    }
}
