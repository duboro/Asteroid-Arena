using UnityEngine;
using System.Collections;

public class CollisionDestructionBehaviour : MonoBehaviour {

    public GameObject explosion;

    void OnTriggerEnter2D(Collider2D other)
    {
		// TODO : Remove explosion once finished
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    void Update()
    {
    }
	
}