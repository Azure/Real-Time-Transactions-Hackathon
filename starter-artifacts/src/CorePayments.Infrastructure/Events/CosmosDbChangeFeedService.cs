﻿using CorePayments.Infrastructure.Repository;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CorePayments.Infrastructure.Domain.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace CorePayments.Infrastructure.Events
{
    public class CosmosDbChangeFeedService : ICosmosDbChangeFeedService
    {
        private readonly CosmosClient _client;
        private readonly Database _database;
        private readonly Container _transaction;
        private readonly Container _leases;

        private ChangeFeedProcessor _changeFeedProcessorProcessCustomerView;

        private readonly ILogger<CosmosDbChangeFeedService> _logger;
        private readonly ICustomerRepository _customerRepository;

        private bool _changeFeedsInitialized = false;

        public bool IsInitialized => _changeFeedsInitialized;

        public CosmosDbChangeFeedService(CosmosClient client,
            ILogger<CosmosDbChangeFeedService> logger,
            ICustomerRepository customerRepository,
            IOptions<DatabaseSettings> options)
        {
            _client = client;
            _customerRepository = customerRepository;
            _logger = logger;

            var database = _client.GetDatabase(options.Value.PaymentsDatabase);

            _database = database ??
                        throw new ArgumentException("Unable to connect to existing Azure Cosmos DB database.");

            _transaction = database?.GetContainer(options.Value.TransactionsContainer) ??
                        throw new ArgumentException("Unable to connect to existing Azure Cosmos DB container or database.");
            _leases = database?.GetContainer("leases") ??
                      throw new ArgumentException("Unable to connect to existing Azure Cosmos DB container or database.");
        }

        public async Task StartChangeFeedProcessorsAsync()
        {
            _logger.LogInformation("Starting Change Feed Processors...");
            try
            {
                _changeFeedProcessorProcessCustomerView = _transaction
                    .GetChangeFeedProcessorBuilder<JObject>("ProcessCustomerView", ProcessCustomerViewChangeFeedHandler)
                    .WithInstanceName("ProcessCustomerView")
                    .WithLeaseContainer(_leases)
                    .WithStartTime(DateTime.MinValue.ToUniversalTime()) // Read from the beginning.
                    .Build();

                await _changeFeedProcessorProcessCustomerView.StartAsync();

                _changeFeedsInitialized = true;
                _logger.LogInformation("Change Feed Processors started.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing change feed processors.");
            }
        }

        public async Task StopChangeFeedProcessorAsync()
        {
            // Stop the ChangeFeedProcessor
            _logger.LogInformation("Stopping Change Feed Processors...");

            if (_changeFeedProcessorProcessCustomerView != null) await _changeFeedProcessorProcessCustomerView.StopAsync();

            _logger.LogInformation("Change Feed Processors stopped.");
        }

        private async Task ProcessCustomerViewChangeFeedHandler(
            ChangeFeedProcessorContext context,
            IReadOnlyCollection<JObject> input,
            CancellationToken cancellationToken)
        {
            /* TODO: Challenge 3.
             * Uncomment and complete the following lines as instructed.
             */
            using var logScope = _logger.BeginScope("Cosmos DB Change Feed Processor: ProcessCustomerViewChangeFeedHandler");

            _logger.LogInformation("Cosmos DB Change Feed Processor: Processing {count} changes...", input.Count);

            await Parallel.ForEachAsync(input, cancellationToken, async (record, token) =>
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
                    _logger.LogError(ex.Message, ex);
                }
            });
        }

    }
}
