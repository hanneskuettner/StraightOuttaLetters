using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("lyrics")]
public class SongLyrics {
    public class Lyric {
        [XmlText]
        public string lyric;
    }
    public class Player : Lyric { }
    public class Oppenent : Lyric { }

    public class Line
    {
        [XmlArray("parts")]
        [XmlArrayItem("player", Type = typeof(Player))]
        [XmlArrayItem("opponent", Type = typeof(Oppenent))]
        public List<Lyric> lyrics;

        public int Length
        {
            get
            {
                int length = 0;
                foreach (Lyric l in lyrics)
                {
                    length += l.lyric.Length + 1;
                }
                return length;
            }
        }
    }

    [XmlArray("lines"), XmlArrayItem("line")]
    public List<Line> lines;

    public static SongLyrics Load(string name) {
        TextAsset textFile = (TextAsset)Resources.Load("songs/" + name + "/lyrics");

        StringReader reader = new StringReader(textFile.text);

        XmlSerializer serializer = new XmlSerializer(typeof(SongLyrics));
        SongLyrics lyrics = serializer.Deserialize(reader) as SongLyrics;

        lyrics.Dump();
        return lyrics;
    }

    public void Dump()
    {
        int idx = 1;
        foreach (Line line in lines)
        {
            Debug.Log(idx++);
            foreach (Lyric l in line.lyrics)
            {
                Debug.Log((l.GetType() == typeof(Player) ? "Player" : "Opponent") + " " + l.lyric);
            }
        }
    }

    public List<string> Lines
    {
        get
        {
            List<string> lines = new List<string>();
            foreach (Line line in this.lines)
            {
                string lineString = "";
                foreach (Lyric lyric in line.lyrics)
                {
                    lineString += lyric.lyric + " ";
                }
                lineString.TrimEnd();
                lines.Add(lineString);
            }
            return lines;
        }
    }
}
