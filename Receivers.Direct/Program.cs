using Microsoft.Azure.EventHubs;
using Receivers.Direct;

Console.WriteLine("Connecting to the Event Hub...");
const string eventConnString = "Endpoint=sb://evhns-climaco-teste.servicebus.windows.net/;SharedAccessKeyName=connection;SharedAccessKey=OpEBfNXw28FcimwNGFKDGCtf3tO21Z/tu9U4SMKwVC4=;EntityPath=hub-contagem";
var eventHubClient = EventHubClient.CreateFromConnectionString(eventConnString);

var runtimeInformation = await eventHubClient.GetRuntimeInformationAsync();
var partitionReceivers = runtimeInformation.PartitionIds.Select(partitionId => 
                         eventHubClient.CreateReceiver("contagem", partitionId, EventPosition.FromStart())).ToList();

Console.WriteLine("Waiting for incoming events...");

foreach (var partitionReceiver in partitionReceivers)
{
    partitionReceiver.SetReceiveHandler(new EventHubPartitionReceiveHandler(partitionReceiver.PartitionId));
}

Console.WriteLine("Press any key to shutdown");
Console.ReadLine();
await eventHubClient.CloseAsync();

//while (true)
//{
//    var eventData = await partitionReceiver.ReceiveAsync(10);
//    if (eventData != null)
//    {
//        foreach (var eventDt in eventData)
//        {
//            var dataAsJson = Encoding.UTF8.GetString(eventDt.Body.Array);
//            Console.WriteLine($"{dataAsJson} | PartitionId: {partitionReceiver.PartitionId}");
//        }
//    }
//}



