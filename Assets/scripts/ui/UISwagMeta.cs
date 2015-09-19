using UnityEngine;
using System.Collections;

public class UISwagMeta : MonoBehaviour {
    public UnityEngine.UI.Image playerImage;
    public UnityEngine.UI.Image enemyImage;

    private float playerScore = 0f;
    private float playerScoreToAdd = 0f;
    private float enemyScore = 0f;
    private float enemyScoreToAdd = 0f;

    private float width;
    private float tick = 0f;

    private float deleteme = 2f;


    // Use this for initialization
    public void Start()
    {
        // get width of container
        width = ((RectTransform)this.transform).rect.width;

        //this.playerImage.rectTransform.offsetMax = new Vector2(-300f , this.playerImage.rectTransform.offsetMax.y);
        //this.enemyImage.rectTransform.offsetMin = new Vector2(100f, this.enemyImage.rectTransform.offsetMin.y);
    }

    // Update is called once per frame
    public void Update()
    {
        this.deleteme -= Time.deltaTime;
        if (this.deleteme < 0f)
        {
            this.deleteme = 2f;
            if (Random.Range(0,1) == 0)
            {
                this.AddPlayerScore(Random.Range(10f, 200f));
            }
            else
            {
                this.AddEnemyScore(Random.Range(10f, 200f));
            }
        }




        this.tick -= Time.deltaTime;
        if (this.tick <= 0f)
        {
            this.tick = 0.2f;
            this.updatePlayerScore();
            this.updateEnemyScore();

            //this.updateScores();
        }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="val">Might be negative</param>
    public void AddPlayerScore(float val)
    {
        playerScoreToAdd += val;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="val">Might be negative</param>
    public void AddEnemyScore(float val)
    {
        enemyScoreToAdd += val;
    }


    private void updateScores()
    {
        float playerSize = this.width / 2f;
        float enemySize = playerSize;
        float totalScore = this.playerScore + this.enemyScore;

        if (totalScore > 0f)
        {
            playerSize = -400f + (this.playerScore / totalScore) * this.width;
            enemySize = (this.enemyScore / totalScore) * this.width;
        }

        // set RIGHT
        this.playerImage.rectTransform.offsetMax = new Vector2(playerSize, this.playerImage.rectTransform.offsetMax.y);

        // set left
        this.enemyImage.rectTransform.offsetMin = new Vector2(enemySize, this.enemyImage.rectTransform.offsetMin.y);
    }

    private void updatePlayerScore()
    {
        if (this.playerScoreToAdd != 0f)
        {
            float val;
            if (this.playerScoreToAdd > 20f)
            {
                val = this.playerScoreToAdd * 0.5f;
            }
            else
            {
                val = 5f;
            }

            this.playerScoreToAdd -= val;
            this.playerScore += val;
        }
    }

    private void updateEnemyScore()
    {
        if (this.enemyScoreToAdd != 0f)
        {
        }
    }
}
