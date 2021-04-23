using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyKitty : Toy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        useTimer = false;
        effectReady = true;
        effectOneShot = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override bool OnHit()
    {
        if (effectReady){
            OnEffectStart();
            int xLower = Mathf.FloorToInt(_player.transform.position.x-1);
            int xUpper = Mathf.CeilToInt(_player.transform.position.x+3);
            int yLower = Mathf.FloorToInt(_player.transform.position.y-6.5f);
            int yUpper = Mathf.CeilToInt(_player.transform.position.y-4.5f);
            for (int i = xLower; i < xUpper; i++){
                for (int j = yLower; j < yUpper; j++){
                    GameObject tile = _levelManager.GetTileAt(i,j);
                    if (tile != null)
                        tile.transform.Translate(0f,-12f,0f);
                }
            }
            return true;
        }
        return false;
    }
}
