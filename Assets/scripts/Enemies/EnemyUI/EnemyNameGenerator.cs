using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyNameGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public TextMeshProUGUI txt;
    public Image healthBar;

    public void GenerateEnemyName(string name)
    {
        txt.text = name;
    }

    public void UpdateEnemyHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.GetComponent<EnemyHealthBar>().SetEnemyHealthBarValue(currentHealth / maxHealth);
    }
}
