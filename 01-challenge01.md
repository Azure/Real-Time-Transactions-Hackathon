# Challenge 1: The Landing Before the Launch

Woodgrove Bank wants to ride the wave of conversational AI to allow customers to interact with their bank accounts using natural language. They want to build a chatbot that will allow customers to ask questions about their accounts and transactions. They also want to build a secure architecture that provides high availability and scalability to support their global customer base.

However, before they can launch the POC, they need to deploy the Azure services needed to support the solution.

## Challenge

Your team must:

1. Deploy the Azure services needed to support the chat interface
2. Set up your development environment

### Hints

- Woodgrove Bank has provided you a script to deploy the foundation of your Azure environment. See the instructions in the README.md of the repo.
- You will need to deploy the following Azure services:
  - Azure Cosmos DB NoSQL API (multi-master in 3 regions)
  - Azure OpenAI
  - Azure Function App Consumption Plan (deployed in 3 regions)
  - Azure Front Door that evenly load balances traffic across the 3 regions to the Azure Function Apps
  - Azure Blob Storage account for hosting a static web app

### Success Criteria

To complete this challenge successfully, you must:

- Clone the repo with the starter artifacts and deployment scripts
- Deploy the Azure services needed to support the payments app interface
- Deploy Azure OpenAI with the following deployments:
  - `completions-003` with the `text-davinci-003` model
- Deploy an Azure Cosmos DB account with the following configurations:
  - API: NoSQL
  - Consistency: Bounded Staleness
  - Geo-Redundancy: Enabled
    - Deployed in the same 3 regions as the Azure Function Apps from the deployment script
  - Multi-region writes: Enabled
  - Analytical store: Disabled
  - Autoscale: Enabled
  - Provision throughput: 1000 RU/s
  - Create a database named `payments`
  - Create new containers named:
    - `customerTransactions` with partition key `/accountId`
    - `globalIndex` with partition key `/partitionKey`
    - `members` with partition key `/memberId`
    - `transactions` with partition key `/accountId`
- Validate that the services are deployed and running

### Resources

- [Azure Cosmos DB](https://learn.microsoft.com/azure/cosmos-db/)
- [Host a static website on Blob Storage](https://learn.microsoft.com/azure/storage/blobs/storage-blob-static-website-host)
- [Azure OpenAI](https://learn.microsoft.com/azure/cognitive-services/openai/overview)
- [Azure Functions](https://learn.microsoft.com/azure/azure-functions/functions-overview)
