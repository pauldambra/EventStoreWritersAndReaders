using System;
using TomKernel;

namespace SharedKernel.Events
{
    class SpeechFinished : Event
    {
        public DateTime At { get; private set; }
        public Guid SpeechId { get; private set; }

        public SpeechFinished(Guid speechId, DateTime at)
        {
            SpeechId = speechId;
            At = at;
        }
    }
}