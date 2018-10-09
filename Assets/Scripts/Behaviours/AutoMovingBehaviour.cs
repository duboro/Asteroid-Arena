using UnityEngine;
using System.Collections;

public class AutoMovingBehaviour : MonoBehaviour {

    public float maxSpeed;

    private Rigidbody2D body;
    private float xSpeed;
    private float ySpeed;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        body.velocity = new Vector2(body.velocity.x + Random.Range(-maxSpeed, maxSpeed), body.velocity.y + Random.Range(-maxSpeed, maxSpeed));
    }
}
