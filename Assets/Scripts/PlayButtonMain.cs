using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButtonMain : MonoBehaviour
{
    [SerializeField]
    private GameObject levelMenu;
    [SerializeField]
    private GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        if (GameManager.GetFlag("TUTORIAL_COMPLETE")){
           button.onClick.AddListener(() => levelMenu.SetActive(true));
           button.onClick.AddListener(() => mainMenu.SetActive(false));
        }
        else {
            button.onClick.AddListener(()=>SceneManager.LoadScene("Level"));
            button.onClick.AddListener(()=>SceneManager.LoadScene("HUD",LoadSceneMode.Additive));
            LevelInfo tutorial = Resources.Load<LevelInfo>("Tutorial");
            GameManager.SetCurrentLevel(tutorial);
        }
    }
}