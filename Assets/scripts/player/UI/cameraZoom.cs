using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    public Camera cam;
    public float size = 12;
    // Start is called before the first frame update
    void Start()
    {
        cam.enabled = true;
        if (cam)
        {
           // cam.orthographic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("hell");
        //if (cam)
        //{
           // cam.orthographic = true;
        //}
        
    }

    private void FixedUpdate()
    {
        if ( playerController.instance.comboTime > 0)
        {
            if (size > 5)
            {
           //     size -= 0.15f;
            }
            else
            {
           //     size = 5;
            }
            cam.orthographicSize = size;
        }
        else
        {
            //Debug.Log("no zoom");
            // cam.orthographicSize = 1f;
            //cam.Size = 12;
            if(size < 10)
            {
                size += 0.15f;
            }
            else
            {
                size = 10;
            }
            cam.orthographicSize = size;

        }
    }
}
