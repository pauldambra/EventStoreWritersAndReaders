using System;
using TomKernel;

namespace SharedKernel.Events
{
    class SpeechStarted : Event
    {
        public DateTime At { get; private set; }
        public Guid SpeechId { get; private set; }

        public SpeechStarted(Guid speechId, DateTime at)
        {
            SpeechId = speechId;
            At = at;
        }
    }
}
