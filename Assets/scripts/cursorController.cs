using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorController : MonoBehaviour
{
    private Vector3 position1 = new Vector3 (-120, 64, 0);
    private Vector3 position2 = new Vector3(-114, 5, 0);
    private Vector3 position3 = new Vector3(-70, -48.5f, 0);
    private Vector3 position4 = new Vector3(-44, -90, 0);
    public Transform myCursor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveCursorStart()
    {
        myCursor.transform.localPosition = position1;
    }

    public void moveCursorTutorial()
    {
        myCursor.transform.localPosition = position2;
    }

    public void moveCursorOptions()
    {
        myCursor.transform.localPosition = position3;
    }

    public void moveCursorExit()
    {
        myCursor.transform.localPosition = position4;
    }
}
