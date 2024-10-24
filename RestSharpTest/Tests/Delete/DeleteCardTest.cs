using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Delete;

internal class DeleteCardTest : BaseTest
{
    private string _createdCardId;
    private string _createdCardName;

    [SetUp]
    public async Task CreateCard()
    {
        _createdCardName = "New Card" + DateTime.Now;
        var request = RequestWithAuth(CardsEndpoints.CreateCardUrl)
            .AddJsonBody(new Dictionary<string, string>
            {
                { "name", _createdCardName },
                { "idList", UrlParamValues.ExistingListId }
            });
        var response = await _client.PostAsync(request);
        _createdCardId = JToken.Parse(response.Content ?? string.Empty).SelectToken("id")?.ToString() ?? throw new InvalidOperationException("Token 'id' not found in the response.");
    }

    [Test]
    public async Task CheckDeleteCard()
    {
        var request = RequestWithAuth(CardsEndpoints.DeleteCardUrl)
            .AddUrlSegment("id", _createdCardId);
        var response = await _client.DeleteAsync(request);

        var valueToken = JToken.Parse(response.Content ?? string.Empty).SelectToken("limits")?.ToString() ?? throw new InvalidOperationException("Token 'limits' not found in the response."); ;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(valueToken, Is.EqualTo("{}"));

        await CheckGetAllCardsDoesNotContainCreatedCard(_createdCardName);
    }

    private async Task CheckGetAllCardsDoesNotContainCreatedCard(string cardName) // can also have GetBoard copycat take just the id and find the specific board
    {
        var request = RequestWithAuth(CardsEndpoints.GetAllCardsUrl)
            .AddQueryParameter("field", "id,name")
            .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
        var response = await _client.GetAsync(request);

        var responseContent = JToken.Parse(response.Content?.ToString() ?? string.Empty);
        bool childToken = responseContent.Children().Select(token => token.SelectToken("name")).Contains(cardName);
        Assert.That(childToken, Is.False);
    }
}
