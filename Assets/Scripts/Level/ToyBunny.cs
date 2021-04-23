using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBunny : Toy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        useTimer = false;
        effectReady = false;
        speedMod = 1.5f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
