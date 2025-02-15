using System.Collections;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Consts;
namespace RestSharpTest.Arguments.Providers;

internal class AuthValidationArgumentsProvider : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new AuthValidationArgumentsHolder()
            {
                AuthParams = ArraySegment<Parameter>.Empty,
                ErrorMessage = "unauthorized permission requested"
            }
        };
        yield return new object[]
        {
            new AuthValidationArgumentsHolder()
            {
                AuthParams = 
                [
                    Parameter.CreateParameter("key", UrlParamValues.ValidKey, ParameterType.QueryString)
                ],
                ErrorMessage = "unauthorized permission requested"
            }
        };
        yield return new object[]
        {
            new AuthValidationArgumentsHolder()
            {
                AuthParams =
                [
                    Parameter.CreateParameter("token", UrlParamValues.ValidToken, ParameterType.QueryString)
                ],
                ErrorMessage = "invalid key"
            }
        };
    }
}