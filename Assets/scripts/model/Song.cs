using UnityEngine;
using System.Collections;

public class Song {

    public string filename;

    public SongTimings timings;
    public SongLyrics lyrics;
    public AudioClip battleClip;
    public AudioClip beatClip;

    public static Song Load(string name) {
        Song song = new Song();

        song.timings = SongTimings.Load(name);
        song.lyrics = SongLyrics.Load(name);
        
        song.battleClip = Resources.Load<AudioClip>("music/" + name + "/battle");
        song.beatClip = Resources.Load<AudioClip>("music/" + name + "/beat");

        return song;
    }
}
