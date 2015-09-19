using UnityEngine;
using System.Collections;

public class UIScore : MonoBehaviour {
    public UnityEngine.UI.Text PlayerScore;
    public UnityEngine.UI.Text EnemyScore;

    public void SetPlayerScore(int score) {
        this.PlayerScore.text = score.ToString();
    }

    public void SetEnemyScore(int score) {
        this.EnemyScore.text = score.ToString();
    }
}
