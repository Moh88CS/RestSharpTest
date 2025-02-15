using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Get;

internal class GetCardsValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
    public async Task CheckGetCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);
        var response = await _client.ExecuteGetAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(validationArguments.StatusCode));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(CardAuthValidationArgumentsProvider))]
    public async Task CheckGetCardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithoutAuth(CardsEndpoints.GetCardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", UrlParamValues.ExistingCardId);
        var response = await _client.ExecuteGetAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    public async Task CheckGetCardWithAnotherUserCredentials()
    {
        var request = RequestWithoutAuth(CardsEndpoints.GetCardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", UrlParamValues.ExistingCardId);
        var response = await _client.ExecuteGetAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo("invalid key"));
    }
}


/*
 NOTE:
Get()
Purpose: Specifically designed to send GET requests to the server.
Behavior: In many versions of RestSharp, Get() will throw an exception (like HttpRequestException) 
if the server returns a non-success status code (e.g., 4xx or 5xx). This can make it difficult to handle 
expected error responses without catching exceptions.
Execute()
Purpose: A more general method that can send any type of request (GET, POST, PUT, DELETE, etc.),
depending on how you configure the request.
Behavior: Execute() does not throw exceptions for non-success status codes. Instead, 
it returns a response object regardless of the status code. This makes it easier 
to handle cases where you expect specific error codes (like 400 or 404).

 */