using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class customControls : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    public KeyCode[,] inputLst = new KeyCode[,] {
        { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space, KeyCode.I, KeyCode.E, KeyCode.O, KeyCode.P, KeyCode.L, KeyCode.Semicolon },
        {KeyCode.Joystick5Button6, KeyCode.Joystick5Button6, KeyCode.Joystick5Button6, KeyCode.Joystick5Button6, KeyCode.Joystick1Button0, KeyCode.Joystick1Button2,KeyCode.Joystick1Button1,KeyCode.Joystick1Button5, KeyCode.Joystick5Button6, KeyCode.Joystick1Button4, KeyCode.Joystick5Button6 }
    };
    */
    public KeyCode[] inputLst = new KeyCode[]
        {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space, KeyCode.I, KeyCode.E, KeyCode.O, KeyCode.P, KeyCode.L, KeyCode.Semicolon };

    public Weapon pMelee = new StarterSword();
    public Weapon pRange = new StarterGun();
    public bool techDash = true;
    public int weaponIndex = 0;
    public float[] pItems = new float[] { 0f, 0f, 0f, 0f, 0f, 0f, 10f, 0f, 35f };
    //float hjj = Input.GetAxis("Joystick1Axis1");
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
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            pItems = new float[] { 0f, 0f, 0f, 0f, 0f, 0f, 10f, 0f, 35f };
            pMelee = new StarterSword();
            pRange = new StarterGun();
            weaponIndex = 11;
        }
    }
}
