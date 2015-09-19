using UnityEngine;
using System.Collections;

public class SongController : MonoBehaviour {

    private AudioSource sourceBattle;
    private AudioSource sourceBeat;

    public float CurrentTime { get { return sourceBattle != null ? sourceBattle.time : 0.0f; } }

    public Song currentSong { get; set; }

    public void SetCurrentSong(Song song) {
        StopAudio();
        currentSong = song;
    }

	// Use this for initialization
	void Start () {
        sourceBattle = GameObject.Find("BattleSource").GetComponent<AudioSource>();
        sourceBeat = GameObject.Find("BeatSource").GetComponent<AudioSource>();
        //currentSong = Song.Load("01-its-tricky");
        //StartAudio();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void StartAudio() {
        sourceBattle.clip = currentSong.battleClip;
        sourceBeat.clip = currentSong.beatClip;
        sourceBattle.volume = 1.0f;
        sourceBeat.volume = 1.0f;
        sourceBeat.loop = true;
        sourceBeat.mute = true;
        sourceBattle.mute = false;
        sourceBeat.Play();
        sourceBattle.Play();
    }

    public void StopAudio() {
        sourceBattle.Stop();
        sourceBeat.Stop();
    }

    public void ChangeAudioHearable() {
        if (sourceBeat.mute)
        {
            sourceBattle.Pause();
            sourceBeat.mute = false;
        }
        else {
            sourceBeat.mute = true;
            sourceBattle.Play();
        }
    }
}
