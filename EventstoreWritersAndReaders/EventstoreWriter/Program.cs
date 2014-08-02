using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using EventStore.ClientAPI;
using SharedKernel;
using SharedKernel.Commands;
using TomKernel;

namespace EventstoreWriter
{
    class Program
    {
        static List<string> quotations = new List<string>
                                            {
                                                "Philander Johnson: Cheer up, the worst is yet to come.",
                                                "Honoré de Balzac: Behind every great fortune there is a crime.",
                                                "Tom Wilson, Ziggy: Honesty is the best image.",
                                                "Will Rogers: Common sense ain't common.",
                                                "Mark Twain: When in doubt, tell the truth.",
                                                "Marvin Kitman: A coward is a hero with a wife, kids, and a mortgage.",
                                                "Haythum R. Khalid: All power corrupts, but we need electricity.",
                                                "Gary Kasparov: Chess is mental torture.",
                                                "George Bernard Shaw: Do not try to live for ever. You will not succeed."
                                            };

        private static IEventStoreConnection _connection;

        static void Main(string[] args)
        {
            try
            {
                InitEventStore();

                var speechId = Guid.NewGuid();
                MessageDispatcher.Send(new StartSpeech(speechId));

                ShowMenu();

                MainLoop(speechId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Doh! => {0}", ex.Message);
            }
        }

        private static void MainLoop(Guid speechId)
        {
            var running = true;
            while (running)
            {
                var input = Console.ReadKey().KeyChar;
                ClearLine();
                if (input == 'q')
                {
                    running = false;
                    MessageDispatcher.Send(new FinishSpeech(speechId));
                    continue;
                }
                int number;
                if (Int32.TryParse(input.ToString(), out number))
                {
                    if (number < quotations.Count)
                    {
                        Console.WriteLine("quotation {0} selected", number);
                        MessageDispatcher.Send(new UseQuotation(speechId, quotations.ElementAt(number)));
                    }
                    else
                    {
                        Console.WriteLine("Unknown quote - try again!");
                    }
                }
            }
        }

        private static void ShowMenu()
        {
            for (var i = 0; i < quotations.Count; i++)
            {
                Console.WriteLine("Press {0} to add quotation: {1}", i, quotations.ElementAt(i));
            }
            Console.WriteLine("press q to quit");
        }

        private static void InitEventStore()
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ipaddress"]), 1113);
            _connection = EventStoreConnection.Create(ipEndPoint);
            _connection.Connect();
            var speechRepository = new EventStoreRepository<Speech>(_connection);
            var speechCommandHandler = new SpeechCommandHandler(speechRepository);

            MessageDispatcher.Register<StartSpeech>(speechCommandHandler.Handle);
            MessageDispatcher.Register<FinishSpeech>(speechCommandHandler.Handle);
            MessageDispatcher.Register<UseQuotation>(speechCommandHandler.Handle);
        }

        static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
