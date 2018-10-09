using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject Menu;
	public GameObject Camera;
	bool Paused = false;
	
	void Start(){
		Menu.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log("got esc");
			if(Paused){
				Debug.Log("paused");
				Time.timeScale = 1.0f;
				Menu.gameObject.SetActive (false);
				Paused = false;
			} else {
				Debug.Log("unpaused");
				Time.timeScale = 0.0f;
				Menu.gameObject.SetActive (true);
				Paused = true;
			}
		}
	}
}
