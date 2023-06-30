using CorePayments.Infrastructure.Domain.Entities;
using CorePayments.Infrastructure.Repository;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorePayments.FunctionApp.Processor
{
    public class ProcessCustomerView
    {
        readonly bool _isMasterRegion;
        readonly ICustomerRepository _customerRepository;

        public ProcessCustomerView(
            ICustomerRepository customerRepository)
        {
            _isMasterRegion = Convert.ToBoolean(Environment.GetEnvironmentVariable("isMasterRegion"));
            _customerRepository = customerRepository;
        }

        [Function("ProcessCustomerView")]
        public async Task RunAsync([CosmosDBTrigger(
            databaseName: "%paymentsDatabase%",
            containerName: "%transactionsContainer%",
            Connection = "CosmosDBConnection",
            CreateLeaseContainerIfNotExists = true,
            StartFromBeginning = true,
            FeedPollDelay = 1000,
            MaxItemsPerInvocation = 50,
            PreferredLocations = "%preferredRegions%",
            LeaseContainerName = "leases")]IReadOnlyList<JObject> input,
            FunctionContext context)
        {
            /* TODO: Challenge 3.
             * Uncomment and complete the following lines as instructed.
             */
            var logger = context.GetLogger<ProcessCustomerView>();

            if (!_isMasterRegion)
                return;

            await Parallel.ForEachAsync(input, async (record, token) =>
            {
                try
                {
                    // This method of writing the document from the change feed directly to the customerTransactions container
                    // with no modifications to the document is called a "passthrough" pattern. We are taking this step to
                    // implement a CQRS pattern, where the customerTransactions container is the "read" side of the pattern and
                    // the transactions container is the "write" side of the pattern. This pattern is used to optimize the
                    // read and write operations for each container, minimizing impact on potentially heavy write operations.

                    // TODO: Uncomment the following line and complete the code to upsert (insert/update) the document to the
                    // customerTransactions container via the customer repository.
                    //await ___________;
                }
                catch (Exception ex)
                {
                    //Should handle DLQ
                    logger.LogError(ex.Message, ex);
                }
            });
        }
    }
}