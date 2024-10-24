using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Create;

internal class CreateBoardTest : BaseTest
{
    private string? _createdBoardId; //Compiler Behavior: With nullable reference types enabled,
                                     //the compiler will provide warnings if you try to use _createdBoardId
                                     //without checking for null, promoting safer code.

    [Test]
    public async Task CheckCreateBoard()
    {
        var boardName = "New Board" + DateTime.Now;
        var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
            .AddJsonBody(new Dictionary<string, string> { { "name", boardName } });
        var response = await _client.PostAsync(request);
        var responseContent = JToken.Parse(response.Content ?? string.Empty);
        _createdBoardId = responseContent.SelectToken("id")?.ToString() ?? throw new InvalidOperationException("Token 'id' not found in the response.");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var nameToken = responseContent.SelectToken("name") ?? throw new InvalidOperationException("Token 'name' not found in the response.");
        Assert.That(nameToken.ToString(), Is.EqualTo(boardName));

        await CheckGetAllBoardsContainsCreatedBoard(boardName);
    }

    private async Task CheckGetAllBoardsContainsCreatedBoard(string boardName) // can also have GetBoard copycat take just the id and find the specific board
    {
        var request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
            .AddQueryParameter("field", "id,name")
            .AddUrlSegment("member", UrlParamValues.UserName);
        var response = await _client.GetAsync(request);
        var responseContent = JToken.Parse(response.Content?.ToString() ?? string.Empty);
        bool childToken = responseContent.Children().Select(token => token.SelectToken("name")).Contains(boardName);
        Assert.That(childToken, Is.True);
    }

    [TearDown]
    public async Task DeleteCreatedBoard()
    {
        var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
            .AddUrlSegment("id", _createdBoardId!); // Using null-forgiving operator ! This guartnees that it wont be null which is valid here.
                                                    // otherwise safest appreach is to null check with if statement and throw exception
        var response = await _client.DeleteAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
