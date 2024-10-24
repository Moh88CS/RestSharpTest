using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Delete;

internal class DeleteBoardTest : BaseTest
{
    private string _createdBoardId;
    private string _createdBoardName;

    [SetUp]
    public async Task CreateBoard()
    {
        _createdBoardName = "New Board" + DateTime.Now;
        var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
            .AddJsonBody(new Dictionary<string, string> { { "name", _createdBoardName } });
        var response = await _client.PostAsync(request);
        _createdBoardId = JToken.Parse(response.Content ?? string.Empty).SelectToken("id")?.ToString() ?? throw new InvalidOperationException("Token 'id' not found in the response.");
    }

    [Test]
    public async Task CheckDeleteBoard()
    {
        var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
            .AddUrlSegment("id", _createdBoardId);
        var response = await _client.DeleteAsync(request);

        var valueToken = JToken.Parse(response.Content ?? string.Empty).SelectToken("_value")?.ToString() ?? throw new InvalidOperationException("Token '_value' not found in the response."); ;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(valueToken, Is.EqualTo(string.Empty));

        await CheckGetAllBoardsDoesNotContainCreatedBoard(_createdBoardName);
    }

    private async Task CheckGetAllBoardsDoesNotContainCreatedBoard(string boardName) // can also have GetBoard copycat take just the id and find the specific board
    {
        var request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
            .AddQueryParameter("field", "id,name")
            .AddUrlSegment("member", UrlParamValues.UserName);
        var response = await _client.GetAsync(request);
        var responseContent = JToken.Parse(response.Content?.ToString() ?? string.Empty);
        bool childToken = responseContent.Children().Select(token => token.SelectToken("name")).Contains(boardName);
        Assert.That(childToken, Is.False);
    }
}
