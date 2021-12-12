using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthText : MonoBehaviour
{
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = playerController.instance.health.ToString() + "/" + (playerController.instance.maxHealth + playerController.instance.itemVals[4]).ToString();
    }
}
