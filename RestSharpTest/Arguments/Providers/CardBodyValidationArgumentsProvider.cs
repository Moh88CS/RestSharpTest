using RestSharpTest.Arguments.Holders;
using RestSharpTest.Consts;
using System.Collections;

namespace RestSharpTest.Arguments.Providers;

internal class CardBodyValidationArgumentsProvider : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new CardBodyValidationArgumentsHolder
            {
                BodyParams = new Dictionary<string, object>
                {
                    {"name", 12345 },
                    {"idList", UrlParamValues.ExistingListId }
                },
                ErrorMessage = "invalid value for name"
            }
        };
        yield return new object[]
        {
            new CardBodyValidationArgumentsHolder
            {
                BodyParams = new Dictionary<string, object>
                {
                    {"name", "New Card" }
                },
                ErrorMessage = "invalid value for idList"
            }
        };
        yield return new object[]
        {
            new CardBodyValidationArgumentsHolder
            {
                BodyParams = new Dictionary<string, object>
                {
                    {"name", "New Card" },
                    {"idList", "invalid" }
                },
                ErrorMessage = "invalid value for idList"
            }
        };
    }
}
