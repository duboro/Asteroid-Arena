using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public GameObject PlayerChoices;
	public int loadingScene;

	//public GameObject loadingImage;
	public void Start(){
		PlayerChoices.gameObject.SetActive (false);
	}

	public void ShowPlayerChoices(Canvas canvas){
		canvas.gameObject.SetActive (true);
	}
	
	public void LoadScene(int nbPlayers){
		//loadingImage.SetActive(true);
		GameSettings.NumberOfPlayers = nbPlayers;
        SceneManager.LoadScene(loadingScene);
    }
}