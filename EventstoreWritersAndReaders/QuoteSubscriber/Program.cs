using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedKernel.Events;
using TomKernel;

namespace QuoteSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            SubscribeToQuotes();

            Console.WriteLine("waiting for someone to use a quote");
            Console.WriteLine("press q to quit");

            var running = true;
            while (running)
            {
                var input = Console.ReadKey();
                if (input.KeyChar.Equals('q'))
                {
                    running = false;
                }
            }
        }

        private static QuotationUsed DeserializeEvent(byte[] metadata, byte[] data)
        {
            const string eventClrTypeHeader = "EventClrTypeName";
            var parsedMetadata = JObject.Parse(Encoding.UTF8.GetString(metadata));
            var eventClrTypeName = parsedMetadata.Property(eventClrTypeHeader).Value;
            return (QuotationUsed) JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)eventClrTypeName));
        }

        private static async void SubscribeToQuotes()
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ipaddress"]), 1113);
            var connection = EventStoreConnection.Create(ipEndPoint);
            connection.Connect();

            await connection.SubscribeToStreamAsync(
                stream:"quotations", 
                resolveLinkTos:true, 
                eventAppeared: (ess, e) =>
                {
                    var @event = DeserializeEvent(e.Event.Metadata, e.Event.Data);
                    Console.WriteLine(@event.Quotation);
                });
        }

    }
}
