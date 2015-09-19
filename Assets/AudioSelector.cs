using UnityEngine;
using System.Collections;

public class AudioSelector : MonoBehaviour {

    public int Level = 0;

    

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponentInParent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
