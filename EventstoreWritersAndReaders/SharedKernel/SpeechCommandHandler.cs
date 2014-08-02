using SharedKernel.Commands;
using TomKernel;

namespace SharedKernel
{
    public class SpeechCommandHandler :
        IHandle<StartSpeech>,
        IHandle<FinishSpeech>,
        IHandle<UseQuotation>
    {
        private readonly EventStoreRepository<Speech> _repository;

        public SpeechCommandHandler(EventStoreRepository<Speech> repository)
        {
            _repository = repository;
        }

        public void Handle(StartSpeech args)
        {
            var speech = new Speech(args.SpeechId);
            _repository.Save(speech);
        }

        public void Handle(FinishSpeech args)
        {
            var speech = _repository.GetById(args.SpeechId);
            speech.Finish();
            _repository.Save(speech);
        }

        public void Handle(UseQuotation args)
        {
            var speech = _repository.GetById(args.SpeechId);
            speech.QuotationUsed(args.Quotation);
            _repository.Save(speech);
        }
    }
}
