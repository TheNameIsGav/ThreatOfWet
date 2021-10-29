using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnTile : MonoBehaviour
{
    public GameObject bossSpawn;
    // Start is called before the first frame update
    public void SpawnBoss()
    {
        bossSpawn.SetActive(true);
    }

    // Update is called once per frame
    public void SetBoss(GameObject boss)
    {
        boss.transform.position = transform.position;
        boss.SetActive(false);
    }
}
