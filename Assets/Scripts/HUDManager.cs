using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttons;
    [SerializeField]
    private Text pointsDisplay;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject levelEndMenu;
    [SerializeField]
    private GameObject levelWinDisplay;
    [SerializeField]
    private GameObject levelLoseDisplay;
    private LevelManager _levelManager;
    [SerializeField]
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // update ui
        if (GameManager.GetFlag(GameManager.GAME_FLAGS.CONTROLS_CHANGED)){
            if (GameManager.GetControls() == GameManager.CONTROL_SCHEME.BUTTONS)
                buttons.SetActive(true);
            else buttons.SetActive(false);
            GameManager.SetFlag(GameManager.GAME_FLAGS.CONTROLS_CHANGED, false);
        }
        if (GameManager.GetFlag(GameManager.GAME_FLAGS.PAUSED)){
            pauseMenu.SetActive(true);
        }
        if (_levelManager.pointsChanged){
            pointsDisplay.text = _levelManager.GetPoints().ToString();
            _levelManager.pointsChanged = false;
        }

        // did player die?
        if (!_player.IsAlive()){
            _levelManager.SetLevelLost();
            levelLoseDisplay.SetActive(true);
        }

        if (_levelManager.IsLevelComplete()){
            levelEndMenu.SetActive(true);
            levelWinDisplay.SetActive(true);
        }
    }
}
