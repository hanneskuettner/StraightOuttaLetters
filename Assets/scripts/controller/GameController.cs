using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class GameController : MonoBehaviour {

    private WordController wordController;
    private SongtextDisplayer songTextDisplayer;
    private SongController songController;
    private UIFlow flowMeta;
    private UIScore scoreScreen;
    private Song song;
    private int currentLine;
    private int currentChar;
    private int currentBlank;

    private float score;
    private int multiplier;
    private bool timerStarted;
    private float currentTimer;

    private WordDictionary dict;

	// Use this for initialization
	void Start () {
        song = Song.Load("01-its-tricky");

        songTextDisplayer = FindObjectOfType<SongtextDisplayer>();
        songController = FindObjectOfType<SongController>();
        wordController = GetComponent<WordController>();
        flowMeta = FindObjectOfType<UIFlow>();
        scoreScreen = FindObjectOfType<UIScore>();

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
        if (!timerStarted && BlankInLine())
        {
            NextBlank();
            songTextDisplayer.SetCurrentBlank(currentChar, currentBlank);
        }
        else
        {
            songTextDisplayer.SetCurrentBlank(currentChar, -1);
        }

        if (timerStarted)
        {
            currentTimer += Time.deltaTime;

            if (currentTimer > 6f)
            {
                SkipBlanks();
            }
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
        score = 0;
        multiplier = 1;
        timerStarted = false;

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
        song.blankChars = new List<string>();
        foreach (SongLyrics.Line line in song.lyrics.lines)
        {
            foreach (SongLyrics.Lyric lyric in line.lyrics)
            {
                if (lyric.GetType() == typeof(SongLyrics.Player))
                {
                    List<int> b = new List<int>();
                    List<int> r = new List<int>();
                    int count = (int)(Random.Range(0, lyric.lyric.Length) / 3f);
                    for (int i = 0; i < count; i++)
                    {
                        r.Add(Random.Range(0, lyric.lyric.Length));
                    }
                    r.Sort();
                    foreach (int pos in r)
                    {
                        if (chars.IndexOf(lyric.lyric[pos]) != -1)
                        {
                            continue;
                        }
                        if (!b.Contains(charpos + pos))
                        {
                            b.Add(charpos + pos);
                            song.blankChars.Add(lyric.lyric[pos].ToString());
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

    private float CalculateScore(bool correct)
    {
        return 100 * multiplier;
    }

    private void AddScore(float score)
    {
        this.score += score;
        scoreScreen.SetPlayerScore((int)this.score);
    }

    public void SkipBlanks()
    {
        while (BlankInLine())
        {
            currentBlank++;
        }
        multiplier = 1;
        songController.ChangeAudioHearable();
    }

    private void IncreaseMultiplier()
    {
        multiplier *= 2;
        multiplier %= 5;
        flowMeta.IncreasePlayerFlow();
    }

    private void DecreaseMultiplier()
    {
        multiplier /= 2;
        if (multiplier == 0)
            multiplier = 1;
        flowMeta.DecreasePlayerFlow();
    }

    private void ResetMultiplier()
    {
        multiplier = 1;
        flowMeta.ResetPlayerFlow();
    }

    public void NextBlank()
    {
        currentBlank++;
        bool upper = song.blankChars[currentBlank] == song.blankChars[currentBlank].ToUpper();
        List<string> alts = new List<string>(dict.Lookup(song.blankChars[currentBlank].ToLower()));

        foreach (string w in alts)
        {
            if (upper)
                alts[alts.IndexOf(w)] = alts[alts.IndexOf(w)].ToUpper();
        }

        alts.Add(song.blankChars[currentBlank]);
        wordController.SpawnButtons(alts);

        currentTimer = 0f;
        timerStarted = true;
    }

    public void PutWord(string word) {
        if (word == song.blankChars[currentBlank])
        {
            AddScore(CalculateScore(true));
            if (currentTimer <= 2)
            {
                IncreaseMultiplier();
            }
            else if (currentTimer > 4)
            {
                DecreaseMultiplier();
            }
            songTextDisplayer.SetGreen(currentChar, currentBlank, song.blankChars[currentBlank]);
        }
        else
        {
            ResetMultiplier();
            AddScore(CalculateScore(true));
            songTextDisplayer.SetRed(currentChar, currentBlank, song.blankChars[currentBlank]);
        }

        if (!BlankInLine())
        {
            songController.ChangeAudioHearable();
        }
        timerStarted = false;
    }
}
