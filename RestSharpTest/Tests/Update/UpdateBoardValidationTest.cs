using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Update;

internal class UpdateBoardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]
    public async Task CheckUpdateBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(BoardsEndpoints.UpdateBoardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);
        var response = await _client.ExecutePutAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(validationArguments.StatusCode));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public async Task CheckUpdateBoardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithoutAuth(BoardsEndpoints.UpdateBoardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
            .AddJsonBody(new Dictionary<string, string> { {"name", "Updated Name" } });
        var response = await _client.ExecutePutAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    public async Task CheckUpdateBoardWithAnotherUserCredentials()
    {
        var request = RequestWithoutAuth(BoardsEndpoints.UpdateBoardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
            .AddJsonBody(new Dictionary<string, string> { { "name", "Updated Name" } });
        var response = await _client.ExecutePutAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo("invalid token"));
    }
}
