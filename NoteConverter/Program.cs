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
            var r = MIDIReader.ReadAllChunks("test.mid");
        }
    }
}
