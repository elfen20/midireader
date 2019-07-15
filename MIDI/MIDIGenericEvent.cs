using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDI
{
    public class MIDIGenericEvent : MIDIEvent
    {
        public byte[] Data { get; private set; }

        public MIDIGenericEvent(int delta, byte type, byte[] data) : base(delta, type)
        {
            Data = data;
        }
    }
}
