using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int group;
    public GameObject enemySpawn;
    // Start is called before the first frame update
    public void SpawnEnemy()
    {
        enemySpawn.SetActive(true);
    }

    // Update is called once per frame
    public void SetEnemy(GameObject enemy)
    {
        enemy.transform.position = transform.position;
        enemy.SetActive(false);
    }
}
