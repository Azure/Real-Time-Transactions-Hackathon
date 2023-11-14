# Challenge 3: Visualize World (Global) Peace

_...or at least the members, accounts, and transaction data._

Now that the data is loaded, it's time to visualize and interact with it. In this challenge you and your team wire up the Members, Account Summary, and Transaction Statement functionality to load the data from Cosmos DB and present it in the UI. This gives you the chance to become familiar with core of the application by seeing the end-to-end in action.

Another component to this challenge is completing the Global Index repository. As you know, we're using the NoSQL API. That means that we do not have an enforced schema within the database or containers, and we're also not dealing with a traditional relational database system. The global index container within the Azure Cosmos DB deployment is used to map different entity relationships based on the partition key, ID, and target doc type. This gives us the ability to have flexible lookups and pseudo joins in NoSQL for one-to-few and one-to-one relationships.

The global index gives us a member-to-account mapping based on their IDs and partition keys. When populated, we can search the global index if we want to be able to find all the accounts for a member within the containers for where the member document lives and the account documents live. Without a global index, we would need to execute several cross-partition queries to locate all of the related documents. The global index gives us a quick way to look up that relationship and get the IDs for the item that we're looking for.

## Challenge

Your team must:

1. Update the Web API code to add a rule when batch processing a transaction to throw an exception if the transaction amount is greater than the account balance plus the overdraft limit.
2. Update the appropriate repository to complete the global index functionality.
3. Connect the application to the REST API supplied by the Web API.
4. Within the application, explore the members list and details.
5. Create a new member.
   1. Add the new member to an existing account .
   2. Remove the account from the member.
6. Explore the accounts list.
7. For a given account, view the account details.
8. For a given account, view the transactions.
9. For a given account, create credit and debit transactions.
   1. Observe that the account document's `balance` property is automatically updated through a patch increment operation.
10. Create a debit transaction that is greater than the account balance plus the overdraft limit.

### Hints

- Search through the solution for the `TODO: Challenge 3` comments and follow the instructions provided.
- Since the app uses role-based access control (RBAC), if you want to run the Web API locally, you have to assign yourself to the "Cosmos DB Built-in Data Contributor" role via the Azure Cloud Shell or Azure CLI with the following:

    ```bash
    az cosmosdb sql role assignment create --account-name YOUR_COSMOS_DB_ACCOUNT_NAME --resource-group YOUR_RESOURCE_GROUP_NAME --scope "/" --principal-id YOUR_AZURE_AD_PRINCIPAL_ID --role-definition-id 00000000-0000-0000-0000-000000000002
    ```

    > Make sure you're signed in to Azure from the Visual Studio or VS Code terminal before running the Web API locally. You need to run `az login` and `az account set --subscription YOUR_AZURE_SUBSCRIPTION_ID` first.

- You can find an `env.template` file inside the `ui` folder for the React web application. Copy this file and name the copy `.env.local`. While running the web app and Web API locally, you need to update the `NEXT_PUBLIC_API_URL` value to `http://localhost:7071/api`. In a later challenge when you deploy the web app and Web API, you need to update this value to the deployed web API URL.

### Success Criteria

To complete this challenge successfully, you must:

- Complete all of the code in the solution that is marked with `TODO: Challenge 3` comments.
- Add a rule to the Web API code to throw an exception if the transaction amount is greater than the account balance plus the overdraft limit. The response should be a 400 Bad Request with a message, such as "Insufficient balance to process transaction."
- Configure the application to connect to the REST API supplied by the Web API.
- Explore the data within the application, as described above.
- Create a new member, edit a member, add and remove an account for the member, and create credit and debit transactions.
- Verify that the application throws an exception when you create a debit transaction that is greater than the account balance plus the overdraft limit.
- Observe the global index data and execute queries against it in the Azure Cosmos DB Data Explorer to verify that the data is populated correctly.

### Resources

- [Azure Cosmos DB partial document update (patch operation)](https://learn.microsoft.com/azure/cosmos-db/partial-document-update)
