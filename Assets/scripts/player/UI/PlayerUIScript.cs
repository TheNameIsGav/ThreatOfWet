using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour
{
    // Start is called before the first frame update

    /*    [SerializeField]
        private Image[] comboPeices;

        [SerializeField]
        private Sprite[] comboTiles;
    */

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

    private void ShakeCombo()
    {
        Debug.Log("Shaking");
        IEnumerator coroutine;
        coroutine = Shaky(.5f);
        StartCoroutine(coroutine);
    }

    private IEnumerator Shaky(float duration)
    {
        float shareOffset = 0f;
        for(float i = 0; i <= duration; i+= .01f)
        {
            shareOffset = Random.Range(-1f, 1f);
            //GameObject.Find("ComboContainer").transform.localPosition += new Vector3(shareOffset, shareOffset, 0);
            yield return new WaitForSeconds(.01f);
        }
        //GameObject.Find("ComboContainer").transform.localPosition = Vector3.zero;
    }

    public static void ScaleCombo(int comboCount)
    {
        ParticleSystemController.scaleSystem(comboCount);
        if (playerController.instance != null) {
            comboText.text = comboCount.ToString() + "   " + playerController.instance.comboGrade;
        }
    }


    /*public void AdjustComboCounter(int currentCount, int comboType)
    {
        Debug.Log(currentCount);
        switch (currentCount){
            case -3: //3 electric unfilled, 1 normal unfilled
                comboPeices[0].sprite = comboTiles[2];
                comboPeices[1].sprite = comboTiles[2];
                comboPeices[2].sprite = comboTiles[2];
                comboPeices[3].sprite = comboTiles[0];
                break;
            case -2: //1 electric filled, 2 electric unfilled, 1 normal unfilled
                comboPeices[0].sprite = comboTiles[3];
                break;
            case -1: //2 electric filled, 1 electric unfilled, 1 normal unfilled
                comboPeices[1].sprite = comboTiles[3];
                break;
            case 0:
                // Restart combo, add functionality to see if we need to display electric before resetting -- add additional switch to determine shattered combo (1), electric combo (2) , or normal combo (0)
                comboPeices[0].sprite = comboTiles[0];
                comboPeices[1].sprite = comboTiles[0];
                comboPeices[2].sprite = comboTiles[0];
                comboPeices[3].sprite = comboTiles[0];
                if (comboType  == 1) { ShakeCombo(); }
                break;
            case 1:
                comboPeices[0].sprite = comboTiles[1];
                break;
            case 2:
                comboPeices[1].sprite = comboTiles[1];
                break;
            case 3:
                comboPeices[2].sprite = comboTiles[1];
                break;
            case 4:
                comboPeices[3].sprite = comboTiles[1];
                break;
        }
    }*/
}
