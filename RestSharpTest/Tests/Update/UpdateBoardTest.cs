using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Update;

internal class UpdateBoardTest : BaseTest
{
    [Test]
    public async Task CheckUpdateBoard()
    {
        var updatedName = "Updated Name" + DateTime.Now;
        var request = RequestWithAuth(BoardsEndpoints.UpdateBoardUrl)
            .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
            .AddJsonBody(new Dictionary<string, string> { { "name", updatedName} });
        var response = await _client.PutAsync(request);
        var responseContent = JToken.Parse(response.Content ?? string.Empty);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var nameToken = responseContent.SelectToken("name") ?? throw new InvalidOperationException("Token 'name' not found in the response.");
        Assert.That(updatedName, Is.EqualTo(nameToken.ToString()));

        await CheckGetBoardFindsUpdatedBoard(UrlParamValues.BoardIdToUpdate, updatedName);
    }

    private async Task CheckGetBoardFindsUpdatedBoard(string boardId, string name)
    {
        var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
            .AddUrlSegment("id", boardId);
        var response = await _client.GetAsync(request);
        var responseContent = JToken.Parse(response.Content ?? string.Empty);
        var nameToken = responseContent.SelectToken("name") ?? throw new InvalidOperationException("Token 'name' not found in the response.");
        Assert.That(name, Is.EqualTo(nameToken.ToString()));
    }
}
