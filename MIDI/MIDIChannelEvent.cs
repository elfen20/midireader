using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDI
{
    public class MIDIChannelEvent : MIDIEvent
    {
        public byte Data1 { get; private set; }
        public byte Data2 { get; private set; }

        public MIDIChannelEvent(int delta, byte type, byte d1, byte d2) : base(delta, type)
        {
            Data1 = d1;
            Data2 = d2;
        }
    }
}
