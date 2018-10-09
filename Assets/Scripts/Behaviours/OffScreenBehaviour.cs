using UnityEngine;
using System.Collections;

public class OffScreenBehaviour : MonoBehaviour {
	
	public float twilightZone;
	Rigidbody2D body;
    GameController gameController;
    //int i = 0;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		
		/*worldHeight = Camera.main.orthographicSize;
		worldWidth = worldHeight * Camera.main.aspect;*/
	}

	// Update is called once per frame
	void Update () {

        if (body.position.y < -gameController.worldHeight - twilightZone)
        {
            body.position = new Vector2(body.position.x, gameController.worldHeight);
        }
        else if (body.position.y > gameController.worldHeight + twilightZone)
        {
            body.position = new Vector2(body.position.x, -gameController.worldHeight);
		}
        if (body.position.x < -gameController.worldWidth - twilightZone)
        {
            body.position = new Vector2(gameController.worldWidth, body.position.y);
        }
        else if (body.position.x > gameController.worldWidth + twilightZone)
        {
            body.position = new Vector2(-gameController.worldWidth, body.position.y);
		}

	
	}
}
