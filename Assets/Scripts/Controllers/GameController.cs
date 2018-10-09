using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public GameObject asteroid;
	public GameObject ship1;
	public GameObject ship2;
    public GameObject player;
    public int numberOfWaves;
    public float timeBetweenAsteroidSpawn;
    public float timeBetweenWaves;
    public float worldWidth;
	public float worldHeight;
	public static bool gameOver = false;

    private int uniqueKey;
    private float twilightZone;
    private bool spawnInProgress;
    private int currentWave;
    private DisplayerController displayerController;
    private Dictionary<int, Object> Hostiles;
    private Dictionary<int, GameObject> Players;

    void Awake()
    {
        displayerController = GameObject.FindWithTag("Displayer").GetComponent<DisplayerController>();

        worldHeight = Camera.main.orthographicSize;
        worldWidth = worldHeight * Camera.main.aspect;

        twilightZone = 0.5f;
        spawnInProgress = false;
        currentWave = 0;
        uniqueKey = 0;

        Hostiles = new Dictionary<int, Object>();

        Players = new Dictionary<int,GameObject>();
        for (int i = 1; i <= GameSettings.NumberOfPlayers; i++)
        {
            player.GetComponent<Identity>().playerID = i;
            Players.Add(i,Instantiate(player));
        }
    }

    void Start()
    {
        int playerNumber = 1;

        foreach (KeyValuePair<int, GameObject> Player in Players)
        {
            PlayerController playerController = Player.Value.GetComponent<PlayerController>();
            playerController.spawnShip(playerNumber);
            displayerController.DisplayLivesNumber(playerNumber, playerController.livesLeft);
            playerNumber ++;
        }

        displayerController.DisplayText(Hostiles.Count.ToString(), DisplayerController.ASTEROIDNUMBERVALUE);
    }

    void Update()
    {
        if (Hostiles.Count == 0 && !spawnInProgress)
        {
            if (currentWave < numberOfWaves)
            {
                currentWave = currentWave + 1;
                StartCoroutine(SpawnWave(ComputeNumberAsteroidToSpawnThisWave(currentWave)));
            }
            else
            {
                Debug.Log("fini !");
            }
        }  
    }

    int ComputeNumberAsteroidToSpawnThisWave(int waveNumber)
    {
        int numberAsteroidToSpawnThisWave = 0;

        numberAsteroidToSpawnThisWave = waveNumber;

        return numberAsteroidToSpawnThisWave;
    }

    IEnumerator SpawnWave(int asteroidsInWave)
    {
        spawnInProgress = true;

        displayerController.DisplayWaveNumber(currentWave);
        yield return new WaitForSeconds(timeBetweenWaves);
        displayerController.HideWaveNumber();

        for (int i = 0; i < asteroidsInWave; i++)
        {
            SpawnAsteroid(0, SpawnVector());

            yield return new WaitForSeconds(timeBetweenAsteroidSpawn);
        }

        spawnInProgress = false;
    }

    public void SpawnAsteroid(int asteroidSize, Vector2 spawnPoint)
    {
        uniqueKey ++;
        AsteroidController asteroidController = asteroid.GetComponent<AsteroidController>();

        asteroidController.asteroidSize = asteroidSize;
        asteroidController.asteroidID = uniqueKey;
        Quaternion quaternion = Quaternion.identity;
        Hostiles.Add(uniqueKey, Instantiate(asteroid, spawnPoint, quaternion));

        displayerController.DisplayText(Hostiles.Count.ToString(), DisplayerController.ASTEROIDNUMBERVALUE);
    }

    Vector2 SpawnVector()
    {
        Vector2 spawnVector = new Vector2();

        int chosenSpawnFace = Random.Range(0, 4);
        float xAxisSpawn = 0;
        float yAxisSpawn = 0;

        switch (chosenSpawnFace)
        {
                // haut
            case 0:
                xAxisSpawn = Random.Range(- worldWidth - twilightZone, worldWidth);
                yAxisSpawn = Random.Range(worldHeight, worldHeight + twilightZone);
                break;
                // gauche
            case 1:
                xAxisSpawn = Random.Range(- worldWidth - twilightZone, - worldWidth);
                yAxisSpawn = Random.Range(- worldHeight - twilightZone, worldHeight);
                break;
                // bas
            case 2:
                xAxisSpawn = Random.Range(-worldWidth, worldWidth + twilightZone);
                yAxisSpawn = Random.Range(- worldHeight - twilightZone, - worldHeight);
                break;
                // droite
            case 3:
                xAxisSpawn = Random.Range(worldWidth, worldWidth + twilightZone);
                yAxisSpawn = Random.Range(- worldHeight, worldHeight + twilightZone);
                break;
            default:
                break;
        }

        spawnVector.x = xAxisSpawn;
        spawnVector.y = yAxisSpawn;

        return spawnVector;
    }

    private IEnumerator Wait()
    {
        Debug.Log("start waiting");
        yield return new WaitForSeconds(3);
        Debug.Log("waiting");
    }

    public void QuitGame()
    {
		Debug.Log ("exit");
		Application.Quit();
	}

    public void IncreaseScore(int destroyedObject, int playerID)
    {
        int scoreToAdd = 0;

        switch (destroyedObject)
        {
            // gros astéroid
            case 0:
                scoreToAdd = 40;
                break;
            // moyen astéroid
            case 1:
                scoreToAdd = 60;
                break;
            // petit astéroid
            case 2:
                scoreToAdd = 80;
                break;
            // joueur adverse
            case 3:
                scoreToAdd = 200;
                break;
        }

        Players[playerID].GetComponent<PlayerController>().score += scoreToAdd;

        displayerController.DisplayText(Players[playerID].GetComponent<PlayerController>().score.ToString(), playerID);
    }

    public void AsteroidDestroyed(int asteroidID)
    {
        Hostiles.Remove(asteroidID);
        displayerController.DisplayText(Hostiles.Count.ToString(), DisplayerController.ASTEROIDNUMBERVALUE);
    }

    public void ShipDestroyed(int playerID)
    {
        PlayerController playerController = Players[playerID].GetComponent<PlayerController>();
        playerController.loseLife();

        if (playerController.livesLeft > 0)
        {
            playerController.spawnShip(playerID);
        }

        displayerController.DisplayLivesNumber(playerID, playerController.livesLeft);
    }

	public void OnApplicationQuit(){
		gameOver = true;
	}    
}
