namespace MIDI
{
    public class MIDIHeader
    {
        MIDIChunk chunk;

        public readonly MIDIFileFormat Format;
        public readonly int TrackCount;

        public MIDIHeader(MIDIChunk chunk)
        {

        }
    }
}