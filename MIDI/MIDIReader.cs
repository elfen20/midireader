using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cave.IO;

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

        public static MIDIFile ReadFile(string fileName)
        {
            using (FileStream stream = File.OpenRead(fileName))
            {
                return ReadFile(stream);
            }
        }


        public static List<MIDIChunk> ReadAllChunks(Stream stream)
        {
            var reader = new MIDIReader(stream);
            var chunks = new List<MIDIChunk>();

            while (true)
            {
                MIDIChunk chunk;
                if (reader.GetNextChunk(out chunk))
                {
                    chunks.Add(chunk);
                }
                else
                { break; }
            }

            return chunks;
        }

        public static MIDIFile ReadFile(Stream stream)
        {
            var chunks = ReadAllChunks(stream);

            return new MIDIFile(chunks);
        }

        bool GetNextChunk(out MIDIChunk chunk)
        {
            chunk = default(MIDIChunk);
            if (dReader.Available > 8)
            {
                chunk.Type = ParseType(dReader.ReadBytes(4));
                chunk.Length = dReader.ReadInt32();
                if (dReader.Available >= chunk.Length)
                {
                    chunk.Data = dReader.ReadBytes(chunk.Length);
                    return true;
                }
            }


            return false;
        }

        MIDIChunkType ParseType(byte[] type)
        {
            switch (Encoding.ASCII.GetString(type))
            {
                case "MThd":
                    return MIDIChunkType.Header;
                case "MTrk":
                    return MIDIChunkType.Track;
                default:
                    return MIDIChunkType.Unknown;
            }
            
        }

        void Close()
        {
            dReader.Close();
        }

        DataReader dReader;

        public MIDIReader(Stream stream)
        {
            dReader = new DataReader(stream,Cave.IO.StringEncoding.ASCII,NewLineMode.CRLF,EndianType.BigEndian);
            
        }

    }
}
