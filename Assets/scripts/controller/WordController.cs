using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WordController : MonoBehaviour {

    private GameController gameController;

    public GameObject WordSelectorPrefab;
    public GameObject ButtonLayout;

    public void Validate(string word) {
        gameController.PutWord(word);
    }

    public void SpawnButtons(List<string> words) {
        foreach (string s in words)
        {
            GameObject g = Instantiate(WordSelectorPrefab) as GameObject;
            g.GetComponentInChildren<Text>().text = s;
            g.transform.SetParent(ButtonLayout.transform);
            g.GetComponent<Button>().onClick.AddListener(delegate { Validate(g.GetComponentInChildren<Text>().text); });
        }
    }

	// Use this for initialization
	void Start () {
        gameController = GameObject.FindObjectOfType<GameController>();
        ButtonLayout = GameObject.FindGameObjectWithTag("ButtonLayout");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
