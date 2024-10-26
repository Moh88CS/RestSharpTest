using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Utils;

public static class RestRequestProvider
{
    public static RestRequest RequestWithAuth() =>
       RequestWithoutAuth()
            .AddOrUpdateParameters(UrlParamValues.AuthQueryParams);

    public static RestRequest RequestWithoutAuth() =>
       new RestRequest();
}
