using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

    public float tumble;
    public GameObject explosion;
    public int asteroidSize = 0;
    public float maxSpeed;
    public int numberOfAsteroidDivision;

    public int asteroidID;

    GameController gameController;
    Rigidbody2D body;

    int asteroidSizes = 2;
    float[] scaleTab = new float[] { 0.3f, 0.2f, 0.1f };
    float[] speedTab = new float[] { 1, 1.5f, 2 };

    void Awake()
    {
        GetComponent<Transform>().localScale = new Vector3(scaleTab[asteroidSize], scaleTab[asteroidSize], 1);
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        body.angularVelocity = Random.Range(-100, 100) * tumble;
        body.velocity = new Vector2(body.velocity.x + Random.Range(-maxSpeed * speedTab[asteroidSize], maxSpeed * speedTab[asteroidSize]), body.velocity.y + Random.Range(-maxSpeed * speedTab[asteroidSize], maxSpeed * speedTab[asteroidSize]));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!(other.tag == "Asteroid"))
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);

            if (asteroidSize < asteroidSizes)
            {
                for (int i = 0; i < numberOfAsteroidDivision; i++)
                {
                    gameController.SpawnAsteroid(asteroidSize + 1, transform.position);
                }
            }

            gameController.IncreaseScore(asteroidSize, other.GetComponent<Identity>().playerID);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        gameController.AsteroidDestroyed(asteroidID);
    }
}
