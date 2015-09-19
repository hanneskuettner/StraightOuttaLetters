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

    public void IncreasePlayerFlow()
    {
        this.playerFlowLevel = Mathf.Min(this.playerFlowLevel + 1, this.flowLevels.Length - 1);

        this.paintPlayer();
    }

    public void IncreaseEnemyFlow()
    {
        this.enemyFlowLevel = Mathf.Min(this.enemyFlowLevel + 1, this.flowLevels.Length - 1);

        this.paintEnemy();
    }


    public void DecreasePlayerFlow()
    {
        this.playerFlowLevel = Mathf.Max(this.playerFlowLevel - 1, 0);

        this.paintPlayer();
    }

    public void DecreaseEnemyFlow()
    {
        this.enemyFlowLevel = Mathf.Max(this.playerFlowLevel - 1, 0);

        this.paintEnemy();
    }


    public void ResetPlayerFlow() {
        this.playerFlowLevel = 0;

        this.paintPlayer();
    }

    public void ResetEnemyFlow()
    {
        this.enemyFlowLevel = 0;

        this.paintEnemy();
    }




    public int GetPlayerMultiplier()
    {
        return this.flowLevels[this.playerFlowLevel];
    }

    public int GetEnemyMultiplier()
    {
        return this.flowLevels[this.enemyFlowLevel];
    }







    private string getFlowText(int level)
    {
        return this.textPrefix + this.flowLevels[level].ToString();
    }

    private void paintPlayer()
    {
        this.PlayerFlow.text = this.getFlowText(this.playerFlowLevel);
    }

    private void paintEnemy()
    {
        this.EnemyFlow.text = this.getFlowText(this.enemyFlowLevel);
    }
}
