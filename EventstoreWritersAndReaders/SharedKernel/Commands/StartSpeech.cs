using System;
using TomKernel;

namespace SharedKernel.Commands
{
    public class UseQuotation : Command
    {
        public Guid SpeechId { get; private set; }
        public string Quotation { get; private set; }

        public UseQuotation(Guid speechId, string quotation)
        {
            SpeechId = speechId;
            Quotation = quotation;
        }
    }

    public class StartSpeech : Command
    {
        public Guid SpeechId { get; private set; }

        public StartSpeech(Guid speechId)
        {
            SpeechId = speechId;
        }
    }

    public class FinishSpeech : Command
    {
        public Guid SpeechId { get; private set; }

        public FinishSpeech(Guid speechId)
        {
            SpeechId = speechId;
        }
    }
}
