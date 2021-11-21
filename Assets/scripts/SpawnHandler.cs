using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public static SpawnHandler instance;
    int enemiesSpawned;
    public GameObject[] level1EnemyPrefabs;
    public GameObject[] level2EnemyPrefabs;
    public GameObject[] level3EnemyPrefabs;
    int enemySpawnCap;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        enemiesSpawned = 0;
        enemySpawnCap = Mathf.FloorToInt(50 + (50 * GameManager.instance.Difficulty()));
    }

    public void SpawnStuff(float diff, GameObject player)
    {
        Debug.Log("Spawn works!");
        GameObject[] platforms = GetSpawnablePlatforms(player);
        GameObject[] chosenPlatforms = ChoosePlatforms(platforms, diff);
        foreach (GameObject platform in chosenPlatforms)
        {
            if (enemiesSpawned < enemySpawnCap) 
                SpawnEnemyOnPlatform(platform, diff);
        }
    }

    private GameObject[] GetSpawnablePlatforms(GameObject player)
    {
        Collider2D[] collide = Physics2D.OverlapCircleAll(player.GetComponent<playerController>().SpawnCheck.position,  5f, LayerMask.GetMask("Spawnable Tile"));
        GameObject[] collidedObjects = new GameObject[collide.Length];
        int spawnableSize = 0;
        for (int i = 0; i < collide.Length; i++)
        {
            collidedObjects[i] = collide[i].gameObject;
            if (collidedObjects[i].GetComponent<BasicTile>().CanSpawnEnemy())
            {
                spawnableSize++;
            }
        }
        GameObject[] finalCollidedObjectList = new GameObject[spawnableSize];
        int j = 0;
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if (collidedObjects[i].GetComponent<BasicTile>().CanSpawnEnemy())
            {
                finalCollidedObjectList[j] = collidedObjects[i];
                j++;
            }
        }
        return finalCollidedObjectList;
    }

    private GameObject[] ChoosePlatforms(GameObject[] platforms, float diff)
    {
        // Do once spawn area and ideal level map is made
        return platforms;
    }

    private void SpawnEnemyOnPlatform(GameObject platform, float diff)
    {
        GameObject enemy;
        if (UnityEngine.Random.Range(0f, 1f) < 0.4f + .6 / diff)
            enemy = GetEnemy(diff);
        else
            enemy = GetEnemy(diff, false);
        Vector2 spawnpos = platform.transform.position;
        enemy.GetComponent<EnemyDefault>().Spawn(spawnpos, diff);
        enemiesSpawned++;

        if (UnityEngine.Random.Range(0f, 1f) > 0.4f + .6/diff && enemiesSpawned < enemySpawnCap)
        {
            enemy = GetEnemy(diff, true);
            spawnpos = platform.transform.position;
            enemy.GetComponent<EnemyDefault>().Spawn(spawnpos, diff);
            enemiesSpawned++;
        }
    }

    private GameObject GetEnemy(float diff)
    {
        return Instantiate(level1EnemyPrefabs[UnityEngine.Random.Range(0, level1EnemyPrefabs.Length)]);
    }

    private GameObject GetEnemy(float diff, bool makeHard)
    {
        if (makeHard)
            return Instantiate(level1EnemyPrefabs[UnityEngine.Random.Range(Mathf.FloorToInt(diff * (level1EnemyPrefabs.Length * 0.2f)), level1EnemyPrefabs.Length)]);
        else
            return Instantiate(level1EnemyPrefabs[UnityEngine.Random.Range(0, level1EnemyPrefabs.Length - (Mathf.FloorToInt(diff * (level1EnemyPrefabs.Length * 0.2f))))]);
    }
}
