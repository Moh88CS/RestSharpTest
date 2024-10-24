using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Create;

internal class CreateCardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(CardBodyValidationArgumentsProvider))]
    public async Task CheckCreateCardWithInvalidName(CardBodyValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(CardsEndpoints.CreateCardUrl)
            .AddJsonBody(validationArguments.BodyParams);
        var response = await _client.ExecutePostAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(CardAuthValidationArgumentsProvider))]
    public async Task CheckCreateCardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
    {
        var CardName = "New Card";
        var request = RequestWithoutAuth(CardsEndpoints.CreateCardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddJsonBody(new Dictionary<string, string> {
                { "name", CardName },
                { "idList", UrlParamValues.ExistingListId }
            });

        var response = await _client.ExecutePostAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }
}
