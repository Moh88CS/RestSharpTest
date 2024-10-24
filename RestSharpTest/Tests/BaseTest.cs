using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests;

public class BaseTest
{
    private protected static IRestClient _client;

    [OneTimeSetUp]
    public static void InitializeRestClient() =>
        _client = new RestClient("https://api.trello.com");

    private protected RestRequest RequestWithAuth(string url) =>
       RequestWithoutAuth(url)
            .AddOrUpdateParameters(UrlParamValues.AuthQueryParams);

    private protected RestRequest RequestWithoutAuth(string url) =>
       new RestRequest(url);

    [OneTimeTearDown]
    public static void CleanupRestClient() =>
            _client?.Dispose(); // ?. is the Null-Conditional Operator
}
