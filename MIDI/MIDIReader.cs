using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cave.IO;

namespace MIDI
{
    public class MIDIReader
    {
        public static MIDIFile ReadFile(string fileName)
        {
            using (FileStream stream = File.OpenRead(fileName))
            {
                return ReadFile(stream);
            }
        }

        public static MIDIFile ReadFile(Stream stream)
        {

            var reader = new MIDIReader(stream);
            var midifile = reader.ReadMidiFile();
            reader.Close();
            return midifile;
        }

        MIDIFile ReadMidiFile()
        {
            var header = ReadHeader();
            var tracks = ReadAllTracks();

            return new MIDIFile(header, tracks);
        }

        MIDIHeader ReadHeader()
        {
            var header = default(MIDIHeader);
            if (dReader.Available < 14)
            {
                throw new EndOfStreamException("could not read header!");
            }
            var chunktype = dReader.ReadString(4);
            if (chunktype != "MThd") throw new InvalidDataException("Invalid chunktype in place of header found!");
            int length = dReader.ReadInt32();
            header.FileType = ParseFileType(dReader.ReadInt16());
            header.TrackCount = dReader.ReadInt16();
            header.Division = dReader.ReadInt16();
            if (length > 6)
            {
                dReader.Skip(length - 6);
            }
            return header;
        }

        MIDIFileType ParseFileType(int type)
        {
            if ((type > -1) && (type < 3)) 
            {
                return (MIDIFileType)type;
            }
            return MIDIFileType.Unknown;
        }

        List<MIDITrack> ReadAllTracks()
        {
            var tracks = new List<MIDITrack>();
            while (dReader.Available > 8)
            {
                MIDITrack track = GetNextTrack();
                tracks.Add(track);
            }
            return tracks;
        }

        MIDITrack GetNextTrack()
        {
            var events = new List<IMIDIEvent>();
            if (dReader.Available < 8)
            {
                throw new EndOfStreamException("could not read track!");
            }
            var chunktype = dReader.ReadString(4);
            if (chunktype != "MTrk") throw new InvalidDataException("Invalid chunktype found!");
            int length = dReader.ReadInt32();
            long endPos = dReader.BaseStream.Position + length;
            runningStatus = false;
            lastEvent = null;
            while (dReader.BaseStream.Position < endPos)
            {
                IMIDIEvent midiEvent = GetNextEvent();
                events.Add(midiEvent);
            }
                               
            return new MIDITrack(events); ;
        }


        IMIDIEvent GetNextEvent()
        {
            int delta = dReader.Read7BitEncodedInt32();
            byte type = dReader.ReadByte();
            if (type < 0x80)
            {
                // data byte found
                if (runningStatus && lastEvent != null)
                {
                    // if running status the read type byte is actually the first databyte
                    if (lastEvent.Type == MIDIEventType.ProgramChange || lastEvent.Type == MIDIEventType.KeyPressure )
                    {
                        return new MIDIChannelEvent(delta, lastEvent.RawType, type, 0);
                    }
                    else
                    {
                        byte secondData = dReader.ReadByte();
                        return new MIDIChannelEvent(delta, lastEvent.RawType, type, secondData);
                    }
                }
                throw new InvalidDataException("data byte found but running status not set");
            }
            else
            {
                MIDIEventType eType = MIDIEvent.GetEventType(type);
                byte data1, data2;
                switch (eType)
                {
                    case MIDIEventType.ProgramChange:
                    case MIDIEventType.KeyPressure:
                        data1 = dReader.ReadByte();
                        lastEvent = new MIDIChannelEvent(delta, type, data1, 0);
                        break;
                    case MIDIEventType.NoteOff:
                    case MIDIEventType.NoteOn:
                    case MIDIEventType.PolyphonicKeyPressure:
                    case MIDIEventType.ControllerChange:
                    case MIDIEventType.ChannelPitchBend:
                        data1 = dReader.ReadByte();
                        data2 = dReader.ReadByte();
                        lastEvent = new MIDIChannelEvent(delta, type, data1, data2);
                        break;
                    case MIDIEventType.Meta:
                        data1 = dReader.ReadByte();
                        int length = dReader.Read7BitEncodedInt32();
                        byte[] buffer = dReader.ReadBytes(length);
                        lastEvent = new MIDIMetaEvent(delta, type, data1, buffer);
                        break;
                    default:
                        throw new NotImplementedException("unknown MIDI Event found !");
                }

                if (type >= 0xF0 && runningStatus)
                {
                    runningStatus = false;
                }
                else
                {
                    runningStatus = true;
                }
                return lastEvent;
            }
        }

        void Close()
        {
            dReader.Close();
        }

        DataReader dReader;
        bool runningStatus = false;
        IMIDIEvent lastEvent;

        public MIDIReader(Stream stream)
        {
            dReader = new DataReader(stream,Cave.IO.StringEncoding.ASCII,NewLineMode.CRLF,EndianType.BigEndian);            
        }

    }
}
