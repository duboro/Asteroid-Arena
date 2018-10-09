using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplayerController : MonoBehaviour {

    public static string SCORE_P1 = "ScoreValueP1";
    public static string SCORE_LABEL_P1 = "ScoreLabelP1";
    public static string SCORE_P2 = "ScoreValueP2";
    public static string SCORE_LABEL_P2 = "ScoreLabelP2";
    public static string LIVES_NUMBER_P1 = "LivesNumberP1";
    public static string LIVES_NUMBER_P2 = "LivesNumberP2";
    public static string ASTEROIDNUMBERVALUE = "AsteroidNumberValue";

    private string racineScoreValue = "ScoreValueP";
    private string racineScoreLabel = "ScoreLabelP";
    private string racineLivesNumber = "LivesNumberP";
    private string spriteNumberName = "numbers";
    private string waveLabel = "WaveLabel";
    private string waveValueHundred = "WaveValueHundred";
    private string waveValueDecade = "WaveValueDecade";
    private string waveValueUnit = "WaveValueUnit";
    private Sprite[] waveNumberSprites;
    private Dictionary<string, Text> displayZones;
    private Dictionary<string, Image> displayImages;

    void Awake()
    {
        displayZones = new Dictionary<string, Text>();
        displayZones.Add(SCORE_P1, GameObject.FindWithTag(SCORE_P1).GetComponent<Text>());
        displayZones.Add(SCORE_LABEL_P1, GameObject.FindWithTag(SCORE_LABEL_P1).GetComponent<Text>());
        displayZones.Add(SCORE_P2, GameObject.FindWithTag(SCORE_P2).GetComponent<Text>());
        displayZones.Add(SCORE_LABEL_P2, GameObject.FindWithTag(SCORE_LABEL_P2).GetComponent<Text>());
        displayZones.Add(LIVES_NUMBER_P1, GameObject.FindWithTag(LIVES_NUMBER_P1).GetComponent<Text>());
        displayZones.Add(LIVES_NUMBER_P2, GameObject.FindWithTag(LIVES_NUMBER_P2).GetComponent<Text>());
        displayZones.Add(ASTEROIDNUMBERVALUE, GameObject.FindWithTag(ASTEROIDNUMBERVALUE).GetComponent<Text>());

        waveNumberSprites = Resources.LoadAll<Sprite>(spriteNumberName);

        displayImages = new Dictionary<string, Image>();
        displayImages.Add(waveLabel, GameObject.FindWithTag(waveLabel).GetComponent<Image>());
        displayImages.Add(waveValueHundred, GameObject.FindWithTag(waveValueHundred).GetComponent<Image>());
        displayImages.Add(waveValueDecade, GameObject.FindWithTag(waveValueDecade).GetComponent<Image>());
        displayImages.Add(waveValueUnit, GameObject.FindWithTag(waveValueUnit).GetComponent<Image>());
    }

	// Use this for initialization
	void Start () 
    {
        InitializePlayersDisplays();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayText (string textToDisplay, string displayZone)
    {
        displayZones[displayZone].text = textToDisplay;
    }

    public void DisplayText(string textToDisplay, int playerID)
    {
        displayZones[racineScoreValue + playerID].text = textToDisplay;
    }

    public void DisplayWaveNumber(int waveNumber)
    {
        SetWaveValue(waveNumber);

        displayImages[waveLabel].enabled = true;
        displayImages[waveValueHundred].enabled = true;
        displayImages[waveValueDecade].enabled = true;
        displayImages[waveValueUnit].enabled = true;
    }

    public void HideWaveNumber()
    {
        displayImages[waveLabel].enabled = false;
        displayImages[waveValueHundred].enabled = false;
        displayImages[waveValueDecade].enabled = false;
        displayImages[waveValueUnit].enabled = false;
    }

    void SetWaveValue(int waveNumber)
    {
        int[] tabWaveDigits = new int[3];
        int indice = 0;

        while (indice < 3)
        {
            tabWaveDigits[indice] = waveNumber % 10;
            waveNumber = waveNumber / 10;

            indice = indice + 1;
        }

        displayImages[waveValueUnit].sprite = waveNumberSprites[tabWaveDigits[0]];
        displayImages[waveValueDecade].sprite = waveNumberSprites[tabWaveDigits[1]];
        displayImages[waveValueHundred].sprite = waveNumberSprites[tabWaveDigits[2]];
    }

    void InitializePlayersDisplays()
    {
        for (int i = 1; i <= GameSettings.NumberOfPlayers; i++)
        {
            displayZones[racineScoreLabel + i].enabled = true;
            displayZones[racineScoreValue + i].enabled = true;
            displayZones[racineLivesNumber + i].enabled = true;
        }
    }

    public void DisplayLivesNumber(int playerID, int livesNumber)
    {
        string lives = "";

        for (int i = 0; i < livesNumber; i++)
        {
            lives = lives + "♥";
        }

        if (livesNumber <= 0)
        {
            lives = "PERDU !";
        }
        

        this.DisplayText(lives, racineLivesNumber + playerID);
    }
}
