using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    //public Rigidbody2D rb;
    public int universalBufferTime = 4;
    public float speedCap = 13f;
    public float hori;
    public float vert;
    public bool jump = false;
    //rb = playerController.instance.rb;
    // Start is called before the first frame update
    void Start()
    {
       // rb = playerController.instance.rb;
    }
    public State()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void StateUpdate()
    {

    }
    public void JumpTrigger(float ff)
    {

    }
}
