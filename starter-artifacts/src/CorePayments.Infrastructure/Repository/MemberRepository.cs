﻿using CorePayments.Infrastructure.Domain.Entities;
using CorePayments.Infrastructure.Events;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;

namespace CorePayments.Infrastructure.Repository
{
    public class MemberRepository : CosmosDbRepository, IMemberRepository
    {
        public MemberRepository(CosmosClient client, IEventHubService eventHub) :
            base(client, containerName: Environment.GetEnvironmentVariable("memberContainer") ?? string.Empty, eventHub)
        {
        }

        public async Task CreateItem(Member member)
        {
            await Container.CreateItemAsync(member);
        }

        public async Task<(IEnumerable<Member>? members, string? continuationToken)> GetPagedMembers(int pageSize, string continuationToken)
        {
            QueryDefinition query = new QueryDefinition("select * from c order by c.lastName desc");

            return await PagedQuery<Member>(query, pageSize, null, continuationToken);
        }

        public async Task<int> PatchMember(Member member, string memberId)
        {
            /* TODO: Challenge 3.
             * Uncomment and complete the following lines as instructed.
             */
            JObject obj = JObject.FromObject(member);

            var ops = new List<PatchOperation>();

            foreach (JToken item in obj.Values())
            {
                // TODO: Uncomment the following line and complete the code to skip the following paths:
                //       "id", "memberId", "type", and empty strings.
                //       Hint: Evaluate the item.Path and item.ToString() values.
                // if (____ is "id" or "memberId" or "type" || string.IsNullOrEmpty(_____))
                //     continue;

                // TODO: Uncomment the following line and complete the code to add the patch operation to the list.
                //       Hint: Create a new PatchOperation with the path and value to add the item.
                //ops.Add(______($"/{_____}", item._____()));
            }

            if (ops.Count == 0)
                return 0;

            var response = await Container.PatchItemAsync<Member>(memberId, new PartitionKey(memberId), ops);
            var paths = string.Join(", ", ops.Select(x => x.Path));
            // TODO: Uncomment the following line and complete the code to get the contacted regions from the response.
            //var regions = string.Join(", ", response.______._______().Select(x => x.regionName));

            // TODO: Uncomment the following line and complete the code to trigger the tracking event.
            //await TriggerTrackingEvent($"Performing member patch operations (paths: {paths}) within the {regions} region(s).");

            return ops.Count;
        }
    }
}