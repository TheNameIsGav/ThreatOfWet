using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerWeaponSelect : MonoBehaviour
{
    public Text pray;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        if(index == 0)
        {
            pray.text = playerController.instance.menu.weaponList[playerController.instance.menu.weaponPos].name;
        }
        else if( index == 1)
        {
            pray.text = playerController.instance.menu.weaponList[playerController.instance.menu.weaponPos].desciption;
        }
        else
        {
            pray.text = playerController.instance.menu.weaponList[playerController.instance.menu.weaponPos].element.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
