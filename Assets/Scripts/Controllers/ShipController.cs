using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
	Animator animator;
	Rigidbody2D body;
	public float torqueForce;
	public float maxAngularVelocity;
	public float accelerationForce;
	public float terminalVelocity;
	public GameObject shot;
	public Transform weapon;
    public GameObject explosion;

	public string up;
	public string left;
	public string right;
	public string fire;

	public float fireRate = 0.5f;
	private float nextFire = 0.0f;
	private bool hasThrusted;
    private int playerID;
    GameController gameController;

	public Vector2 startingPoint;

    void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        playerID = this.GetComponent<Identity>().playerID;
    }

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		body = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		// TODO : Animation not running correctly
		if (Input.GetKey (KeyCode.UpArrow)) {
			animator.SetBool("isThrottling", true);
		} else {
			animator.SetBool("isThrottling", false);
		}
	}

	private void Respawn(){
		Instantiate(Resources.Load("BlueShip", typeof(GameObject)), startingPoint, Quaternion.identity); 
	}

	void FireShot(){
        shot.GetComponent<Identity>().playerID = playerID;
		GameObject shotFired = Instantiate (shot, weapon.position, weapon.rotation) as GameObject;
		shotFired.GetComponent<Rigidbody2D>().AddForce (new Vector2 (150 * Mathf.Cos (Mathf.Deg2Rad * (body.rotation + 90)), 150 * Mathf.Sin (Mathf.Deg2Rad * (body.rotation + 90))));
		// Add velocity of ship to bullet
		shotFired.GetComponent<Rigidbody2D> ().velocity += body.velocity;

	}
	
	// Update is called once per frame
	void Update ()
	{
		// TODO : Whenever going on one direction should take "time" to go the other way around?
		if (Input.GetButton (left)) {
			body.angularVelocity = torqueForce;
			hasThrusted = true;
		} else if (Input.GetButton (right)) {
			body.angularVelocity = -torqueForce;
			hasThrusted = true;
		} else if (hasThrusted) {
			body.angularVelocity = 0;
			hasThrusted = false;
		}

		if (body.velocity.magnitude < terminalVelocity && Input.GetButton (up)) {
			body.AddForce (new Vector2 (accelerationForce * Mathf.Cos (Mathf.Deg2Rad * (body.rotation + 90)), accelerationForce * Mathf.Sin (Mathf.Deg2Rad * (body.rotation + 90))));
		}

		if (Input.GetButton(fire) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			FireShot ();
		}

	}

    void OnTriggerEnter2D(Collider2D other)
    {

        gameController.ShipDestroyed(playerID);

        Instantiate(explosion, transform.position, explosion.transform.rotation);

        if (other.tag != "Asteroid")
        {
            gameController.IncreaseScore(3, other.GetComponent<Identity>().playerID);
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }

	void OnDestroy () 
    {
        
	}
}
