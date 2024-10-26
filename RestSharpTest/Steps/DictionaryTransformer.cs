using RestSharpTest.Consts;
using TechTalk.SpecFlow;

namespace RestSharpTest.Steps;

[Binding]
public class DictionaryTransformer
{
    [StepArgumentTransformation]
    public IDictionary<string, string> Dictionary(Table table)
    {
        var transformedDictionary = table.Rows.ToDictionary(row => row[0], row => row[1]);

        foreach (var entry in transformedDictionary)
        {
            transformedDictionary[entry.Key] = ConvertValue(entry.Value);
        }

        return transformedDictionary;
    }

    private string ConvertValue(string value)
    {
        return value switch // switch syntax
        {
            "current_user_key" => UrlParamValues.ValidKey,
            "empty_value" => string.Empty,
            "current_user_token" => UrlParamValues.ValidToken,
            "another_user_key" => UrlParamValues.AnotherUserKey,
            "another_user_token" => UrlParamValues.AnotherUserToken,
            _ => value
        };
    }
}
