using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game Manager? I hardly know her!
    public static GameManager instance;

    // I don't know
    private float difficulty;
    // I don't know
    private long score;
    // I don't know where Imma gonna go when the volcano blows
    private int enemiesKilled;
    /* 
     * Something made to track the combos the player performs,
     * I'm thinking that when a combo of magnitude/type X is performed successfully, it's added to 
     * the Xth position in the array. Easy way to track it
    */
    private int[] combos;

    // Public Player reference
    public GameObject Player;
    // Public NavMesh reference
    public NavMeshGenerator NavMesh;

    private SpawnHandler spawner;
    private float timeToSpawn;

    private int[] numbers;

    // Jack and Jill are talking with each other. Jack says "I met a man with a wooden leg named Smith." Jill asks "What's the name of his other leg?"
    void Start()
    {
        instance = this;
        difficulty = 1;
        score = 0;
        enemiesKilled = 0;
        Player = GameObject.Find("player");
        NavMesh = GameObject.Find("NavMesh").GetComponent<NavMeshGenerator>();
        combos = new int[5+1];
        DontDestroyOnLoad(this);
        spawner = SpawnHandler.instance;
        timeToSpawn = .1f;
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
        timeToSpawn -= 2;
    }

    void scorePoints(int value)
    {
        score += value;
    }

    void setDifficulty(int challenge)
    {
        difficulty = challenge;
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

    public void Update()
    {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0)
        {
            timeToSpawn = 1f + (Random.Range(0f,3f) / difficulty);
            Debug.Log(timeToSpawn);
            spawner.SpawnStuff(difficulty, Player);
        }
    }
}
