using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Net;
using TechTalk.SpecFlow;

namespace RestSharpTest.Steps;

[Binding]
public class TrelloApiAssertSteps
{

    private readonly TestContext testContext;

    public TrelloApiAssertSteps(TestContext testContext)
    {
        this.testContext = testContext;
    }

    [Then("the response status code is {}")]
    public void TheResponseStatusCodeIs(HttpStatusCode expectedStatusCode)
    {
        Assert.That(testContext.Response.StatusCode, Is.EqualTo(expectedStatusCode));
    }

    [Then("the response matches '{}' schema")]
    public void TheResponseMatchesSchema(string schemaName)
    {
        var responseContent = JToken.Parse(testContext.Response.Content ?? string.Empty);
        var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/" + schemaName));
        Assert.That(responseContent.IsValid(jsonSchema), Is.True);
    }

    [Then("body value has the following values by paths:")]
    public void BodyValueByPathIsEqualTo(Table table)
    {
        var responseContent = JToken.Parse(testContext.Response.Content ?? string.Empty);
        foreach (var row in table.Rows)
        {
            string rowExpectedValue = row["expected_value"];
            string expectedValue = rowExpectedValue.Equals("empty_string") ? string.Empty : rowExpectedValue;
            var nameToken = responseContent.SelectToken(row["path"]) ?? throw new InvalidOperationException("Token 'name' not found in the response.");
            Assert.That(nameToken.ToString(), Is.EqualTo(expectedValue));
        }
    }

    [Then("the response body is equal to {string}")]
    public void TheResponseBodyIsEqualTo(string expectedValue)
    {
        Assert.That(testContext.Response.Content, Is.EqualTo(expectedValue));
    }

    [Then("the response body does not have any item by paths:")]
    public void TheResponseBodyDoesNotHaveAnyItemsByPaths(Table table)
    {
        foreach (var row in table.Rows)
        {
            string rowValue = row["expected_value"];
            string expectedValue = rowValue.Equals("created_board_id") ? testContext.BoardId : rowValue;
            var responseContent = JToken.Parse(testContext.Response.Content?.ToString() ?? string.Empty);
            bool childToken = responseContent.Children().Select(token => token.SelectToken(row["path"])).Contains(expectedValue);
            Assert.That(childToken, Is.False);
        }
    }

    [Then("the response body has any item by paths:")]
    public void TheResponseBodyHasAnyItemByPaths(Table table)
    {
        var responseContent = JToken.Parse(testContext.Response.Content?.ToString() ?? string.Empty);
        foreach (var row in table.Rows)
        {
            var rowValue = row["value"];
            var expectedValue = rowValue.Equals("created_board_id") ? testContext.BoardId : rowValue;
            bool childToken = responseContent.Children().Select(token => token.SelectToken(row["path"])).Contains(expectedValue);
            Assert.That(childToken, Is.True);
        }
    }
}
