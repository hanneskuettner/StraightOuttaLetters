using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class GameController : MonoBehaviour {

    private SongtextDisplayer songTextDisplayer;
    private SongController songController;
    private Song song;
    private int currentLine;
    private int currentChar;
    private int currentBlank;

    private WordDictionary dict;

	// Use this for initialization
	void Start () {
        song = Song.Load("01-its-tricky");

        songTextDisplayer = FindObjectOfType<SongtextDisplayer>();
        songController = FindObjectOfType<SongController>();

        songController.SetCurrentSong(song);

        dict = WordDictionary.Load("dict");

        StartGame();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            song.timings.times.Add(songController.CurrentTime);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(song.timings.times.Count);
            song.timings.Save();
        }


        if (songController.CurrentTime > song.timings.times[currentLine+1])
        {
            currentChar += song.lyrics.lines[currentLine].Length;
            currentLine++;
            songTextDisplayer.SetLines(song.lyrics.lines.GetRange(currentLine, 2));
            bool playerHasLine = false;
            foreach (SongLyrics.Lyric lyric in song.lyrics.lines[currentLine].lyrics)
            {
                if (lyric.GetType() == typeof(SongLyrics.Player))
                {
                    if (song.blanks[currentBlank + 1] < currentChar + song.lyrics.lines[currentLine].Length)
                    {
                        playerHasLine = true;
                        break;
                    }
                }
            }
            if (playerHasLine)
            {
                songController.ChangeAudioHearable();
            }
        }
        if (BlankInLine())
        {
            NextBlank();
            songTextDisplayer.SetCurrentBlank(currentChar, currentBlank);
        }
        else
        {
            songTextDisplayer.SetCurrentBlank(currentChar, -1);
        }
	}

    public bool BlankInLine()
    {
        foreach (SongLyrics.Lyric lyric in song.lyrics.lines[currentLine].lyrics)
        {
            if (lyric.GetType() == typeof(SongLyrics.Player))
            {
                if (song.blanks[currentBlank + 1] < currentChar + song.lyrics.lines[currentLine].Length)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void StartGame()
    {
        songController.StartAudio();
        currentLine = 0;
        currentChar = 0;
        currentBlank = -1;
        GenerateBlanks();
        songTextDisplayer.SetBlankPositions(song.blanks);
        songTextDisplayer.SetCurrentBlank(currentChar, currentBlank);
        songTextDisplayer.SetLines(song.lyrics.lines.GetRange(currentLine, 2));
    }

    private void GenerateBlanks()
    {
        string chars =" \n,.!?\'";
        int charpos = 0;
        song.blanks = new List<int>();
        song.blankChars = new List<char>();
        foreach (SongLyrics.Line line in song.lyrics.lines)
        {
            foreach (SongLyrics.Lyric lyric in line.lyrics)
            {
                if (lyric.GetType() == typeof(SongLyrics.Player))
                {
                    List<int> b = new List<int>();
                    int count = (int)(Random.Range(0, lyric.lyric.Length) / 3f);
                    for (int i = 0; i < count; i++)
                    {
                        int pos = Random.Range(0, lyric.lyric.Length);
                        if (chars.IndexOf(lyric.lyric[pos]) != -1)
                        {
                            continue;
                        }
                        if (!b.Contains(charpos + pos))
                        {
                            b.Add(charpos + pos);
                            song.blankChars.Add(lyric.lyric[pos]);
                            StringBuilder sb = new StringBuilder(lyric.lyric);
                            sb[pos] = '_';
                            lyric.lyric = sb.ToString();
                        }
                    }
                    song.blanks.AddRange(b);
                }
                charpos += lyric.lyric.Length + 1;
            }
        }
    }

    public void NextBlank()
    {
        currentBlank++;
        List<string> atls = dict.Lookup(song.blankChars[currentBlank].ToString());


    }

    public void PutWord(string word) { 
        //TODO: Logic
    }
}
