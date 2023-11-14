# Azure Cosmos DB & Azure OpenAI Service Reference Architecture: Payments & Accounts Hackathon

Woodgrove Bank is a global bank that has been in business for over 100 years. They have a large customer base and are looking to expand their business by offering new services to their customers. Members have accounts, each account with corresponding balances, overdraft limits and credit/debit transactions. They are looking to build a new application that will allow their customers to manage their accounts and better understand their bank transactions that contribute to their overall balance. They have decided to use Azure Cosmos DB and Azure OpenAI Service to build these new capabilities.

Woodgrove Bank wants to ride the wave of conversational AI to allow customers to interact with their bank accounts using natural language. They want to build a chatbot that will allow customers to ask questions about their accounts and transactions. They also want to build a secure architecture that provides high availability and scalability to support their global customer base. They are interested in how they can use high consistency across multiple regions, allowing customers to read and write to database endpoints in their local region, and make sure that the data is consistent across all regions. A pattern they want to explore is to separate read and write operations since the transaction data has a high rate of volume. They do not want to impact the performance of their database operations by introducing heavy reads against the same resource handling incoming write operations. They also want to make sure that if a region goes down, their customers within the impacted region can still access their accounts and make transactions.

In this hackathon, you will build a POC that does the following:

- Replicate transaction data across multiple geographic regions for both reads and writes, while maintaining consistency. Updates are made efficiently with the patch operation.
- Apply business rules govern if a transaction is allowed.
- Create an AI powered co-pilot enables agents to analyze transactions using natural language.

## Prerequisites

- Azure Subscription
- Subscription access to Azure OpenAI service. Start here to [Request Access to Azure OpenAI Service](https://customervoice.microsoft.com/Pages/ResponsePage.aspx?id=v4j5cvGGr0GRqy180BHbR7en2Ais5pxKtso_Pz4b1_xUOFA5Qk1UWDRBMjg0WFhPMkIzTzhKQ1dWNyQlQCN0PWcu)

- Backend (Web API, Worker Service, Console Apps, etc.)
  - Visual Studio 2022 17.6 or later (required for passthrough Visual Studio authentication for the Docker container)
  - .NET 7 SDK
  - Docker Desktop (with WSL for Windows machines)
  - Azure CLI ([v2.49.0 or greater](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli))
  - [Helm 3.11.1 or greater](https://helm.sh/docs/intro/install/)
- Frontend (React web app)
  - Visual Studio Code
  - Ensure you have the latest version of NPM and node.js:
    - Install NVM from https://github.com/coreybutler/nvm-windows
    - Run nvm install latest
    - Run nvm list (to see the versions of NPM/node.js available)
    - Run nvm use latest (to use the latest available version)

To start the React web app:

1. Navigate to the `ui` folder
2. Run npm install to restore the packages
3. Run npm run dev
4. Open localhost:3000 in a web browser

## Run the solution locally using Visual Studio

You can run the website and the REST API provided by the Web API that supports it locally. You need to first update your local configuration, and then you can run the solution in the debugger using Visual Studio.

#### Configure local settings

> **Note:** Only complete these steps if you did not deploy the solution to Azure using the deployment guide. The deployment scripts create the `appsettings.Development.json` files for you. If these files do not exist for some reason, you can follow these steps to create them.

- In the `CorePayments.WebAPI` project, copy the `appsettings.Development.template.json` file and name it `appsettings.Development.json`. This file should like similar to this (make sure you replace the `{{---}}` and other placeholders with your deployed resource names):

    ```json
    {
        "Logging": {
          "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
          }
        },
        "CosmosDBConnection:accountEndpoint": "{{cosmosEndpoint}}",
        "DatabaseSettings": {
          "CustomerContainer": "customerTransactions",
          "GlobalIndexContainer": "globalIndex",
          "IsMasterRegion": "True",
          "MemberContainer": "members",
          "PaymentsDatabase": "payments",
          "PreferredRegions": "East US",
          "TransactionsContainer": "transactions"
        },
        "AllowedHosts": "*",
        "AnalyticsEngine": {
          "OpenAIEndpoint": "{{openAiEndpoint}}",
          "OpenAIKey": "{{openAiKey}}",
          "OpenAICompletionsDeployment": "completions"
        }
    }
    ```

- In the `CoreClaims.WorkerService` project, copy the `appsettings.Development.template.json` file and name it `appsettings.Development.json`. This file should like similar to this (make sure you replace the `{{---}}` and other placeholders with your deployed resource names):

    ```json
    {
      "CosmosDBConnection:accountEndpoint": "{{cosmosEndpoint}}"
    }
    ```

- In the `account-generator` project, copy `local.settings.template.json` to a new file named `local.settings.json` and make sure it looks similar to this:

    ```json
    {
      "CosmosDbConnectionString": "{{CosmosDbConnectionString}}",
      "GeneratorOptions": {
        "RunMode": "OneTime",
        "BatchSize": 200,
        "Verbose": true,
        "SleepTime": 1000
      }
    }
    ```

### Running in debug

To run locally and debug using Visual Studio, open the solution file to load the projects and prepare for debugging.

Before you can start debugging the `CorePayments.WebAPI` and `CorePayments.WorkerService` projects, make sure the newly created `appsettings.Development.json` files are copied to the output directory in each project. To do this, right-click on the file in the Solution Explorer and select `Properties`. In the properties window, set the `Copy to Output Directory` property to `Copy always`..

You are now ready to start debugging the solution locally. To do this, you first need to set up multiple startup projects to run when you debug. Right-click the solution in Solution Explorer, then select **Configure Startup Projects...**. Set `CorePayments.WorkerService` and `CorePayments.WebAPI` to **Start** under Action. All others should be set to None. When you're ready to debug, press `F5` or select `Debug > Start Debugging` from the menu.

**NOTE**: With Visual Studio, you can also use alternate ways to manage the secrets and configuration. For example, you can use the `Manage User Secrets` option from the context menu of the `CorePayments.WebAPI` and `CorePayments.WorkerService` projects to open the `secrets.json` file and add the configuration values there.

## Teardown

When you have finished with the hackathon, simply delete the resource group that was created.
