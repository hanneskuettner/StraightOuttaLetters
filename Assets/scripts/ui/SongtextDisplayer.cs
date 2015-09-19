using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class SongtextDisplayer : MonoBehaviour {

    public Text playerText;
    public Text playerGrayText;
    public Text opponentText;
    public Text opponentGrayText;

    public void Start () {
    
    }

    public void SetLines(List<SongLyrics.Line> lines) {
        playerText.text = "";
        playerGrayText.text = "";
        opponentText.text = "";
        opponentGrayText.text = "";
        foreach (SongLyrics.Line line in lines)
        {
            string playerString = "";
            string opponentString = "";
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
            }
            playerText.text += playerString + "\n";
            playerGrayText.text += opponentString + "\n";
            opponentText.text += opponentString + "\n";
            opponentGrayText.text += playerString + "\n";
        }
    }
}
