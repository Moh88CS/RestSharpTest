using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Create;

internal class CreateBoardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(BoardNameValidationArgumentsProvider))]
    public async Task CheckCreateBoardWithInvalidName(IDictionary<string, object> bodyParams)
    {
        var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
            .AddJsonBody(bodyParams);
        var response = await _client.ExecutePostAsync(request);
        var responseContent = JToken.Parse(response.Content ?? string.Empty);
        var messageToken = responseContent.SelectToken("message")?.ToString() ?? throw new InvalidOperationException("Token 'message' not found in the response.");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(messageToken, Is.EqualTo("invalid value for name"));
    }

    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public async Task CheckCreateBoardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments) // void type wont help with async
    {
        var boardName = "New Board";
        var request = RequestWithoutAuth(BoardsEndpoints.CreateBoardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddJsonBody(new Dictionary<string, string> { { "name", boardName } });

        var response = await _client.ExecutePostAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }
}
