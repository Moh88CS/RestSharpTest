using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Delete;

internal class DeleteCardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
    public async Task CheckDeleteCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(CardsEndpoints.DeleteCardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);
        var response = await _client.ExecuteDeleteAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(validationArguments.StatusCode));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(CardAuthValidationArgumentsProvider))]
    public async Task CheckDeleteCardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithoutAuth(CardsEndpoints.DeleteCardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", UrlParamValues.ExistingCardId);
        var response = await _client.ExecuteDeleteAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    public async Task CheckDeleteCardWithAnotherUserCredentials()
    {
        var request = RequestWithoutAuth(CardsEndpoints.DeleteCardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", UrlParamValues.ExistingCardId);
        var response = await _client.ExecuteDeleteAsync(request); // read note
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo("invalid token"));
    }
}
