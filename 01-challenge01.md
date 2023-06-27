# Challenge 1: The Landing Before the Launch

Woodgrove Bank wants to ride the wave of conversational AI to allow customers to interact with their bank accounts using natural language. They want to build a chatbot that will allow customers to ask questions about their accounts and transactions. They also want to build a secure architecture that provides high availability and scalability to support their global customer base.

However, before they can launch the POC, they need to deploy the Azure services needed to support the solution.

## Challenge

Your team must:

1. Deploy the Azure services needed to support the chat interface
2. Set up your development environment

### Hints

- You will need to deploy the following Azure services:
  - Azure Cosmos DB NoSQL API (multi-master in 3 regions)
  - Azure OpenAI
  - Azure Function App Consumption Plan (deployed in 3 regions)
  - Azure Front Door that evenly load balances traffic across the 3 regions to the Azure Function Apps
  - Azure Blob Storage account for hosting a static web app

### Success Criteria

To complete this challenge successfully, you must:

- Deploy the Azure services needed to support the payments app interface
- Validate that the services are deployed and running

### Resources

- [Azure Cosmos DB](https://learn.microsoft.com/azure/cosmos-db/)
- [Host a static website on Blob Storage](https://learn.microsoft.com/azure/storage/blobs/storage-blob-static-website-host)
- [Azure OpenAI](https://learn.microsoft.com/azure/cognitive-services/openai/overview)
- [Azure Functions](https://learn.microsoft.com/azure/azure-functions/functions-overview)
