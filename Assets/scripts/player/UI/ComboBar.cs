using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboBar : MonoBehaviour
{
    private static Image counter;
    private static float decreaseScale;
    static float Scale { get { return decreaseScale; } set { decreaseScale = value; } }

    public static void ResetCounter()
    {
        counter.fillAmount = 1;
    }

    public static void SetScale(float amt)
    {
        decreaseScale = amt;
    }

    private void Update()
    {
        //Debug.Log(playerController.instance.comboTime.ToString() + playerController.instance.comboBase);
        Debug.Log(Mathf.Min(1f, ( (float)playerController.instance.comboTime / (float)(playerController.instance.comboBaseTime + (int) playerController.instance.itemVals[2]))));
        //counter.fillAmount = Mathf.Clamp(counter.fillAmount - (.01f  * decreaseScale), 0, 1);
        counter.fillAmount = Mathf.Min(1f, ((float)playerController.instance.comboTime / (float)(playerController.instance.comboBaseTime + (int)playerController.instance.itemVals[2])));
    }

    private void Awake()
    {
        counter = GetComponent<Image>();
    }

}
