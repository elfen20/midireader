namespace MIDI
{
    public struct MIDIChunk
    {
        public MIDIChunkType Type;
        public int Length;
        public byte[] Data;

        public override string ToString()
        {
            return $"{Type.ToString()} Chunk, {Length} bytes";
        }
    }
}
