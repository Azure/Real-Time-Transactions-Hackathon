using CorePayments.Infrastructure.Domain.Entities;
using CorePayments.Infrastructure.Domain.Settings;
using CorePayments.Infrastructure.Events;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Net;

namespace CorePayments.Infrastructure.Repository
{
    public class TransactionRepository : CosmosDbRepository, ITransactionRepository
    {
        public TransactionRepository(CosmosClient client, IOptions<DatabaseSettings> options) :
            base(client, containerName: options.Value.TransactionsContainer ?? string.Empty, options)
        {
        }

        public async Task<AccountSummary> ProcessTransactionSProc(Transaction transaction)
        {
            var response = await Container.Scripts.ExecuteStoredProcedureAsync<AccountSummary>("processTransaction", new PartitionKey(transaction.accountId), new[] { transaction });

            //Should handle/retry precondition failure

            return response.Resource;
        }

        public async Task<(AccountSummary? accountSummary, HttpStatusCode statusCode, string message)> ProcessTransactionTBatch(Transaction transaction)
        {
            /* TODO: Challenge 3.
             * Uncomment and complete the following lines as instructed.
             */
            var pk = new PartitionKey(transaction.accountId);

            var responseRead = await ReadItem<AccountSummary>(transaction.accountId, transaction.accountId);
            var account = responseRead.Resource;

            if (account == null)
            {
                return new(null, HttpStatusCode.NotFound, "Account not found!");
            }

            // TODO: Create a business rule to check if the transaction type is "debit" and throw an
            //       exception if the transaction amount is greater than the account balance plus the overdraft limit.
            if (transaction.type.ToLowerInvariant() == Constants.DocumentTypes.TransactionDebit)
            {
                // TODO: Uncomment the following lines and complete the code to check if the transaction amount is
                //       greater than the account balance plus the overdraft limit.
                // if ((_____ + _____ ) < transaction.amount)
                // {
                //     return new(null, HttpStatusCode.BadRequest, "Insufficient balance/limit!");
                // }
                // else
                // {
                     account.balance -= transaction.amount;
                // }
            }

            var batch = Container.CreateTransactionalBatch(pk);

            batch.PatchItem(account.id,
                new List<PatchOperation>()
                {
                    PatchOperation.Increment("/balance", transaction.type.ToLowerInvariant() == Constants.DocumentTypes.TransactionDebit ? -transaction.amount : transaction.amount)
                },
                new TransactionalBatchPatchItemRequestOptions()
                {
                    IfMatchEtag = responseRead.ETag
                }
            );
            batch.CreateItem<Transaction>(transaction);

            var responseBatch = await batch.ExecuteAsync();

            if (responseBatch.IsSuccessStatusCode)
            {
                account = responseBatch.GetOperationResultAtIndex<AccountSummary>(0).Resource;
                return new(account, HttpStatusCode.OK, string.Empty);
            }
            else if (responseBatch.StatusCode == HttpStatusCode.PreconditionFailed)
                return new (null, HttpStatusCode.PreconditionFailed, string.Empty);
            else
                return new (null, HttpStatusCode.BadRequest, string.Empty);
        }

        public async Task CreateItem<T>(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await Container.CreateItemAsync(item);
        }
    }
}
