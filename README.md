# Azure Cosmos DB & OpenAI Reference Architecture: Payments & Accounts Hackathon

Woodgrove Bank is a global bank that has been in business for over 100 years. They have a large customer base and are looking to expand their business by offering new services to their customers. Members have accounts, each account with corresponding balances, overdraft limits and credit/debit transactions. They are looking to build a new application that will allow their customers to manage their accounts and better understand their bank transactions that contribute to their overall balance. They have decided to use Azure Cosmos DB and Azure OpenAI to build these new capabilities.

Woodgrove Bank wants to ride the wave of conversational AI to allow customers to interact with their bank accounts using natural language. They want to build a chatbot that will allow customers to ask questions about their accounts and transactions. They also want to build a secure architecture that provides high availability and scalability to support their global customer base. They are interested in how they can use high consistency across multiple regions, allowing customers to read and write to database endpoints in their local region, and make sure that the data is consistent across all regions. A pattern they want to explore is to separate read and write operations since the transaction data has a high rate of volume. They do not want to impact the performance of their database operations by introducing heavy reads against the same resource handling incoming write operations. They also want to make sure that if a region goes down, their customers within the impacted region can still access their accounts and make transactions.

In this hackathon, you will build a POC that does the following:

- Replicate transaction data across multiple geographic regions for both reads and writes, while maintaining consistency. Updates are made efficiently with the patch operation.
- Apply business rules govern if a transaction is allowed.
- Create an AI powered co-pilot enables agents to analyze transactions using natural language.

## Prerequisites

- Azure Subscription
- Subscription with access to the Azure OpenAI service. Start here to [Request Access to Azure OpenAI Service](https://customervoice.microsoft.com/Pages/ResponsePage.aspx?id=v4j5cvGGr0GRqy180BHbR7en2Ais5pxKtso_Pz4b1_xUOFA5Qk1UWDRBMjg0WFhPMkIzTzhKQ1dWNyQlQCN0PWcu)

### Prerequisites for running/debugging locally

- Backend (Function App, Console Apps, etc.)
  - Visual Studio Code or Visual Studio 2022
  - .NET 7 SDK
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

### Clone this repo

Clone this repository:

```pwsh
git clone https://github.com/...
```

### Deploy to Azure the core services

1. Open the PowerShell command line and navigate to the directory where you cloned the repo.
2. Navigate into the `starter-artifacts\deploy\powershell` folder.
3. Run the following PowerShell script to provision the infrastructure and deploy the base set of required Azure services. Provide the name of a NEW resource group that will be created. This will provision the resource group, blob storage accounts, Event Hub, and a Synapse Workspace.

```pwsh
./Starter-Deploy.ps1  -resourceGroup <resource-group-name> -subscription <subscription-id>
```

## Run the solution locally using Visual Studio

You can run the website and the REST API provided by the Azure Function App that supports it locally. You need to first update your local configuration and then you can run the solution in the debugger using Visual Studio.

#### Configure local settings

- In the `CorePayments.FunctionApp` project, copy the `local.settings.template.json` file and name it `local.settings.json`. This file should like similar to this (make sure you replace the `{{---}}` and other placeholders with your deployed resource names):

    ```json
    {
        "IsEncrypted": false,
        "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "CosmosDBConnection__accountEndpoint": "{{cosmosEndpoint}}",
        "EventHubConnection__fullyQualifiedNamespace": "{{eventHubEndpoint}}",
        "customerContainer": "customerTransactions",
        "globalIndexContainer": "globalIndex",
        "isMasterRegion": "True",
        "memberContainer": "members",
        "paymentsDatabase": "payments",
        "preferredRegions": "East US",
        "transactionsContainer": "transactions"
        },
        "Host": {
        "LocalHttpPort": 7071,
        "CORS": "*"
        },
        "AnalyticsEngine": {
        "OpenAIEndpoint": "{{openAiEndpoint}}",
        "OpenAIKey": "{{openAiKey}}",
        "OpenAICompletionsDeployment": "{{openAiDeployment}}"
        }
    }
    ```

- In the `CorePayments.EventMonitor` project, copy `local.settings.template.json` to a new file named `local.settings.json` and make sure it looks similar to this:

    ```json
    {
      "EventHubConnection__fullyQualifiedNamespace": "{{eventHubEndpoint}}"
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

Before you can start debugging the Function App, make sure the newly created `local.settings.json` file is copied to the output directory. To do this, right-click on the file in the Solution Explorer and select `Properties`. In the properties window, set the `Copy to Output Directory` property to `Copy always`..

You are now ready to start debugging the solution locally. To do this, press `F5` or select `Debug > Start Debugging` from the menu.

**NOTE**: With Visual Studio, you can also use alternate ways to manage the secrets and configuration. For example, you can use the `Manage User Secrets` option from the context menu of the `CorePayments.FunctionApp` project to open the `secrets.json` file and add the configuration values there.

## Teardown

When you have finished with the hackathon, simply delete the resource group that was created.
