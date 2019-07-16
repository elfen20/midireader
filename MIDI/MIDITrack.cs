using System.Collections.Generic;

namespace MIDI
{
    public class MIDITrack
    {
        public List<IMIDIEvent> Events { get; private set; }

        public MIDITrack(List<IMIDIEvent> events)
        {
            Events = events;
        }
    }
}