using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyPuppy : Toy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        useTimer = true;
        timerMax = 12f;
        timerCurrent = timerMax;
        effectReady = false;
        effectOneShot = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (effectReady){
            OnEffectStart();
            int yLower = Mathf.FloorToInt(_player.transform.position.y-6.5f);
            int yUpper = Mathf.FloorToInt(_player.transform.position.y-4.5f);
            int xPos = Mathf.CeilToInt(_player.transform.position.x);
            for (int i = 0; i < 10; i++){
                bool effectHappened = false;
                for (int j = yLower; j < yUpper; j++){
                    GameObject tile = _levelManager.GetTileAt(xPos+i,j);
                    if (tile != null){
                        tile.transform.Translate(0f,-12f,0f);
                        effectHappened = true;
                    }
                }
                if (effectHappened) break;
            }
        }
    }
}
