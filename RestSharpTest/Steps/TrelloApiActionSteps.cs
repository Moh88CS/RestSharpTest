using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using RestSharpTest.Utils;
using TechTalk.SpecFlow;

namespace RestSharpTest.Steps;

[Binding]
public class TrelloApiActionSteps
{
    private readonly TestContext testContext;

    public TrelloApiActionSteps(TestContext testContext)
    {
        this.testContext = testContext;
    }

    [Given("a request {} authorization")]
    public void ARequestAuthorization(bool withAuth)
    {
        testContext.Request = withAuth ? RestRequestProvider.RequestWithAuth() : RestRequestProvider.RequestWithoutAuth();

    }

    [Given("the request has query params:")] // word is only one word unlike free string
    public void TheRequestHasQueryParam(IDictionary<string, string> rows)
    {
        foreach (var row in rows)
        {
            testContext.Request = testContext.Request.AddQueryParameter(row.Key, row.Value);
        }
    }

    [Given("the request has path params:")]
    public void TheRequestHasPathParam(Table table)
    {
        foreach (var row in table.Rows)
        {
            var rowValue = row["value"];
            var value = rowValue.Equals("created_board_id") ? testContext.BoardId : rowValue;
            testContext.Request = testContext.Request.AddUrlSegment(row["name"], value);
        }
    }

    [Given("the request has body params:")]
    public void TheRequestHasBodyParamWithValue(Table table)
    {

        testContext.Request = testContext.Request.AddJsonBody(table.Rows.ToDictionary(row => row[0], row => row[1])); // two lambda functions passed in to tell the converter how to take keys and values
    }

    [When("the '{}' request is sent to '{}' endpoint")]
    public void TheRequestIsSentToEndpoint(Method method, Endpoint endpoint)
    {
        testContext.Request.Method = method;
        testContext.Request.Resource = EndpointProvider.GetUrl(endpoint);
        testContext.Response = testContext.Client.ExecuteAsync(testContext.Request).Result;
    }

    [When("the response ID from the response is remembered")]
    public void TheBoardIdFromTheResponseIsRemembered()
    {
        var responseContent = JToken.Parse(testContext.Response.Content ?? string.Empty);
        testContext.BoardId = responseContent.SelectToken("id")?.ToString() ?? throw new InvalidOperationException("Token 'id' not found in the response.");
    }
}
