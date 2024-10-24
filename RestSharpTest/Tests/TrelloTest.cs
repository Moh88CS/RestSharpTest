using RestSharp;
using System.Net;

namespace RestSharpTest.Tests;

public class TrelloTest : BaseTest
{
    [Test]
    public void CheckTrelloApi()
    {
        var request = new RestRequest();
        //var response = new RestResponse();

        Console.WriteLine($"{request.Method}");

        var response = _client.Get(request);

        Console.WriteLine(response.Content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
