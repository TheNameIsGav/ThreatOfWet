using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour
{
    private static Text comboText;

    private void Awake()
    {
        comboText = GameObject.Find("ComboCount").GetComponent<Text>();
    }

    void Start()
    {
        HealthBar.SetHealthBarValue(1);

        //Manual testing of combo scaling
        ScaleCombo(50);
        //ScaleCombo(40);
        //ScaleCombo(30);
        //ScaleCombo(20);
        //ScaleCombo(10);
        //ScaleCombo(1);
    }

    public void UpdatePlayerHealthBar(float currentHealth, float maxHealth)
    {
        HealthBar.SetHealthBarValue(currentHealth / maxHealth);
    }

    public static void ScaleCombo(int comboCount)
    {
        //ParticleSystemController.scaleSystem(comboCount);
        if (playerController.instance != null) {
            comboText.text = comboCount.ToString() + "   " + playerController.instance.comboGrade;
        }
    }

    public static void UpdateWeaponSprite(Sprite wep, Element elem)
    {

        GameObject target = GameObject.Find("WeaponImage");

        target.GetComponent<RawImage>().texture = wep.texture;
        target.GetComponent<RawImage>().SetNativeSize();

        GameObject background = GameObject.Find("WeaponBackground");
        Debug.Log(elem);
        switch (elem){
            case Element.WATER:
                background.GetComponent<Image>().color = Color.blue;
                break;
            case Element.GROUND:
                background.GetComponent<Image>().color = Color.green;
                break;
            case Element.ELECTRIC:
                background.GetComponent<Image>().color = Color.yellow;
                break;
            case Element.FIRE:
                background.GetComponent<Image>().color = Color.red;
                break;
            case Element.DEFAULT:
                background.GetComponent<Image>().color = Color.white;
                break;
        }
    }
}
