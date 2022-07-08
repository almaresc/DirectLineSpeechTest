using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpc_client
{
    public class WavQueueEntry
    {
        public WavQueueEntry(string id, bool playInitiated, ProducerConsumerStream stream, RawSourceWaveStream reader) =>
            (this.Id, this.PlayInitiated, this.Stream, this.Reader) = (id, playInitiated, stream, reader);

        public string Id { get; }

        public bool PlayInitiated { get; set; } = false;

        public bool SynthesisFinished { get; set; } = false;

        public ProducerConsumerStream Stream { get; }

        public RawSourceWaveStream Reader { get; }
    }

    public enum ListenState
    {
        NotListening,
        Initiated,
        Listening,
    }
}
