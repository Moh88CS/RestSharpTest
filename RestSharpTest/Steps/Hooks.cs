using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using RestSharpTest.Utils;
using System.Net;
using TechTalk.SpecFlow;

namespace RestSharpTest.Steps;

[Binding]
public class Hooks
{
    private readonly TestContext testContext;

    public Hooks(TestContext testContext)
    {
        this.testContext = testContext;
    }

    [BeforeScenario("@CreateBoard")]
    public void CreateBoard()
    {
        testContext.BoardName = "New Board" + DateTime.Now;
        var request = RestRequestProvider.RequestWithAuth()
            .AddJsonBody(new Dictionary<string, string> { { "name", testContext.BoardName } });
        request.Resource = EndpointProvider.GetUrl(Endpoint.CreateABoard);
        var response = testContext.Client.PostAsync(request).Result;
        testContext.BoardId = JToken.Parse(response.Content ?? string.Empty).SelectToken("id")?.ToString() ?? throw new InvalidOperationException("Token 'id' not found in the response.");
    }

    [AfterScenario("@DeleteBoard")]
    public void DeleteBoard()
    {
        var request = RestRequestProvider.RequestWithAuth()
            .AddUrlSegment("id", testContext.BoardId!); // Using null-forgiving operator ! This guartnees that it wont be null which is valid here.
                                                    // otherwise safest appreach is to null check with if statement and throw exception
        request.Resource = EndpointProvider.GetUrl(Endpoint.DeleteABoard);
        var response = testContext.Client.DeleteAsync(request).Result;

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
