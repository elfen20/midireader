using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDI
{
    public interface IMIDIEvent
    {
        int Delta { get; }
        byte RawType { get; }
        MIDIEventType Type { get; }
    }
}
