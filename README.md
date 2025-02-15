# C# RestSharp API Testing Framework

This project is a robust API testing framework built using **C#**, **RestSharp**, and **NUnit**. It is designed to automate the testing of RESTful APIs, ensuring reliability, scalability, and maintainability. The framework leverages industry-standard tools and practices to provide a comprehensive solution for API testing. Additionally, the `ToSpecFlow` branch transitions this framework into a **Behavior-Driven Development (BDD)** style using **SpecFlow**, making it more collaborative and human-readable.

## Features

- **RestSharp Integration**: Uses RestSharp, a powerful .NET library for REST API testing, to simplify request creation, response validation, and error handling.
- **NUnit Testing**: Utilizes NUnit as the test runner for robust and scalable test execution.
- **Modular Design**: The framework is structured in a modular way, making it easy to extend and maintain.
- **Data-Driven Testing**: Supports data-driven testing using external data sources like JSON, CSV, or Excel files.
- **Reporting**: Generates detailed test execution reports for better visibility into test results.
- **Environment Configuration**: Easily switch between different environments (e.g., dev, staging, production) using configuration files.
- **Logging**: Integrated logging for better debugging and traceability.
- **CI/CD Integration**: Ready to be integrated into CI/CD pipelines for automated testing.
- **SpecFlow BDD (ToSpecFlow Branch)**: Transitions the framework into a BDD-style framework using SpecFlow, enabling collaboration between technical and non-technical stakeholders.

## Prerequisites

Before you begin, ensure you have the following installed:

- **.NET SDK**: Version 5.0 or higher.
- **IDE**: Visual Studio or JetBrains Rider.
- **Git**: To clone the repository.

## Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/Moh88CS/RestSharpTest.git
   cd RestSharpTest
   dotnet restore
   dotnet test

2. **If you get some errors with NuGet packages, try checking your nuget sources**
   ```bash
   dotnet nuget list source
   dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org
   dotnet restore

## Reporting
NUnit generates detailed test execution reports, which can be viewed in your IDE or CI/CD pipeline. For SpecFlow, you can use plugins like SpecFlow+ LivingDoc or Allure to generate interactive and detailed reports.
