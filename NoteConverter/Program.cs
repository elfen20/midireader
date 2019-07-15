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
            var r = MIDIReader.ReadFile("test.mid");
            Console.WriteLine($"found {r.Tracks.Count} Tracks(s)...");
            int i = 1;
            foreach (MIDITrack track in r)
            {
                Console.WriteLine($"Nr. {i}: {track.ToString()}");
                i++;
            }
        }
    }
}
