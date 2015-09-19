using UnityEngine;
using System.Collections;

public class Song {

    public string filename;

    public SongTimings timings;
    public AudioClip battleClip;
    public AudioClip beatClip;
    //Lyrics lyrics;

    public static Song Load(string name) {
        Song song = new Song();

        song.timings = SongTimings.Load(name);

        song.battleClip = Resources.Load<AudioClip>("music/" + name + "/beats.ogg");
        song.beatClip = Resources.Load<AudioClip>("music/" + name + "/battle.ogg");

        return song;
    }
}
