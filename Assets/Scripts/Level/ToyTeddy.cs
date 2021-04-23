using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyTeddy : Toy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        useTimer = true;
        timerMax = 10f;
        timerCurrent = timerMax;
        effectReady = false;
        effectOneShot = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override bool OnPointGain(){
        if (effectReady){
            OnEffectStart();
            _levelManager.ChangePoints(10);
            return true;
        }
        return false;
    }
}
