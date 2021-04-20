using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelInfo level;
    private char[][] levelMap;
    private GameObject _camera;

    // Start is called before the first frame update
    void Start()
    {
        level = GameManager.GetCurrentLevel();

        // load bgm
        if (level.bgm != null)
            AudioManager.Instance.PlayBGM(level.bgm);

        // load map
        string[] separators = {"\r\n", "\r", "\n"};
        string[] lines = level.map.text.Split(separators,System.StringSplitOptions.None);
        for (int i = 0; i < lines.Length; i++){
            levelMap[i] = lines[i].ToCharArray();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
