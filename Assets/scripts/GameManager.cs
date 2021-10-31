using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int difficulty;
    private long score;
    private int enemiesKilled;

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
