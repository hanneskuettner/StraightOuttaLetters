using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private SongtextDisplayer songTextDisplayer;

	// Use this for initialization
	void Start () {
        Song song = Song.Load("01-its-tricky");

        songTextDisplayer = FindObjectOfType<SongtextDisplayer>();

        songTextDisplayer.SetLines(song.lyrics.lines);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void StartGame()
    {
    }

    public void PutWord(string word) { 
        //TODO: Logic
    }
}
