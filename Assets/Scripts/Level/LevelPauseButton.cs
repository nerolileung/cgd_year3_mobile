using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPauseButton : MonoBehaviour
{
    public void Click(){
        bool current = GameManager.GetFlag(GameManager.GAME_FLAGS.PAUSED);
        GameManager.SetFlag(GameManager.GAME_FLAGS.PAUSED,!current);
        if (current) Time.timeScale = 1;
        else Time.timeScale = 0;
    }
}