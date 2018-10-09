using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

	public static int NumberOfPlayers = 2;
	
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
}
