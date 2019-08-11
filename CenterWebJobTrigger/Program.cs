using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterWebJobTrigger
{
    class Program
    {
        static void Main(string[] args)
        {
            RunOnBackground().Wait();
        }

        static async Task RunOnBackground()
        {
            var conn1 = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            var conn2 = CloudConfigurationManager.GetSetting("StorageConnectionString");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(conn1);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("sandbox221b");
            _ = await queue.CreateIfNotExistsAsync();

            CloudQueueMessage message = new CloudQueueMessage("Hey! a message is commming");
            await queue.AddMessageAsync(message);

            CloudQueueMessage peekedMessage = queue.PeekMessage();

            Console.WriteLine(peekedMessage.AsString);
        }
    }
}
