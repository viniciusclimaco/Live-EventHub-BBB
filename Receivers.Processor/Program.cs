using Microsoft.Azure.EventHubs.Processor;
using Receivers.Processor;

const string eventHubPath = "hub-contagem";
const string consumerGroupName = "contagem";
const string eventHubConnectionString = "Endpoint=sb://evhns-climaco-teste.servicebus.windows.net/;SharedAccessKeyName=connection;SharedAccessKey=OpEBfNXw28FcimwNGFKDGCtf3tO21Z/tu9U4SMKwVC4=;EntityPath=hub-contagem";
const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=apresentacaobbbstorage;AccountKey=ko13UDLWRLgzgldmNigRpa/OBZz06/J5GeAxa81NBgxvPOVQ1KFSvGQkFVnc2ADqfjoROMKrr1wF+AStXYIbvQ==;EndpointSuffix=core.windows.net";
const string leaseContainerName = "processcheckpoint";

Console.WriteLine($"Register the {nameof(CodersInRioEventProcessor)}");
var eventProcessorHost = new EventProcessorHost(
    eventHubPath,
    consumerGroupName,
    eventHubConnectionString,
    storageConnectionString,
    leaseContainerName
    );

await eventProcessorHost.RegisterEventProcessorAsync<CodersInRioEventProcessor>();

Console.WriteLine("Waiting for incoming events...");
Console.WriteLine("Press any key to shutdown");
Console.ReadLine();

await eventProcessorHost.UnregisterEventProcessorAsync();
