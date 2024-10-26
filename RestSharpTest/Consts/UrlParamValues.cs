using RestSharp;

namespace RestSharpTest.Consts;

internal class UrlParamValues
{
    public const string ValidKey = "e9ec7223857773c53964cc7286ee6da5";
    public const string ValidToken = "ATTAbdb870c70e23e66ac72f51077bd8512b477cc9a72a8043456b4b86f3d64c2897D9A2F1F3";
    public const string AnotherUserKey = "e9ec7223857773c53964cc7286ee6da5";
    public const string AnotherUserToken = "ATTAbdb870c70e23e66ac72f51077bd8512b477cc9a72a8043456b4b86f3d64c2897D9A2F1F0";

    public static readonly IEnumerable<Parameter> AuthQueryParams =
    [
        Parameter.CreateParameter("key", UrlParamValues.ValidKey, ParameterType.QueryString),
        Parameter.CreateParameter("token", UrlParamValues.ValidToken, ParameterType.QueryString)
    ];
    
}
