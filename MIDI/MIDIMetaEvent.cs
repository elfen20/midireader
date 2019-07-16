using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDI
{
    public class MIDIMetaEvent : MIDIEvent
    {
        public byte MetaType { get; private set; }
        public byte[] Data { get; private set; }

        public MIDIMetaEvent(int delta, byte eventType, byte metaType, byte[] data) : base(delta, eventType)
        {
            MetaType = metaType;
            Data = data;
        }
    }
}
