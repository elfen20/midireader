using System.Collections.Generic;
using System.IO;

namespace MIDI
{
    public class MIDIReader
    {
        public static List<MIDIChunk> ReadAllChunks(string fileName)
        {
            using (FileStream stream = File.OpenRead(fileName))
            {
                return ReadAllChunks(stream);
            }
        }

        public static List<MIDIChunk> ReadAllChunks(Stream stream)
        {
            var reader = new MIDIReader(stream);
            var chunks = new List<MIDIChunk>();

            // reader.Close();
            return chunks;
        }

        public MIDIReader(Stream stream)
        {

        }

    }
}
