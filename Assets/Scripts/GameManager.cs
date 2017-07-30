using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    private GameObject percentGauge;

    [SerializeField]
    private GameObject gameMenu;

    [SerializeField]
    private float currentSpeed;

    private GameObject player;
    private int playerLoopIndex;
    private float powerRemaining;
    private Vector3 percentGaugeScaleOriginal;
    private SpriteRenderer percentGaugeSpriteRenderer;

    private GameObject[] electricities;

    public GameObject Player { get { return player; } set { player = value; } }
    public GameObject GameMenu { get { return gameMenu; } set { gameMenu = value; } }
    public int PlayerLoopIndex { get { return playerLoopIndex; } set { playerLoopIndex = value; } }
    public float CurrentSpeed { get { return currentSpeed; } set { currentSpeed = Mathf.Min(40.0f, value); } }
    public float PowerRemaining {
        get { return powerRemaining; }
        set {
            Vector3 scale = percentGauge.transform.localScale;
            powerRemaining = value;
            scale.x = (PowerRemaining);
            percentGauge.transform.localScale = scale; 
            if (powerRemaining <= 0) {
                Debug.Log("GAME OVER MAN");
                Time.timeScale = 0;
                GameMenu.SetActive(true);
                //GameRestart();
            }
        }
    }

    // Use this for initialization
    void Start () {
        PlayerLoopIndex = 2;
        PowerRemaining = 1.0f;
        percentGaugeScaleOriginal = percentGauge.transform.localScale;
        percentGaugeSpriteRenderer = percentGauge.GetComponent<SpriteRenderer>();

        electricities = GameObject.FindGameObjectsWithTag("ELECTRICITY");
        RandomlyTurnOnSparks(1);
    }

    // Update is called once per frame
    void Update () {
        GameEscape();
    }


    public bool RandomizeBool() {
        return (Random.value >= 0.5);
    }


    public void RandomlyTurnOnSparks(int count) {
        GameExtensions.Fisher_Yates_CardDeck_Shuffle(electricities);
        for (int i = 0; i < count; i++) {
            Electricity electricity = electricities[i].GetComponent<Electricity>();
            electricity.ShowSpark = true;
        }
    }

    private void GameEscape() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = Time.timeScale == 1.0f ? 0 : 1.0f;
        }
    }

    public void GameRestart() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameQuit() {
        Application.Quit();
    }
}

static class GameExtensions {
    public static GameObject[] Fisher_Yates_CardDeck_Shuffle(GameObject[] aList) {

        System.Random _random = new System.Random();

        GameObject myGO;

        int n = aList.Length;
        for (int i = 0; i < n; i++) {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }
}