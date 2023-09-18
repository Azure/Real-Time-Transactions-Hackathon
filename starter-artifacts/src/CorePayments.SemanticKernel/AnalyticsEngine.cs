using CorePayments.Infrastructure.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.Planning;
using System.Text.Json;

namespace CorePayments.SemanticKernel
{
    public class AnalyticsEngine : IAnalyticsEngine
    {
        readonly AnalyticsEngineSettings _settings;

        public AnalyticsEngine(
            IOptions<AnalyticsEngineSettings> settings)
        {
            _settings = settings.Value;
        }


        public async Task<string> ReviewTransactions(IEnumerable<Transaction> transactions, string query)
        {
            /* TODO: Challenge 4.
             * Uncomment and complete the following lines as instructed.
             */
            // TODO: Uncomment the code below and instantiate a new KernelBuilder.
            //var builder = __________;
            // builder.WithAzureChatCompletionService(
            //          _settings.OpenAICompletionsDeployment,
            //          _settings.OpenAIEndpoint,
            //          _settings.OpenAIKey);

            // TODO: Create a new kernel instance from the builder.
            //var kernel = ___________;

            string skPrompt = @"
            You are an analyst bot that helps staff summarize data about account transactions by processing a list of transactions. 
            You are provided the list of transactions in the JSON format, as well as the query submitted by the user.
            You can return your results in JSON or the format specified by the user in the query. 

            For example:

            +++

            [INPUT]
            Transaction Data:
            [
              {
                ""id"": ""9b0e2f75-6316-4d88-aa74-46ae5d4aef7b"",
                ""accountId"": ""0909090907"",
                ""description"": ""Item refund"",
                ""merchant"": ""Tailspin Toys"",
                ""type"": ""deposit"",
                ""amount"": 38.26,
                ""timestamp"": ""2023-06-20T23:13:00.9725896Z""
              },
              {
                ""id"": ""0bb8f13f-65b1-4611-83f5-c2c028ee6545"",
                ""accountId"": ""0909090907"",
                ""description"": ""Online purchase"",
                ""merchant"": ""Tailspin Toys"",
                ""type"": ""debit"",
                ""amount"": 38.26,
                ""timestamp"": ""2023-06-20T22:39:45.3257116Z""
              }
            ]

            User Query:
            How many transactions does the accountId ""0909090907"" have?
            [END INPUT]

            Provide your response by completing the following bullet:
            - Result: 2

            +++

            [INPUT]
            Transaction Data:
            {{$transactionData}}

            User Query:
            {{$query}}
            [END INPUT]

            Provide your response by completing the following bullet on a new line:
            - Result:
            ";

            // TODO: Complete the code below to create a new semantic function for the review skill.
            // HINT: The first parameter is the semantic kernel prompt created above.
            //var reviewer = kernel.CreateSemanticFunction(______, "review", "ReviewSkill", description: "Review the input.", maxTokens: 2000, temperature: 0.0);

            JsonSerializerOptions ser_options = new()
            {
                WriteIndented = true,
                MaxDepth = 20,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            // Optimize the transaction data we send as context to the semantic function.
            var contextData = transactions.Select(t => new
            {
                t.description,
                t.merchant,
                t.type,
                t.amount,
                t.timestamp
            });

            var transactionData = JsonSerializer.Serialize(contextData, ser_options);

            // TODO: Uncomment the below three lines to create a new context from the kernel and set the
            //       transaction context to your transaction data and the query context to your user query.
            //var context = kernel._______();
            //context["transactionData"] = transactionData;
            //context["query"] = query;

            string result = string.Empty;
            // TODO: Uncomment the following line to finish the review skill invocation.
            //string result = (await reviewer.InvokeAsync(context)).Result;

            return result;
        }
    }
}