using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Update;

internal class UpdateCardTest : BaseTest
{
    [Test]
    public async Task CheckUpdateCard()
    {
        var updatedName = "Updated Name" + DateTime.Now;
        var request = RequestWithAuth(CardsEndpoints.UpdateCardUrl)
            .AddUrlSegment("id", UrlParamValues.CardIdToUpdate)
            .AddJsonBody(new Dictionary<string, string> { { "name", updatedName} });
        var response = await _client.PutAsync(request);
        var responseContent = JToken.Parse(response.Content ?? string.Empty);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var nameToken = responseContent.SelectToken("name") ?? throw new InvalidOperationException("Token 'name' not found in the response.");
        Assert.That(updatedName, Is.EqualTo(nameToken.ToString()));

        await CheckGetCardFindsUpdatedCard(UrlParamValues.CardIdToUpdate, updatedName);
    }

    private async Task CheckGetCardFindsUpdatedCard(string cardId, string name)
    {
        var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
            .AddQueryParameter("field", "id,name")
            .AddUrlSegment("id", cardId);
        var response = await _client.GetAsync(request);
        var responseContent = JToken.Parse(response.Content ?? string.Empty);
        var nameToken = responseContent.SelectToken("name") ?? throw new InvalidOperationException("Token 'name' not found in the response.");
        Assert.That(name, Is.EqualTo(nameToken.ToString()));
    }
}
