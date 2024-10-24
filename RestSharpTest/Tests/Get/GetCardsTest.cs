using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Get;

internal class GetCardsTest : BaseTest
{
    [Test]
    public async Task CheckGetCards()
    {
        var request = RequestWithAuth(CardsEndpoints.GetAllCardsUrl)
            .AddQueryParameter("field", "id,name")
            .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
        var response = await _client.GetAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var responseContent = JToken.Parse(response.Content ?? string.Empty);
        var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_cards.json"));
        Assert.That(responseContent.IsValid(jsonSchema));
    }

    [Test]
    public async Task CheckGetCard()
    {
        var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
            .AddQueryParameter("field", "id,name")
            .AddUrlSegment("id", UrlParamValues.ExistingCardId);
        var response = await _client.GetAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var responseContent = JToken.Parse(response.Content ?? string.Empty);
        var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_card.json"));
        Assert.That(responseContent.IsValid(jsonSchema));
        var nameToken = responseContent.SelectToken("name") ?? throw new InvalidOperationException("Token 'name' not found in the response.");
        Assert.That(nameToken.ToString(), Is.EqualTo("one more card"));
    }
}
