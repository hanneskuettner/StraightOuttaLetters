using UnityEngine;
using System.Collections;

public class ParticleSorting : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "foreground";
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 2;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
