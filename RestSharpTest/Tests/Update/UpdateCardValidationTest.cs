using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Update;

internal class UpdateCardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
    public async Task CheckUpdateCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(CardsEndpoints.UpdateCardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);
        var response = await _client.ExecutePutAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(validationArguments.StatusCode));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(CardAuthValidationArgumentsProvider))]
    public async Task CheckUpdateCardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithoutAuth(CardsEndpoints.UpdateCardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", UrlParamValues.CardIdToUpdate)
            .AddJsonBody(new Dictionary<string, string> { {"name", "Updated Name" } });
        var response = await _client.ExecutePutAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    public async Task CheckUpdateCardWithAnotherUserCredentials()
    {
        var request = RequestWithoutAuth(CardsEndpoints.UpdateCardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", UrlParamValues.CardIdToUpdate)
            .AddJsonBody(new Dictionary<string, string> { { "name", "Updated Name" } });
        var response = await _client.ExecutePutAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo("invalid key"));
    }
}
