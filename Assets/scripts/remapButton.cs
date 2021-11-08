using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class remapButton : MonoBehaviour
{
    public int index;
    public Text txt;
    public string val;
    private bool change = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            /*
            if (!Input.anyKey)
            {

            }
            else
            {
                Debug.Log("piss wizard");
                change = false;
                Debug.Log(Event.current.keyCode.ToString());
                customControls.instance.inputLst[index] = Event.current.keyCode;
            }
            */
        }
        else
        {
            txt.text = val + ": " + customControls.instance.inputLst[index].ToString();
        }
    }
    
    public void OnClick()
    {
        //var help = Input.h
        //customControls.instance.inputLst[index] = Input.KeyCode;
        change = true;
        Debug.Log("held");
        txt.text = val + ": ";
        
        
        //Debug.Log(KeyCode.A.ToString());
    }
    public void OnGUI()
    {
        if (change)
        {
            if (!Input.anyKey)
            {

            }
            else
            {
                Debug.Log("piss wizard");
                change = false;
                Debug.Log(Event.current.keyCode.ToString());
                customControls.instance.inputLst[index] = Event.current.keyCode;
            }
        }
        }
}
