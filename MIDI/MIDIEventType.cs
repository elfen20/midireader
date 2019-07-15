using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDI
{
    public enum MIDIEventType
    {
        Unknown = -1,
        NoteOff = 0x8,
        NoteOn = 0x9,
        PolyphonicKeyPressure = 0xA,
        ControllerChange = 0xB,
        ProgramChange = 0xC,
        KeyPressure = 0xD,
        ChannelPitchBend = 0xE,
        Meta = 0xF,
    }
}
