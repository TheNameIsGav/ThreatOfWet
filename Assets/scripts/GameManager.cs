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
    private int difficulty;
    private Text difficultyText;
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

    // Public Player reference
    public GameObject Player;
    // Public NavMesh reference
    public NavMeshGenerator NavMesh;

    // Jack and Jill are talking with each other. Jack says "I met a man with a wooden leg named Smith." Jill asks "What's the name of his other leg?"
    void Start()
    {
        instance = this;
        difficultyText = GameObject.Find("Difficulty").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        goldText = GameObject.Find("Gold").GetComponent<Text>();
        levelText = GameObject.Find("Level").GetComponent<Text>();

        difficulty = 1;
        score = 0;
        gold = 0;
        difficultyText.text = "Difficulty: " + difficulty;
        scoreText.text = "Score: " + score;
        goldText.text = "Gold: " + gold;
        levelText.text = "Level: " + SceneManager.GetActiveScene().name;
        enemiesKilled = 0;
        Player = GameObject.Find("player");
        NavMesh = GameObject.Find("NavMesh").GetComponent<NavMeshGenerator>();
        combos = new int[5+1];
        DontDestroyOnLoad(this);
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
    }

    void scorePoints(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    void setDifficulty(int challenge)
    {
        difficulty = challenge;
        difficultyText.text = "Difficulty: " + difficulty;
    }

    int EnemiesKilled()
    {
        return enemiesKilled;
    }

    int Difficulty()
    {
        return difficulty;
    }

    long Score()
    {
        return score;
    }
}
