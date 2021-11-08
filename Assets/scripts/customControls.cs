using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customControls : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode[] inputLst = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space, KeyCode.I, KeyCode.E, KeyCode.O, KeyCode.P, KeyCode.L, KeyCode.Semicolon };
    /*
    public string up = "w";
    public string down = "s";
    public string left = "a";
    public string right = "d";
    public string jump = "space";
    public string dash = "i";
    public string interact = "e";
    public string lightM = "o";
    public string heavyM = "p";
    public string lightR = "l";
    public string heavyR = ";";
    */
    public static customControls instance;
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
