using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo : Player
{
    public Ichigo() : base() { }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.speed = 1f;
        this.jumpPow = 100f;
        this.hp = 100;
        this.mp = 0;
        this.attackDelay = 0.5f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
