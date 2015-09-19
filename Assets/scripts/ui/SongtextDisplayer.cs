using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SongtextDisplayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        text = SongTextObject.GetComponent<Text>();
	}

    public string[] songLines;
    public float currentTime;
    public int currentBars = 0;
    public int CurrentLine;
    public int bpm = 90;
    public AudioSource audioSource;
    public GameObject SongTextObject;
    private Text text;


	// Update is called once per frame
	void Update () {
        if (audioSource.isPlaying)
        {
            currentTime = audioSource.time / 60;
            currentBars = CalculateCurrentBar(currentTime);
            if (currentBars - 1 % 2 == 0)
                OnBarsDone();
        }
	}

    public int CalculateCurrentBar(float time) {
        float totalBeats = bpm / time;
        return Mathf.FloorToInt(totalBeats/4);
    }

    public void OnBarsDone() { 
        if (CurrentLine > 0 || CurrentLine < songLines.Length - 1)
            text.text = songLines[CurrentLine - 1] + "\n" + songLines[CurrentLine] + "\n" + songLines[CurrentLine + 1];
        else if (CurrentLine == 0)
            text.text = songLines[CurrentLine] + "\n" + songLines[CurrentLine + 1];
        else
            text.text = songLines[CurrentLine - 1] + "\n" + songLines[CurrentLine];
    }

    public string MakeLine(int start, int end, string replacement, string line) { 
        var tmp = line.Substring(start, end - start);
        return line.Replace(tmp, replacement);
    }
}
