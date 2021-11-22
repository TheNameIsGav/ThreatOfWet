using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerMenuText : MonoBehaviour
{
    public Text ts;
    public int val;
    // Start is called before the first frame update
    void Start()
    {
        if(val == 0)
        {
            val = playerController.instance.menu.att;
        }
        else if(val == 1)
        {
            val = playerController.instance.menu.def;
        }
        else
        {
            val = playerController.instance.menu.luck;
        }
        ts.text = playerController.instance.menu.descrip[val];
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
