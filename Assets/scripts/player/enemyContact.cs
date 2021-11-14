using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyContact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hostile"))
        {
            collision.gameObject.GetComponent<EnemyDefault>().TakeDamage(new Damage(4f));
            playerController.instance.combo = true;
            playerController.instance.comboCount++;
            playerController.instance.attack.comboCount++;
        }
    }
}
