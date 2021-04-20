using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void Click(){
        bool current = GameManager.GetFlag(GameManager.GAME_FLAGS.PAUSED);
        GameManager.SetFlag(GameManager.GAME_FLAGS.PAUSED,!current);
    }
}