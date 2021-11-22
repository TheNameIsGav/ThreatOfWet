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
        if (collision.gameObject.CompareTag("Hostile") && playerController.instance.attack.delay <= 0)
        {
            if (!playerController.instance.attack.light) 
            {
                collision.gameObject.GetComponent<EnemyDefault>().TakeDamage(new Damage(1.5f * playerController.instance.attack.scale * (playerController.instance.attack.activeWeapon.damageBase + playerController.instance.itemVals[1]), true, 2f, playerController.instance.itemVals[6]));
            }
            else
            {
                collision.gameObject.GetComponent<EnemyDefault>().TakeDamage(new Damage(playerController.instance.attack.scale * (playerController.instance.attack.activeWeapon.damageBase + playerController.instance.itemVals[1]), true, 2f, playerController.instance.itemVals[6]));
            }
            playerController.instance.ChangeHealth(playerController.instance.itemVals[3]* (playerController.instance.attack.scale * (playerController.instance.attack.activeWeapon.damageBase + playerController.instance.itemVals[1])));
            playerController.instance.attack.delay = 5;
            if(true /*playerController.instance.combo && (playerController.instance.attack.activeWeapon.element != playerController.instance.attack.enemy.GetComponent<EnemyDefault>().Element || playerController.instance.attack.activeWeapon.element == Element.DEFAULT)*/)
            {
                playerController.instance.combo = true;
                playerController.instance.comboCount++;
                playerController.instance.attack.comboCount++;
                playerController.instance.attack.enemy = collision.gameObject;
            }
        }
        else if (collision.gameObject.CompareTag("Hostile"))
        {
            collision.gameObject.GetComponent<EnemyDefault>().TakeDamage(new Damage(playerController.instance.attack.scale * (playerController.instance.attack.activeWeapon.damageBase + playerController.instance.itemVals[1]), true, 2f, playerController.instance.itemVals[6]));
        }
    }
}
