# RestSharp API Testing with SpecFlow (BDD)

This branch (`ToSpecFlow`) transforms the existing **RestSharp API testing framework** into a **Behavior-Driven Development (BDD)** framework using **SpecFlow**. SpecFlow allows tests to be written in a human-readable Gherkin syntax, making them accessible to both technical and non-technical stakeholders. The framework is designed to automate RESTful API testing with a focus on clarity, maintainability, and scalability.

## Key Features

- **SpecFlow BDD**: Uses SpecFlow to write tests in Gherkin syntax, making them readable and understandable by non-technical stakeholders.
- **RestSharp Integration**: Leverages RestSharp for robust API request creation, response validation, and error handling.
- **Modular Design**: The framework is structured in a modular way, making it easy to extend and maintain.
- **Data-Driven Testing**: Supports data-driven testing using external data sources like JSON, CSV, or Excel files.
- **Reporting**: Generates detailed and interactive test execution reports using SpecFlow's reporting tools.
- **Environment Configuration**: Easily switch between different environments (e.g., dev, staging, production) using configuration files.
- **Logging**: Integrated logging for better debugging and traceability.
- **CI/CD Integration**: Ready to be integrated into CI/CD pipelines for automated testing.

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
   git checkout ToSpecFlow
   dotnet restore
   dotnet test
