using UnityEngine;
using System.Collections;

public class WordController : MonoBehaviour {

    private GameController gameController;

    public void Validate(string word) {
        gameController.PutWord(word);
    }    

	// Use this for initialization
	void Start () {
        gameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
