using UnityEngine;
using System.Collections;

public class DestroyByTimeBehaviour : MonoBehaviour {

	public float lifetime;

	void Start ()
	{
		Destroy (gameObject, lifetime);
	}
}
