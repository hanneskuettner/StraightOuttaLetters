using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class SongtextDisplayer : MonoBehaviour {

    public Text playerText;
    public Text playerGrayText;
    public Text opponentText;
    public Text opponentGrayText;
    public Image arrow;

    private SongTimings timings;
    private List<SongLyrics.Line> lines;
    private int currentLine;
    private int currentLineWordCount;
    private List<int> blanks;
    private int characterSize = 16;

    public void Start () {
        currentLine = 0;
    }
    private int CountWords(string s)
    {
        MatchCollection collection = Regex.Matches(s, @"[\S]+");
        return collection.Count;
    }


    public void SetLines(List<SongLyrics.Line> lines) {
        this.lines = lines;
        playerText.text = "";
        playerGrayText.text = "";
        opponentText.text = "";
        opponentGrayText.text = "";

        foreach (SongLyrics.Line line in lines)
        {
            string playerString = "";
            string opponentString = "";
            string lineString = "";
            foreach (SongLyrics.Lyric lyric in line.lyrics)
            {
                if (lyric.GetType() == typeof(SongLyrics.Player))
                {
                    playerString += lyric.lyric + " ";
                    opponentString += new String(' ', lyric.lyric.Length + 1);
                }
                else
                {
                    opponentString += lyric.lyric + " ";
                    playerString += new String(' ', lyric.lyric.Length + 1);
                }
                lineString += lyric.lyric + " ";
            }
            playerText.text += playerString + "\n";
            playerGrayText.text += opponentString + "\n";
            opponentText.text += opponentString + "\n";
            opponentGrayText.text += playerString + "\n";
        }
    }

    public void SetBlankPositions(List<int> blanks)
    {
        this.blanks = blanks;
    }

    public void SetTimings(SongTimings timings)
    {
        this.timings = timings;
    }

    public void SetCurrentBlank(int currentChar, int currentBlank)
    {
        if (currentBlank == -1)
        {
            arrow.gameObject.SetActive(false);
            return;
        }
        arrow.gameObject.SetActive(true);
        arrow.rectTransform.localPosition = new Vector3((blanks[currentBlank] - currentChar) * characterSize + characterSize - 312.5f, arrow.rectTransform.localPosition.y);
    }
}
