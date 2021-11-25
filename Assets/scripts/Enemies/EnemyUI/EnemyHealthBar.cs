using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    private Image health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Image>();
    }

    public void SetEnemyHealthBarValue(float value)
    {
        health.fillAmount = value;
        if (health.fillAmount < 0.2f)
        {
            SetEnemyHealthBarColor(Color.red);
        }
        else if (health.fillAmount < 0.4f)
        {
            SetEnemyHealthBarColor(Color.yellow);
        }
        else
        {
            SetEnemyHealthBarColor(Color.green);
        }
    }

    public float GetEnemyHealthBarValue()
    {
        return health.fillAmount;
    }

    public void SetEnemyHealthBarColor(Color healthColor)
    {
        health.color = healthColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
