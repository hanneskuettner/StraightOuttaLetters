using UnityEngine;
using System.Collections;
using System;

public class UIFlow : MonoBehaviour {

    public UnityEngine.UI.Text PlayerFlow;
    public UnityEngine.UI.Text EnemyFlow;

    private string textPrefix = "x";

    private int[] flowLevels = new int[] { 1, 2, 4 };

    private int playerFlowLevel = 0;
    private int enemyFlowLevel = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void increasePlayerFlow()
    {
        this.playerFlowLevel = Mathf.Min(this.playerFlowLevel + 1, this.flowLevels.Length - 1);

        this.PlayerFlow.text = this.getFlowText(this.playerFlowLevel);
    }

    public void increaseEnemyFlow()
    {
        this.enemyFlowLevel = Mathf.Min(this.enemyFlowLevel + 1, this.flowLevels.Length - 1);

        this.EnemyFlow.text = this.getFlowText(this.enemyFlowLevel);
    }

    public void resetPlayerFlow() {
        this.playerFlowLevel = 0;
    }

    public void resetEnemyFlow()
    {
        this.enemyFlowLevel = 0;
    }

    public int getPlayerMultiplier()
    {
        return this.flowLevels[this.playerFlowLevel];
    }

    public int getEnemyMultiplier()
    {
        return this.flowLevels[this.enemyFlowLevel];
    }

    private string getFlowText(int level)
    {
        return this.textPrefix + this.flowLevels[level].ToString();
    }
}
