using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDI
{
    public abstract class MIDIEvent : IMIDIEvent
    {
        public int Delta { get; private set; }

        public byte RawType { get; private set; }

        public MIDIEventType Type { get; private set; }

        public MIDIEvent(int delta, byte type)
        {
            Delta = delta;
            RawType = type;
            Type = GetEventType(type);
        }

        public static MIDIEventType GetEventType(byte rawType)
        {
            if (rawType >= 0x80)
            {
                return (MIDIEventType)(rawType >> 4);
            }
            return MIDIEventType.Unknown;
        }
    }
}
