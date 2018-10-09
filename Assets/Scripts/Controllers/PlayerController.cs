using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject blueShip;
    public GameObject redShip;
    public int livesNumber;

    public int score { get; set; }
    public int livesLeft { get; set; }

    private int playerID;

    void Awake()
    {
        score = 0;
        playerID = this.GetComponent<Identity>().playerID;
        livesLeft = livesNumber;
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void spawnShip(int playerNumber)
    {
        switch (playerNumber)
        {
            // Le playerID commence à 1 
            case 1:
                blueShip.GetComponent<Identity>().playerID = playerID;
                blueShip.GetComponent<ShipController>().startingPoint = new Vector2(-2, -1);
                Instantiate(blueShip, blueShip.GetComponent<ShipController>().startingPoint, Quaternion.identity);
                break;
            case 2:
                redShip.GetComponent<Identity>().playerID = playerID;
                redShip.GetComponent<ShipController>().startingPoint = new Vector2(2, 1);
                Instantiate(redShip, redShip.GetComponent<ShipController>().startingPoint, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    public void getLife()
    {
        livesLeft++;
    }

    public void loseLife()
    {
        livesLeft--;
    }
}
