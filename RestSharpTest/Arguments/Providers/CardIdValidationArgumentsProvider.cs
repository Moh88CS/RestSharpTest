using RestSharp;
using RestSharpTest.Arguments.Holders;
using System.Collections;
using System.Net;

namespace RestSharpTest.Arguments.Providers;

internal class CardIdValidationArgumentsProvider : IEnumerable // Exposes an enumerator, which supports a simple iteration over a non-generic collection. // read on yields
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new CardIdValidationArgumentsHolder
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
            new CardIdValidationArgumentsHolder
            {
                ErrorMessage = "The requested resource was not found.",
                StatusCode = HttpStatusCode.NotFound,
                PathParams =
                [
                    Parameter.CreateParameter("id", "670c4ddf8eeaca6bf1766960", ParameterType.UrlSegment)
                ]
            }
        };
    }
}
