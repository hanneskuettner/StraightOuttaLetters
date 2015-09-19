using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WordController : MonoBehaviour {

    private GameController gameController;

    public GameObject WordSelectorPrefab;
    private GameObject ButtonLayout;
    private List<GameObject> buttons = new List<GameObject>();

    public void Validate(string word) {
        gameController.PutWord(word);
        NukeButtons();
    }

    public void SpawnButtons(List<string> words) {
        Shuffle(ref words);
        foreach (string s in words)
        {
            GameObject g = Instantiate(WordSelectorPrefab) as GameObject;
            g.GetComponentInChildren<Text>().text = s;
            g.transform.SetParent(ButtonLayout.transform);
            g.GetComponent<Button>().onClick.AddListener(delegate { Validate(g.GetComponentInChildren<Text>().text); });
            buttons.Add(g);
        }
    }

    private void Shuffle(ref List<string> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            string value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void NukeButtons() {
        foreach (GameObject g in buttons)
        {
            Destroy(g);            
        }
        buttons.Clear();
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
