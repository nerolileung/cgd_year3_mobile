using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyDucky : Toy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        useTimer = false;
        effectReady = true;
        effectOneShot = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (state == TOY_STATE.AFFECTING){
            if (_player.transform.position.y < -4){
                Vector3 pos = _player.transform.position;
                pos.y = -3.5f;
                _player.transform.position = pos;
            }
            _player.transform.Translate(Time.deltaTime*2f,Time.deltaTime*24f,0f,Space.Self);
            // check tiles below player
            int startY = Mathf.FloorToInt(_player.transform.localPosition.y-0.5f)-6;
            for (int i = startY; i > -12; i--){
                GameObject tile = _levelManager.GetTileAt(Mathf.CeilToInt(_player.transform.position.x),i);
                if (tile != null && tile.tag != "Danger"){
                    state = TOY_STATE.EFFECT_END;
                    _player.transform.Translate(-_player.transform.localPosition.x,0f,0f);
                    _player.SetGhost(false);
                    break;
                }
            }
        }
    }
    public override bool OnFall()
    {
        if (effectReady){
            OnEffectStart();
            _player.SetGhost(true);
            _player.transform.Translate(0f,2f,0f);
            return true;
        }
        return false;
    }
}
