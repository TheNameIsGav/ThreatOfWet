using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    //public Rigidbody2D rb;
    public int universalBufferTime = 4;
    public float speedCap = 13f;
    public float hori;
    public float vert;
    public bool jump = false;
    //public Rigidbody2D rb = playerController.instance.GetComponent<Rigidbody2D>();
    public int eatShit = 4;
    //rb = playerController.instance.rb;
    // Start is called before the first frame update



    // Update is called once per frame
    public abstract void Update();

    public abstract void StateUpdate();

    public abstract void OnEnter();
    public abstract void OnExit();

  
    public abstract void JumpTrigger();
}
