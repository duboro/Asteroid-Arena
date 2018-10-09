using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		// TODO : Sound stops if shot is "destroyed"
		GetComponent<AudioSource> ().Play();
		// TODO : Add lifespan of DestroyByTimeBehaviour here
	}
}

