using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public static SpawnHandler instance;
    int enemiesSpawned;
    public GameObject[] level1EnemyPrefabs;
    // public GameObject[] level2EnemyPrefabs;
    // public GameObject[] level3EnemyPrefabs;
    public float spawnRadius = 5;
    int maxEnemySpawn;

    void Start()
    {
        enemiesSpawned = 0;
        instance = this;
    }

    public void SpawnStuff(float diff, GameObject player)
    {
        // Debug.Log("Spawn Called!");
        GameObject[] platforms = GetSpawnablePlatforms(player);
        // Debug.Log("Spawnable Platforms Found! (" + platforms.Length + ")");
        GameObject[] chosenPlatforms = ChoosePlatforms(platforms, diff);
		// Debug.Log("Spawn Platforms Chosen! (" + chosenPlatforms.Length + ")");
          foreach (GameObject platform in chosenPlatforms)
        {
            SpawnEnemyOnPlatform(platform, diff);
        }
    
        // Debug.Log("Spawn Complete!");
    }

    private GameObject[] GetSpawnablePlatforms(GameObject player)
    {
        // Get All Local Platforms
        Collider2D[] collide = Physics2D.OverlapCircleAll(
            player.transform.position + new Vector3(spawnRadius + 2, 0, 0), spawnRadius, LayerMask.GetMask("SpawnableTile"));
        GameObject[] collidedObjects = new GameObject[collide.Length];
        if (collide.Length < 1)
            return collidedObjects;

        int spawnableSize = 0;
        for (int i = 0; i < collide.Length; i++)
        {
            collidedObjects[i] = collide[i].gameObject;
            if (collidedObjects[i].GetComponent<BasicTile>() != null 
                && collidedObjects[i].GetComponent<BasicTile>().CanSpawnEnemy())
            {
                spawnableSize++;
            } 
        }

        // Get All Spawnable Platforms, Trim for OverSpawning
        GameObject[] trimmedCollidedObjectList = new GameObject[spawnableSize];
        int j = 0;
        int finalSize = 0;
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if (collidedObjects[i].GetComponent<BasicTile>() != null
                && collidedObjects[i].GetComponent<BasicTile>().CanSpawnEnemy()
                && GameManager.instance.NavMesh.GetComponent<NavMeshGenerator>().
                    FindNearestMovePoint(collidedObjects[i]).GetComponent<MovePoint>().IsUnoccupied())
            {
                trimmedCollidedObjectList[j] = collidedObjects[i];
                // Debug.Log(finalCollidedObjectList[j].name);
                j++;
                finalSize++;
            } else
            {
                if (!GameManager.instance.NavMesh.GetComponent<NavMeshGenerator>().
                    FindNearestMovePoint(collidedObjects[i]).GetComponent<MovePoint>().IsUnoccupied())
                {
                    Debug.Log("Check Works");
                }
            }
        }

        // Trim List of Nulls
        GameObject[] finalPlatformList = new GameObject[finalSize];
        for (int i = 0; i < finalSize; i++)
        {
            finalPlatformList[i] = trimmedCollidedObjectList[i];
        }
        return finalPlatformList;
    }

    private GameObject[] ChoosePlatforms(GameObject[] platforms, float diff)
    {
        maxEnemySpawn = Mathf.FloorToInt(Mathf.Min(
            Mathf.Ceil(diff / 4f),
            (float) platforms.Length));
/*        if (maxEnemySpawn <= 0)
        {
            maxEnemySpawn = 1;
        }*/
        GameObject[] selectedPlatforms = new GameObject[maxEnemySpawn];
        for (int i = 0; i < maxEnemySpawn; i++)
        {
            int selection = UnityEngine.Random.Range(i, platforms.Length);

            //Debug.Log("Platforms.length: " + platforms.Length);
            //Debug.Log("Selection: " + selection);

            GameObject selectedGameObject = platforms[selection];
            GameObject temp = platforms[i];
            platforms[i] = platforms[selection];
            platforms[selection] = temp;
            selectedPlatforms[i] = selectedGameObject;
    }
        return selectedPlatforms;
    }

    private void SpawnEnemyOnPlatform(GameObject platform, float diff)
    {
        GameObject enemy;
        enemy = GetEnemy();
        Vector2 spawnpos = platform.transform.position;
        Debug.Log("Spawnpos: " + spawnpos);
        Debug.Log("diff: " + diff);
        enemy.GetComponent<EnemyDefault>().Spawn(spawnpos, diff);
        enemiesSpawned++;

        if (UnityEngine.Random.Range(0f, 1f) > 0.4f + .6/(diff/10f))
        {
            enemy = GetEnemy();
            spawnpos = platform.transform.position;
            enemy.GetComponent<EnemyDefault>().Spawn(spawnpos, diff);
            // Debug.Log(spawnpos);
            enemiesSpawned++;
        }
    }

    private GameObject GetEnemy()
    {
        return Instantiate(level1EnemyPrefabs[UnityEngine.Random.Range(0, level1EnemyPrefabs.Length)]);
    }

    /*
     * Made when I thought the difficulty would range [1, 3]
    private GameObject GetEnemy(float diff, bool makeHard)
    {
        if (makeHard)
            return Instantiate(level1EnemyPrefabs[UnityEngine.Random.Range(Mathf.FloorToInt(diff * (level1EnemyPrefabs.Length * 0.2f)), level1EnemyPrefabs.Length)]);
        else
            return Instantiate(level1EnemyPrefabs[UnityEngine.Random.Range(0, level1EnemyPrefabs.Length - (Mathf.FloorToInt(diff * (level1EnemyPrefabs.Length * 0.2f))))]);
    }
    */
}
