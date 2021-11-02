using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game Manager? I hardly know her!
    public static GameManager instance;

    // I don't know
    private int difficulty;
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

    // Jack and Jill are talking with each other. Jack says "I met a man with a wooden leg named Smith." Jill asks "What's the name of his other leg?"
    void Start()
    {
        instance = this;
        difficulty = 0;
        score = 0;
        enemiesKilled = 0;
        Player = GameObject.Find("Player");
        NavMesh = GameObject.Find("NavMesh").GetComponent<NavMeshGenerator>();
        combos = new int[5+1];
    }

    void successfulCombo(int mag)
    {
        combos[mag]++;
    }

    void enemyKill(int value)
    {
        enemiesKilled++;
        score += value;
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

    int Difficulty()
    {
        return difficulty;
    }

    long Score()
    {
        return score;
    }
}
