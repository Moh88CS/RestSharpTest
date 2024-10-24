using RestSharp;

namespace RestSharpTest.Consts;

internal class UrlParamValues
{
    public const string ExistingBoardId = "670bfaf62656fd3e8f5e7549";
    public const string ExistingListId = "670bfb7956de20d533863709";
    public const string ExistingCardId = "670c4ddf8eeaca6bf176696e";
    public const string BoardIdToUpdate = "670c37e377a68c3d1131d0d9";
    public const string CardIdToUpdate = "670c3af3b378307bd2db94b4";
    public const string UserName = "mohamedabdulal";
    public const string ValidKey = "e9ec7223857773c53964cc7286ee6da5";
    public const string AnotherUserKey = "e9ec7223857773c53964cc7286ee6da5";
    public const string ValidToken = "ATTAbdb870c70e23e66ac72f51077bd8512b477cc9a72a8043456b4b86f3d64c2897D9A2F1F3";
    public const string AnotherUserToken = "ATTAbdb870c70e23e66ac72f51077bd8512b477cc9a72a8043456b4b86f3d64c2897D9A2F1F0";

    public static readonly IEnumerable<Parameter> AuthQueryParams =
    [
        Parameter.CreateParameter("key", UrlParamValues.ValidKey, ParameterType.QueryString),
        Parameter.CreateParameter("token", UrlParamValues.ValidToken, ParameterType.QueryString)
    ];
    public static readonly IEnumerable<Parameter> AnotherUserAuthQueryParams =
    [
        Parameter.CreateParameter("key", UrlParamValues.AnotherUserKey, ParameterType.QueryString),
        Parameter.CreateParameter("token", UrlParamValues.AnotherUserToken, ParameterType.QueryString)
    ];
}
