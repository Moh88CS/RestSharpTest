using RestSharpTest.Consts;
using TechTalk.SpecFlow;

namespace RestSharpTest.Steps;

[Binding]
public class StepArgumentsTransformer
{
    [StepArgumentTransformation("(with|without)")] // now method will only be called when we have argument that has with or without
    public bool With(string with)
    {
        return with.Equals("with");
    }

    [StepArgumentTransformation("(GetAllBoards|GetABoard|CreateABoard|DeleteABoard|UpdateABoard)")]
    public Endpoint Endpoint(string endpoint)
    {
        return (Endpoint) Enum.Parse(typeof(Endpoint), endpoint); // parsing the enum look into
    }
}
