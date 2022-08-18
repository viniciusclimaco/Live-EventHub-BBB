using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Text;

namespace Receivers.Processor
{
    public class CodersInRioEventProcessor : IEventProcessor
    {
        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine($"Shutting down processor for partition {context.PartitionId}. " + $"Reason: {reason}");
            return Task.CompletedTask;
        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine($"Initialized processor for partittion {context.PartitionId}");
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            Console.WriteLine($"Error for partition {context.PartitionId} : {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            if (messages != null)
            {
                foreach (var eventDt in messages)
                {
                    var dataAsJson = Encoding.UTF8.GetString(eventDt.Body.Array);
                    //var codersInRioData = JsonConvert.DeserializeObject<CodersInRioData>(dataAsJson);
                    Console.WriteLine($"{dataAsJson} | PartitionId: {context.PartitionId} | OffSet: {eventDt.SystemProperties.Offset}");
                }
            }
            // Armazena o atual offset
            return context.CheckpointAsync();
        }
    }
}
