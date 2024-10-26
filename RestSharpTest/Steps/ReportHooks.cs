using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using TechTalk.SpecFlow;

namespace RestSharpTest.Steps;

[Binding]
public class ReportHooks
{
    private static ExtentReports ExtentReport;
    private static ExtentTest Feature {  get; set; }
    private ExtentTest Scenario { get; set; }
    private ExtentTest Step { get; set; }

    [BeforeTestRun]
    public static void GenerateExtentReport()
    {
        ExtentReport = new ExtentReports();
        ExtentReport.AttachReporter(new ExtentSparkReporter("Report.html")); // listener for the tests with name Report.html

    }

    [BeforeFeature]
    public static void AttachFeature(FeatureContext featureContext) // stores feature status and title (context)
    {
        Feature = ExtentReport.CreateTest<Feature>(featureContext.FeatureInfo.Title);
    }

    [BeforeScenario]
    public void AttachScenario(ScenarioContext scenarioContext)
    {
        Scenario = Feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
    }

    [BeforeStep]
    public void AttachStep(ScenarioContext scenarioContext)
    {
        Step = Scenario.CreateNode(new GherkinKeyword(scenarioContext.CurrentScenarioBlock.ToString()), scenarioContext.StepContext.StepInfo.Text); // first step is type (given, when) second step is the title
    }

    [AfterStep]
    public void AddStepResult(ScenarioContext scenarioContext)
    {
        if (scenarioContext.TestError != null)
        {
            Step.Fail(scenarioContext.TestError);
        }
    }

    [AfterTestRun]
    public static void CloseExtentReport()
    {
        ExtentReport.Flush(); // gather all the results by the listener and put in the html file
    }
}
