using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerTextSelect : MonoBehaviour
{
    public Text tx;
    public Image im;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.instance.menu.index == index)
        {
            im.enabled = true;
        }
        else
        {
            im.enabled = false;
        }
    }
}
