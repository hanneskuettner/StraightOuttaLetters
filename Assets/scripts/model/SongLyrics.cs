using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class SongLyrics {


    public static SongLyrics Load(string name) {
        TextAsset textFile = (TextAsset)Resources.Load("songs/" + name + "/lyrics.txt");

        StringReader reader = new StringReader(textFile.text);

        XmlSerializer serializer = new XmlSerializer(typeof(SongLyrics));
        SongLyrics lyrics = serializer.Deserialize(reader) as SongLyrics;
        
        return song;
    }
}
