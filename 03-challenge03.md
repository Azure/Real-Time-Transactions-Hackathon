# Challenge 3: Visualize World Peace

_...or at least the members, accounts, and transaction data._

Now that the data is loaded, it's time to visualize and interact with it. In this challenge you and your team wire up the Members, Account Summary, and Transaction Statement functionality to load the data from Cosmos DB and present it in the UI. This gives you the chance to become familiar with core of the application by seeing the end-to-end in action.

Since the Azure Cosmos DB account supports several regions, and the web traffic is routed to random regions by Azure Front Door, you can experience replication of data in Cosmos DB across the regions. This means that you can write to any region and the data will be replicated to all other regions. This is a great feature for global applications that need to be highly available and scalable. However, one challenge that can arise is that there may be conflicts when updating the same document in different regions at the same time or within a brief period of time. The Azure Cosmos DB SDK provides a `PatchItem`/`PatchItemAsync` feature that allows you to update a portion of a document without having to retrieve it first. This feature also automatically manages merging patch operations that occur at the same time in different write regions. You will explore this capability within this challenge.

## Challenge

Your team must:

1. Update the Function App code to add a rule when batch processing a transaction to throw an exception if the transaction amount is greater than the account balance plus the overdraft limit.
2. Connect the application to the REST API supplied by the Azure Function App.
3. Within the application, explore the members list and details.
4. Create a new member.
5. Explore the accounts list.
6. For a given account, view the account details.
7. For a given account, view the transactions.
8. For a given account, create credit and debit transactions.
   1. Observe that the account document's `balance` property is automatically updated through a patch increment operation.
9. Create a debit transaction that is greater than the account balance plus the overdraft limit.
10. Start the `CorePayments.EventMonitor` console application to monitor the member patch operations, then update a member several times in a row to review the monitor output.

### Hints

- Search through the solution for the `TODO: Challenge 3` comments and follow the instructions provided.
- Since the app uses role-based access control (RBAC), if you want to run the Function App locally, you have to assign yourself to the "Cosmos DB Built-in Data Contributor" role via the Azure Cloud Shell or Azure CLI with the following:

    ```bash
    az cosmosdb sql role assignment create --account-name YOUR_COSMOS_DB_ACCOUNT_NAME --resource-group YOUR_RESOURCE_GROUP_NAME --scope "/" --principal-id YOUR_AZURE_AD_PRINCIPAL_ID --role-definition-id 00000000-0000-0000-0000-000000000002
    ```

- Event Hubs is also using RBAC. The Member Repository sends an Event Hubs event when patching members. The `CorePayments.EventMonitor` monitor listens for Event Hub events and displays the output. For the events to work, you need to add yourself to the "Azure Event Hubs Data Owner" role via the Azure Cloud Shell or Azure CLI with the following:

    ```bash
    az role assignment create --assignee "YOUR_EMAIL_ADDRESS" --role "Azure Event Hubs Data Owner" --scope "/subscriptions/YOUR_AZURE_SUBSCRIPTION_ID/resourceGroups/YOUR_RESOURCE_GROUP_NAME/providers/Microsoft.EventHub/namespaces/YOUR_EVENT_HUBS_NAMESPACE"
    ```

    > Make sure you're signed in to Azure from the Visual Studio or VS Code terminal before running the Function App locally. You need to run `az login` and `az account set --subscription YOUR_AZURE_SUBSCRIPTION_ID` first.

- You can find an `env.template` file inside the `ui` folder for the React web application. Copy this file and name the copy `.env.local`. While running the web app and Function App locally, you need to update the `NEXT_PUBLIC_API_URL` value to `http://localhost:7071/api`. In a later challenge when you deploy the web app and Function App, you need to update this value to the deployed Function App URL.

### Success Criteria

To complete this challenge successfully, you must:

- Complete all of the code in the solution that is marked with `TODO: Challenge 3` comments.
- Add a rule to the Web API code to throw an exception if the transaction amount is greater than the account balance plus the overdraft limit. The response should be a 400 Bad Request with a message, such as "Insufficient balance to process transaction."
- Configure the application to connect to the REST API supplied by the Web API.
- Explore the data within the application, as described above.
- Create a new member, edit a member, and create credit and debit transactions.
- Verify that the application throws an exception when you create a debit transaction that is greater than the account balance plus the overdraft limit.
- Start the `CorePayments.EventMonitor` console application, then perform the following tasks:
  - Update one or more fields using the edit member form and observe the output in the console application.
  - Make several quick updates to various fields while monitoring the application. Observe the region where the write operation occurred and make edits until you see writes take place in different regions. Spot-check the data in the Azure Cosmos DB Data Explorer to verify that the data was merged correctly.

### Resources

- [Azure Cosmos DB partial document update (patch operation)](https://learn.microsoft.com/azure/cosmos-db/partial-document-update)
