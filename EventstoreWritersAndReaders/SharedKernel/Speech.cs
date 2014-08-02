using System;
using System.Collections.Generic;
using SharedKernel.Events;
using TomKernel;

namespace SharedKernel
{
    public class Speech : AggregateRoot
    {
        private Guid _id;
        private DateTime _startedAt;
        private DateTime _finishedAt;
        private readonly List<string> _quotesUsed = new List<string>();

        public override Guid Id { get { return _id; } }

        public Speech()
        {
            //for eventstore
        }

        public Speech(Guid id)
        {
            ApplyChange(new SpeechStarted(id, DateTime.Now));
        }

        public void QuotationUsed(string quote)
        {
            ApplyChange(new QuotationUsed(Guid.NewGuid(), _id, quote, DateTime.Now));
        }

        public void Finish()
        {
            ApplyChange(new SpeechFinished(_id, DateTime.Now));    
        }

        private void Apply(SpeechStarted e)
        {
            _id = e.SpeechId;
            _startedAt = e.At;
        }

        private void Apply(SpeechFinished e)
        {
            _finishedAt = e.At;
        }

        private void Apply(QuotationUsed quoteUsed)
        {
            _quotesUsed.Add(quoteUsed.Quotation);
        }
    }
}
