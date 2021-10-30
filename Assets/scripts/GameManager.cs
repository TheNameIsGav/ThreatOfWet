using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int difficulty;
    private long score;
    private int enemiesKilled;

    // Start is called before the first frame update
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
