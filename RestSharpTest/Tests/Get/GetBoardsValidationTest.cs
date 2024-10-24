using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Get;

internal class GetBoardsValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]
    public async Task CheckGetBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams); // used to work with type Parameter
        var response = await _client.ExecuteGetAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(validationArguments.StatusCode));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public async Task CheckGetBoardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
        var response = await _client.ExecuteGetAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    public async Task CheckGetBoardWithAnotherUserCredentials()
    {
        var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
        var response = await _client.ExecuteGetAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo("invalid token"));
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