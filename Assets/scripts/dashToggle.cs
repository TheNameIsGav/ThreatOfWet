using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashToggle : MonoBehaviour
{
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (customControls.instance.techDash)
        {
            txt.text = "Dash Type: Technical";
        }
        else
        {
            txt.text = "Dash Type: Auto";
        }
    }

    public void onClick()
    {
        customControls.instance.techDash = !customControls.instance.techDash;
    }
}
