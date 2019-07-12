using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIDI;

namespace NoteConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading MIDI File...");
            var r = MIDIReader.ReadAllChunks("test.mid");
            Console.WriteLine($"found {r.Count} Chunk(s)...");
            int i = 1;
            foreach (MIDIChunk chunk in r)
            {
                Console.WriteLine($"Nr. {i}: {chunk.ToString()}");
                i++;
            }
        }
    }
}
