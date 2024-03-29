﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Song {

    public string filename;

    public SongTimings timings;
    public SongLyrics lyrics;
    public List<int> blanks;
    public List<string> blankChars;
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
