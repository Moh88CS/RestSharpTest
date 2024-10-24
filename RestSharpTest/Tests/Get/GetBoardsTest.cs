using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Get;

internal class GetBoardsTest : BaseTest
{
    [Test]
    public async Task CheckGetBoards()
    {
        var request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
            .AddQueryParameter("field", "id,name")
            .AddUrlSegment("member", UrlParamValues.UserName);
        var response = await _client.GetAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var responseContent = JToken.Parse(response.Content ?? string.Empty); // if null give empty string to avoid null values
        var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
        Assert.That(responseContent.IsValid(jsonSchema), Is.True);
    }

    [Test]
    public async Task CheckGetBoard()
    {
        var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
            .AddQueryParameter("field", "id,name")
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
        var response = await _client.GetAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var responseContent = JToken.Parse(response.Content ?? string.Empty);
        var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_board.json"));
        Assert.That(responseContent.IsValid(jsonSchema), Is.True);
        var nameToken = responseContent.SelectToken("name") ?? throw new InvalidOperationException("Token 'name' not found in the response.");
        Assert.That(nameToken.ToString(), Is.EqualTo("My Trello board"));
    }
}
