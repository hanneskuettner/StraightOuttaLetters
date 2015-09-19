﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("song")]
public class SongTimings {

    public string Name;
    public float length;

    [XmlArray("times"), XmlArrayItem("time")]
    List<float> times;

    private string name;

    public static SongTimings Load(string name)
    {
        Debug.Log("songs/" + name + "/timings.xml");
        TextAsset textFile = (TextAsset)Resources.Load("songs/" + name + "/timings");
        Debug.Log(textFile);
        StringReader reader = new StringReader(textFile.text);

        XmlSerializer serializer = new XmlSerializer(typeof(SongTimings));
        SongTimings song = serializer.Deserialize(reader) as SongTimings;

        song.Name = name;

        return song;
    }

    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SongTimings));

        FileStream stream = new FileStream("Assets/resources/songs/" + this.name + "/timings.xml", FileMode.OpenOrCreate);

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
