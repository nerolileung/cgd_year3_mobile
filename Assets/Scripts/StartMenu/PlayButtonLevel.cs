using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonLevel : MonoBehaviour
{
    private static LevelInfo level;
    public static void SetLevel(LevelInfo _level){
        level = _level;
    }
    public void Click(){
        GameManager.SetCurrentLevel(level);
        SceneManager.LoadScene("Level");
    }
}
