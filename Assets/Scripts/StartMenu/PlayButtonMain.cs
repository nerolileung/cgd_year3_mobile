using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButtonMain : MonoBehaviour
{
    [SerializeField]
    private Canvas levelMenu;
    [SerializeField]
    private Canvas mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        if (GameManager.GetFlag(GameManager.GAME_FLAGS.TUTORIAL_COMPLETE)){
           button.onClick.AddListener(() => levelMenu.enabled = true);
           button.onClick.AddListener(() => mainMenu.enabled = false);
        }
        else {
            button.onClick.AddListener(()=>SceneManager.LoadScene("Level"));
            LevelInfo tutorial = Resources.Load<LevelInfo>("LevelInfo/Tutorial");
            GameManager.SetCurrentLevel(tutorial);
        }
    }
}