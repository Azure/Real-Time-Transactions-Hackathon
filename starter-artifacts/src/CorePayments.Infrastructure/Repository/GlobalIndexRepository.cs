using CorePayments.Infrastructure.Domain.Entities;
using CorePayments.Infrastructure.Domain.Settings;
using CorePayments.Infrastructure.Events;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using static CorePayments.Infrastructure.Constants;

namespace CorePayments.Infrastructure.Repository
{
    public class GlobalIndexRepository : CosmosDbRepository, IGlobalIndexRepository
    {
        public GlobalIndexRepository(CosmosClient client, IOptions<DatabaseSettings> options) :
            base(client, containerName: options.Value.GlobalIndexContainer ?? string.Empty, options)
        {
        }

        public async Task ProcessAccountAssignment(AccountAssignmentOperations operation, string memberId, string accountId)
        {
            /* TODO: Challenge 3.
            * Uncomment and complete the following lines as instructed.
            */
            if (operation == AccountAssignmentOperations.Add)
            {
                // Create the global index document for the Member Account record:
                var globalIndexMemberAccount = new GlobalIndex
                {
                    // TODO: Uncomment and complete the following lines to set the id and partitionKey property values.
                    // Remember, we are creating a relationship between an Account and a Member. Pay attention to the
                    // targetDocType set below as a hint for the id value. The partitionKey value should be for the Member.
                    //id = <SET THE ID VALUE>,
                    //partitionKey = <SET THE PARTITION KEY VALUE>,
                    targetDocType = DocumentTypes.AccountSummary
                };
                // Create the global index document for the Account Member record:
                var globalIndexAccountMember = new GlobalIndex
                {
                    // TODO: Uncomment and complete the following lines to set the id and partitionKey property values.
                    // Remember, we are creating a relationship between a Member and an Account. Pay attention to the
                    // targetDocType set below as a hint for the id value. The partitionKey value should be for the Member.
                    //id = <SET THE ID VALUE>,
                    //partitionKey = <SET THE PARTITION KEY VALUE>,
                    targetDocType = DocumentTypes.Member
                };

                // Cannot do a batch operation because the primary keys are different.
                await Container.CreateItemAsync(globalIndexMemberAccount, new PartitionKey(globalIndexMemberAccount.partitionKey));
                await Container.CreateItemAsync(globalIndexAccountMember, new PartitionKey(globalIndexAccountMember.partitionKey));
                return;
            }

            // Perform a point read to retrieve the global index document for the Member Account record if it exists:
            var pk = new PartitionKey(memberId);
            var responseReadGlobalIndex = await Container.ReadItemAsync<GlobalIndex>(accountId, pk);
            var globalIndexMemberAccountToDelete = responseReadGlobalIndex.Resource;

            // Perform a point read to retrieve the global index document for the Account Member record if it exists:
            pk = new PartitionKey(accountId);
            responseReadGlobalIndex = await Container.ReadItemAsync<GlobalIndex>(memberId, pk);
            var globalIndexAccountMemberToDelete = responseReadGlobalIndex.Resource;

            // Delete the global index records.
            if (globalIndexMemberAccountToDelete != null)
            {
                await Container.DeleteItemAsync<GlobalIndex>(globalIndexMemberAccountToDelete.id,
                    new PartitionKey(globalIndexMemberAccountToDelete.partitionKey));
            }
            if (globalIndexAccountMemberToDelete != null)
            {
                await Container.DeleteItemAsync<GlobalIndex>(globalIndexAccountMemberToDelete.id,
                    new PartitionKey(globalIndexAccountMemberToDelete.partitionKey));
            }
        }

        public async Task<IEnumerable<GlobalIndex>> GetAccountsForMember(string memberId)
        {
            /* TODO: Challenge 3.
            * Uncomment and complete the following lines as instructed.
            */
            // TODO: Uncomment and complete the following lines to create a query to retrieve the global index documents.
            // HINT: You will need to set the docType value to be an account summary.
            //QueryDefinition query = new QueryDefinition("select * from c where <COMPLETE THE QUERY>")
                //.WithParameter("@memberId", <SET THE MEMBER ID VALUE>)
                //.WithParameter("@docType", <SET THE DOC TYPE VALUE>);
            //return await Query<GlobalIndex>(query);
            // TODO: Comment out or delete the below line after completing the above.
            return new List<GlobalIndex>();
        }

        public async Task CreateItem(GlobalIndex globalIndex)
        {
            await Container.CreateItemAsync(globalIndex, new PartitionKey(globalIndex.partitionKey));
        }
    }
}