using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Create;

internal class CreateCardTest : BaseTest
{
    private string? _createdCardId; //Compiler Behavior: With nullable reference types enabled,
                                     //the compiler will provide warnings if you try to use _createdBoardId
                                     //without checking for null, promoting safer code.

    [Test]
    public async Task CheckCreateCard()
    {
        var cardName = "New Card" + DateTime.Now;
        var request = RequestWithAuth(CardsEndpoints.CreateCardUrl)
            .AddJsonBody(new Dictionary<string, string>
            {
                { "name", cardName },
                { "idList", UrlParamValues.ExistingListId }
            });
        var response = await _client.PostAsync(request);
        var responseContent = JToken.Parse(response.Content ?? string.Empty);

        _createdCardId = responseContent.SelectToken("id")?.ToString() ?? throw new InvalidOperationException("Token 'id' not found in the response.") ;

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var nameToken = responseContent.SelectToken("name") ?? throw new InvalidOperationException("Token 'name' not found in the response.");
        Assert.That(nameToken.ToString(), Is.EqualTo(cardName));

        await CheckGetAllCardsContainsCreatedCard(cardName);
    }

    private async Task CheckGetAllCardsContainsCreatedCard(string cardName) // can also have GetBoard copycat take just the id and find the specific board
    {
        var request = RequestWithAuth(CardsEndpoints.GetAllCardsUrl)
            .AddQueryParameter("field", "id,name")
            .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
        var response = await _client.GetAsync(request);

        var responseContent = JToken.Parse(response.Content?.ToString() ?? string.Empty);
        bool childToken = responseContent.Children().Select(token => token.SelectToken("name")).Contains(cardName);
        Assert.That(childToken, Is.True);
    }

    [TearDown]
    public async Task DeleteCreatedCard()
    {
        var request = RequestWithAuth(CardsEndpoints.DeleteCardUrl)
            .AddUrlSegment("id", _createdCardId!); // Using null-forgiving operator ! This guartnees that it wont be null which is valid here.
                                                    // otherwise safest appreach is to null check with if statement and throw exception
        var response = await _client.DeleteAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
