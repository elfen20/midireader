using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDI
{
    public class MIDIFile
    {
        public readonly MIDIHeader Header;
        public readonly List<MIDITrack> Tracks;

        public MIDIFile(List<MIDIChunk> chunks)
        {

        }
    }
}
