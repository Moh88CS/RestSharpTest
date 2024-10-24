using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Delete;

internal class DeleteBoardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]
    public async Task CheckDeleteBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);
        var response = await _client.ExecuteDeleteAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(validationArguments.StatusCode));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public async Task CheckDeleteBoardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithoutAuth(BoardsEndpoints.DeleteBoardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
        var response = await _client.ExecuteDeleteAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    public async Task CheckDeleteBoardWithAnotherUserCredentials()
    {
        var request = RequestWithoutAuth(BoardsEndpoints.DeleteBoardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
        var response = await _client.ExecuteDeleteAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo("invalid token"));
    }
}
