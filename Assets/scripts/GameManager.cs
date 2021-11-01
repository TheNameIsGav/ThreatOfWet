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

    // Public Player reference
    public playerControler Player;
    // Public NavMesh reference
    //public NavMesh NavMesh;

    // Jack and Jill are talking with each other. Jack says "I met a man with a wooden leg named Smith." Jill asks "What's the name of his other leg?"
    void Start()
    {
        instance = this;
        difficulty = 0;
        score = 0;
        enemiesKilled = 0;
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
