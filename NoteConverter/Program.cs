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
        static void AnalyzeTrack(MIDITrack track)
        {
            foreach (IMIDIEvent lEvent in track.Events)
            {                
                switch (lEvent.Type)
                {
                    case MIDIEventType.NoteOn:
                        MIDIChannelEvent cEvent = (MIDIChannelEvent)lEvent;
                        Console.WriteLine($"Note on: {cEvent.Data1}");
                        break;
                    case MIDIEventType.Meta:
                        MIDIMetaEvent mEvent = (MIDIMetaEvent)lEvent;
                        if (mEvent.MetaType == 3)
                        {
                            Console.WriteLine($"Track name: {Encoding.ASCII.GetString(mEvent.Data)}");
                        }
                        if (mEvent.MetaType == 4)
                        {
                            Console.WriteLine($"Instrument: {Encoding.ASCII.GetString(mEvent.Data)}");
                        }
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Reading MIDI File...");
            var r = MIDIReader.ReadFile("test.mid");
            Console.WriteLine($"found {r.Tracks.Count} Tracks(s)...");
            int i = 1;
            foreach (MIDITrack track in r.Tracks)
            {
                Console.WriteLine($"Nr. {i}: {track.ToString()}");
                AnalyzeTrack(track);
                i++;
            }
        }
    }
}
