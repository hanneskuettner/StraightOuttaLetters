using UnityEngine;
using System.Collections;

public class SongController : MonoBehaviour {

    private AudioSource sourceBattle;
    private AudioSource sourceBeat;

    private int bpm = 128;


    private double currentTime;
    private double nextBeatTime;
    private double beatTime;

    public float CurrentTime { get { return sourceBattle != null ? sourceBattle.time : 0.0f; } }

    public Song currentSong { get; set; }

    public void SetCurrentSong(Song song) {
        StopAudio();
        currentSong = song;
    }

    void Awake()
    {
        sourceBattle = GameObject.Find("BattleSource").GetComponent<AudioSource>();
        sourceBeat = GameObject.Find("BeatSource").GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void StartAudio() {
        sourceBattle.clip = currentSong.battleClip;
        sourceBeat.clip = currentSong.beatClip;
        sourceBeat.loop = true;
        sourceBeat.mute = true;
        sourceBattle.mute = false;
        sourceBeat.Play();
        sourceBattle.Play();
        beatTime = 60.0 / bpm / 2.0;
        currentTime = 0;
        nextBeatTime = 0;
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

    void FixedUpdate()
    {
        if (sourceBattle.isPlaying)
        {
            currentTime += Time.fixedDeltaTime;
            if (currentTime > nextBeatTime)
            {
                nextBeatTime += beatTime;
                //animator.OnBeat();
            }
        }
    }
}
