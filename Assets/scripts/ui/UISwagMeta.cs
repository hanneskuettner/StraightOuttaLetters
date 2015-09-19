using UnityEngine;
using System.Collections;

public class UISwagMeta : MonoBehaviour {
    public UnityEngine.UI.Image playerImage;
    public UnityEngine.UI.Image enemyImage;

    private float playerScore = 0f;
    private float enemyScore = 0f;

    private float currentPlayerSize = 200f;
    private float targetPlayerSize = 0f;
    private float currentEnemySize
    {
        get
        {
            return 400f - this.currentPlayerSize;
        }
    }

    private float width;


    // Use this for initialization
    public void Start()
    {
        // get width of container
        width = ((RectTransform)this.transform).rect.width;
    }

    // Update is called once per frame
    public void Update()
    {


        this.updateScores();
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="val">Might be negative</param>
    public void AddPlayerScore(float val)
    {
        playerScore += val;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="val">Might be negative</param>
    public void AddEnemyScore(float val)
    {
        enemyScore += val;
    }


    private void updateScores()
    {
        float playerSize = this.width / 2f;
        float enemySize = playerSize;
        float totalScore = this.playerScore + this.enemyScore;

        if (totalScore > 0f)
        {
            this.targetPlayerSize = (this.enemyScore / totalScore) * this.width;

            float diff = this.currentPlayerSize - this.targetPlayerSize;
            //Debug.Log(this.targetPlayerSize + " " + this.playerImage.rectTransform.rect.xMax + "    diff " + diff);
            if (Mathf.Abs(diff) > 20f)
            {
                this.currentPlayerSize -= diff * 2f * Time.deltaTime;
            }
            else
            {
                if (Mathf.Abs(diff) > 1f)
                {
                    this.currentPlayerSize -= Mathf.Sign(diff) * 20f * Time.deltaTime;
                }
                else
                {
                    this.currentPlayerSize = this.targetPlayerSize;
                }
            }

            playerSize = this.currentPlayerSize;
            enemySize = this.currentEnemySize;
        }
        
        // set RIGHT
        this.playerImage.rectTransform.offsetMax = new Vector2(-playerSize, this.playerImage.rectTransform.offsetMax.y);

        // set LEFT
        this.enemyImage.rectTransform.offsetMin = new Vector2(enemySize, this.enemyImage.rectTransform.offsetMin.y);
    }
}
