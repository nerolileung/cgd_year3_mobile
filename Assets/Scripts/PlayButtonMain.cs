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
        GameManager _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        Button button = gameObject.GetComponent<Button>();
        if (_gameManager.GetFlag("TUTORIAL_COMPLETE")){
           button.onClick.AddListener(() => levelMenu.SetActive(true));
           button.onClick.AddListener(() => mainMenu.SetActive(false));
        }
        else {
            // todo change to Real tutorial
            button.onClick.AddListener(()=>SceneManager.LoadScene("Prototype"));
            button.onClick.AddListener(()=>SceneManager.LoadScene("HUD",LoadSceneMode.Additive));
        }
    }
}
