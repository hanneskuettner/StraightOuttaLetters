using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("dict")]
public class WordDictionary {
    public class Word
    {
        [XmlAttribute("original")]
        public string original;
        [XmlArray("alts"), XmlArrayItem("alt")]
        public List<string> alts;
    }

    [XmlArray("words"), XmlArrayItem("word")]
    public List<Word> words;

    private string filename;

    public static WordDictionary Load(string filename)
    {
        TextAsset textFile = (TextAsset)Resources.Load(filename);

        StringReader reader = new StringReader(textFile.text);

        XmlSerializer serializer = new XmlSerializer(typeof(WordDictionary));
        WordDictionary dict = serializer.Deserialize(reader) as WordDictionary;

        dict.filename = filename;

        return dict;
    }

    public void Save() {
	    XmlSerializer serializer = new XmlSerializer (typeof(WordDictionary));

	    FileStream stream = new FileStream ("Assets/resources/" + this.filename + ".xml", FileMode.OpenOrCreate);

	    words.Sort();
	    foreach (Word w in words) {
		    w.alts.Sort();
	    }
		
	    try {
		    serializer.Serialize(stream, this);
	    }
	    catch {
		    throw;
	    }
	    finally {
		    stream.Close();
		    stream.Dispose();
	    }
    }

    public List<string> Lookup(string word)
    {
        foreach (Word w in words)
        {
            if (w.original == word)
                return w.alts;
        }
        return null;
    }
}
