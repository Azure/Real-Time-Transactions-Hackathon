# Challenge 3: Visualize World Peace

_...or at least the members, accounts, and transaction data._

Now that the data is loaded, it's time to visualize and interact with it. In this challenge you and your team wire up the Members, Account Summary, and Transaction Statement functionality to load the data from Cosmos DB and present it in the UI. This gives you the chance to become familiar with core of the application by seeing the end-to-end in action.

Since the Azure Cosmos DB account is multi-master across several regions, and the web traffic is routed to random regions by Azure Front Door, you can experience a feature of Cosmos DB called _multi-master_ replication. This means that you can write to any region and the data will be replicated to all other regions. This is a great feature for global applications that need to be highly available and scalable. However, one challenge that can arise is that there may be conflicts when updating the same document in different regions at the same time or within a brief period of time. The Azure Cosmos DB SDK provides a `PatchItem`/`PatchItemAsync` feature that allows you to update a portion of a document without having to retrieve it first. This feature also automatically manages merging patch operations that occur at the same time in different write regions. You will explore this capability within this challenge.

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

### Success Criteria

To complete this challenge successfully, you must:

- Add a rule to the Function App code to throw an exception if the transaction amount is greater than the account balance plus the overdraft limit. The response should be a 400 Bad Request with a message, such as "Insufficient balance to process transaction."
- Configure the application to connect to the REST API supplied by the Azure Function App.
- Explore the data within the application, as described above.
- Create a new member, edit a member, and create credit and debit transactions.
- Verify that the application throws an exception when you create a debit transaction that is greater than the account balance plus the overdraft limit.
- Start the `CorePayments.EventMonitor` console application, then perform the following tasks:
  - Update one or more fields using the edit member form and observe the output in the console application.
  - Make several quick updates to various fields while monitoring the application. Observe the region where the write operation occurred and make edits until you see writes take place in different regions. Spot-check the data in the Azure Cosmos DB Data Explorer to verify that the data was merged correctly.

### Resources

- [Azure Cosmos DB partial document update (patch operation)](https://learn.microsoft.com/azure/cosmos-db/partial-document-update)
