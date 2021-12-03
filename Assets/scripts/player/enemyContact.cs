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
            playerController.instance.attack.delay = 5;
            playerController.instance.comboCount++;
            playerController.instance.attack.enemy = collision.gameObject;
            playerController.instance.comboDown += 5;
            int exTime = 0;
            bool burn = false;
            float elemBoost = 1f;
            float heavyBoost = 1f;
            if (playerController.instance != null)
            {
                if (playerController.instance.attack.activeWeapon.element == Element.FIRE)
                {
                    burn = true;
                }
                else if (playerController.instance.attack.activeWeapon.element == Element.ELECTRIC)
                {
                    if (playerController.instance.attack.light)
                    {
                        elemBoost = 1.1f;
                    }
                }
                else if (playerController.instance.attack.activeWeapon.element == Element.GROUND)
                {
                    if (!playerController.instance.attack.light)
                    {
                        elemBoost = 1.1f;
                    }
                }
                if (!playerController.instance.attack.light)
                {
                    heavyBoost = 1.5f;
                }
            }
            float gradeBoost = 1f;
            float comboGrade = (playerController.instance.comboUp / playerController.instance.comboDown);
            //SSS
            if(comboGrade > 7)
            {
                gradeBoost = 1.7f;
                playerController.instance.comboGrade = "SSS";
            }
            //SS
            else if(comboGrade > 6)
            {
                gradeBoost = 1.6f;
                playerController.instance.comboGrade = "SS";
            }
            //S
            else if (comboGrade > 5)
            {
                gradeBoost = 1.5f;
                playerController.instance.comboGrade = "S";
            }
            //A
            else if (comboGrade > 4)
            {
                gradeBoost = 1.4f;
                playerController.instance.comboGrade = "A";
            }
            //B
            else if (comboGrade > 3)
            {
                gradeBoost = 1.3f;
                playerController.instance.comboGrade = "B";
            }
            //C
            else if (comboGrade > 2)
            {
                gradeBoost = 1.2f;
                playerController.instance.comboGrade = "C";
            }
            //D
            else if (comboGrade > 1)
            {
                gradeBoost = 1.1f;
                playerController.instance.comboGrade = "D";
            }
            //F
            else
            {
                gradeBoost = 1.0f;
                playerController.instance.comboGrade = "F";
            }

            collision.gameObject.GetComponent<EnemyDefault>().TakeDamage(new Damage(elemBoost * heavyBoost * gradeBoost* (1 + ((float)playerController.instance.comboCount / 100)) * (playerController.instance.attack.activeWeapon.damageBase + playerController.instance.itemVals[1]), true, 2f, playerController.instance.itemVals[6], false, burn, false));
            playerController.instance.ChangeHealth(playerController.instance.itemVals[3] * (elemBoost * heavyBoost * gradeBoost * (1 + ((float)playerController.instance.comboCount / 100)) * (playerController.instance.attack.activeWeapon.damageBase + playerController.instance.itemVals[1])));
            PlayerUIScript.ScaleCombo(playerController.instance.comboCount);
            if(playerController.instance.comboTime >= (playerController.instance.comboBaseTime + exTime + (int)playerController.instance.itemVals[2]) - 25 || playerController.instance.comboTime < 25)
            {
                playerController.instance.comboUp += 50;
            }
            else
            {
                playerController.instance.comboUp += 15;
            }
            //playerController.instance.comboUp += playerController.instance.comboTime;
            playerController.instance.comboTime = playerController.instance.comboBaseTime + exTime + (int)playerController.instance.itemVals[2];
            //combo just scales over time, 1.01, 1.02, 1.03 etc etc
            //then the grade is a seperate value, and the grade value is a seperate scale added on top, and this varries with performance.
            //button variance adds, going fast adds, enemy deaths add
            //same combo subtracts, going slow subtracts, getting hit subtracts

            //sss = .6, ss = .5, s = .4, a = .3, b = .2, c = .1, d = 0


            //what val is the baseline
            /*
             * 
             * how how whwat do
             * hjwo sasda
             * 
             * comboshit = val?
             * comboShit + 20 for every 2 different
             * comboShit -20 for every 2 same
             * comboShit +50 for enemy death
             * comboShit -50 for getting hit
             * comboShit +  (comboTime) - (comboTimeMax - comboTime) 
             * comboShit +5 for each hit
             * 
             * what kinda vals does this give, how do give this a grade?
             * 
             * WAIT POSITIVES OVER NEGATIVES
             * THEN CHECK HOW MUCH GREATER THAN 1?
             * 
             * IF VAL -1 THEN
            comboUp / comboDown - if < 1 then d if <2 then c < 3 then b < 4 then a < 5 then s < 6 then ss < 7 then sss  (mod these values later.)1``
             * 
             * 
             */



        }
        else if (collision.gameObject.CompareTag("Hostile"))
        {
            collision.gameObject.GetComponent<EnemyDefault>().TakeDamage(new Damage(playerController.instance.attack.scale * (4f + playerController.instance.itemVals[1])));
        }
    }
}
