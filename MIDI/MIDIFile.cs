using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDI
{
    public class MIDIFile
    {
        public MIDIHeader Header { get; private set; }
        public List<MIDITrack> Tracks { get; private set}

        public MIDIFile(MIDIHeader header, List<MIDITrack> tracks)
        {
            Header = header;
            Tracks = tracks;
        }
    }
}
