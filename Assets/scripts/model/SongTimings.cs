using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("song")]
public class SongTimings {

    public string name;
    public float length;

    [XmlArray("times"), XmlArrayItem("time")]
    List<float> times;

    private string filename;

    public static SongTimings Load(string filename)
    {
        TextAsset textFile = (TextAsset)Resources.Load("songs/" + filename);

        StringReader reader = new StringReader(textFile.text);

        XmlSerializer serializer = new XmlSerializer(typeof(SongTimings));
        SongTimings song = serializer.Deserialize(reader) as SongTimings;

        song.filename = filename;

        return song;
    }

    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(WordDictionary));

        FileStream stream = new FileStream("Assets/resources/songs/" + this.filename + ".xml", FileMode.OpenOrCreate);

        times.Sort();

        try
        {
            serializer.Serialize(stream, this);
        }
        catch
        {
            throw;
        }
        finally
        {
            stream.Close();
            stream.Dispose();
        }
    }
}
