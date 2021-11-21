using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Game Manager? I hardly know her!
    public static GameManager instance;

    // I don't know
    private float difficulty;
    private float diffTimer;
    // I don't know
    private long score;
    private Text scoreText;
    // I don't know where Imma gonna go when the volcano blows
    private int enemiesKilled;
    /* 
     * Something made to track the combos the player performs,
     * I'm thinking that when a combo of magnitude/type X is performed successfully, it's added to 
     * the Xth position in the array. Easy way to track it
    */
    private int[] combos;
    private int gold;
    private Text goldText;
    private Text levelText;
    // private GameObject backgroundSquare;

    // Public Player reference
    public GameObject Player;
    // Public NavMesh reference
    public NavMeshGenerator NavMesh;

    private SpawnHandler spawner;
    private float timeToSpawn;

    private int[] numbers;

    private void Awake()
    {
        instance = this;
    }

    // Jack and Jill are talking with each other. Jack says "I met a man with a wooden leg named Smith." Jill asks "What's the name of his other leg?"
    void Start()
    {
        instance = this;
        // scoreText = GameObject.Find("Score").GetComponent<Text>();
        // goldText = GameObject.Find("Gold").GetComponent<Text>();
        // levelText = GameObject.Find("Level").GetComponent<Text>();

        difficulty = diffTimer = 0;
        score = 0;
        gold = 0;
        // scoreText.text = "Score: " + score;
        // goldText.text = "Gold: " + gold;
        // levelText.text = "Level: " + SceneManager.GetActiveScene().name;
        enemiesKilled = 0;
        Player = GameObject.Find("player");
        NavMesh = GameObject.Find("NavMesh").GetComponent<NavMeshGenerator>();
        combos = new int[5+1];
        DontDestroyOnLoad(this);
        spawner = SpawnHandler.instance;
        timeToSpawn = 1f;
        numbers = new int[10];
    }

    public void ResetCameraToPlayer()
    {
        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.SetParent(Player.transform);
        cam.transform.position = Player.transform.position + new Vector3(0, 0, -10);
    }

    public void ChangeCameraParent(GameObject go)
    {
        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.SetParent(go.transform);
        cam.transform.position = go.transform.position + new Vector3(0, 0, -10);
    }

    void successfulCombo(int mag)
    {
        combos[mag]++;
    }

    void enemyKill(int value)
    {
        enemiesKilled++;
        score += value;
        scoreText.text = "Score: " + score;
        timeToSpawn -= 2;
    }

    void scorePoints(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    void setDifficulty(int challenge)
    {
        difficulty = challenge;
        // difficultyText.text = "Difficulty: " + difficulty;
    }

    int EnemiesKilled()
    {
        return enemiesKilled;
    }

    public float Difficulty()
    {
        return difficulty;
    }

    long Score()
    {
        return score;
    }

    public void FixedUpdate()
    {
        diffTimer += Time.fixedDeltaTime;
        if (diffTimer > 8.5)
        {
            diffTimer -= 8.5f;
            difficulty++;
        }
    }

    public void Update()
    {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn > 10 && GameObject.FindGameObjectsWithTag("Hostile").Length == 0 )
        {
            timeToSpawn -= Time.deltaTime;
        }
        if (timeToSpawn <= 0)
        {
            timeToSpawn = 10f + (20f * Mathf.Log10((difficulty + 10) / 4f));
            Debug.Log(timeToSpawn);
            // Debug.Log();
            SpawnHandler.instance.SpawnStuff(difficulty, Player);
        }
    }
}
