using UnityEngine;
using System.Collections;

public class SongController : MonoBehaviour {

    private AudioSource sourceBattle;
    private AudioSource sourceBeat;

    public float CurrentTime { get { return sourceBattle != null ? sourceBattle.time : 0.0f; } }

    public Song currentSong;


	// Use this for initialization
	void Start () {
        sourceBattle = GameObject.Find("BattleSource").GetComponent<AudioSource>();
        sourceBeat = GameObject.Find("BeatSource").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void SetCurrentSong(Song song)
    {
        currentSong = song;
    }

    public void StartAudio() {
        sourceBattle.clip = currentSong.battleClip;
        sourceBeat.clip = currentSong.beatClip;
        sourceBeat.loop = true;
        sourceBeat.mute = true;
        sourceBattle.mute = false;
        sourceBeat.Play();
        sourceBattle.Play();
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
