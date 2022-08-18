using EventHub.Model;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System.Text;

namespace Receivers.Direct
{
    public class EventHubPartitionReceiveHandler : IPartitionReceiveHandler
    {
        public string PartitionId { get; }
        public EventHubPartitionReceiveHandler(string partitionId)
        {
            PartitionId = partitionId;        
        }
        int IPartitionReceiveHandler.MaxBatchSize { get => 10; set => throw new NotImplementedException(); }

        public Task ProcessErrorAsync(Exception error)
        {
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(IEnumerable<EventData> events)
        {
            if (events != null)
            {
                foreach (var eventDt in events)
                {
                    var dataAsJson = Encoding.UTF8.GetString(eventDt.Body.Array);
                    //var codersInRioData = JsonConvert.DeserializeObject<CodersInRioData>(dataAsJson);
                    Console.WriteLine($"{dataAsJson} | PartitionId: {PartitionId} | OffSert: {eventDt.SystemProperties.Offset}");
                }
            }
            return Task.CompletedTask;
        }
    }

}
