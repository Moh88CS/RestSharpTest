namespace RestSharpTest.Consts;

public enum Endpoint
{
    GetAllBoards,
    GetABoard,
    CreateABoard,
    DeleteABoard,
    UpdateABoard
}

public static class EndpointProvider
{
    private static readonly IDictionary<Endpoint, string> endpoints = new Dictionary<Endpoint, string>()
    {
        {Endpoint.GetABoard, "/1/boards/{id}" },
        {Endpoint.GetAllBoards, "/1/members/{member}/boards" },
        {Endpoint.CreateABoard, "/1/boards" },
        {Endpoint.UpdateABoard, "/1/boards/{id}" },
        {Endpoint.DeleteABoard, "/1/boards/{id}" }
    };

    public static string GetUrl(Endpoint endpoint) { return endpoints[endpoint]; }
}
