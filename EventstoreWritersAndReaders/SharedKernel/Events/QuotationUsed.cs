using System;
using TomKernel;

namespace SharedKernel.Events
{
    public class QuotationUsed : Event
    {
        public Guid SpeechId { get; private set; }
        public string Quotation { get; private set; }
        public DateTime At { get; private set; }

        public QuotationUsed(Guid id, Guid speechId, string quotation, DateTime at)
        {
            Id = id;
            SpeechId = speechId;
            Quotation = quotation;
            At = at;
        }
    }
}
