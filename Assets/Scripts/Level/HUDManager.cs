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
    [SerializeField]
    private Text levelScoreText;
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
        if (!_player.IsAlive()&&!_levelManager.IsLevelComplete()){
            _levelManager.SetLevelLost();
            levelEndMenu.SetActive(true);
            DisplayFinalScore();
            levelLoseDisplay.SetActive(true);
        }

        if (_levelManager.IsLevelComplete()&&!levelEndMenu.activeInHierarchy){
            levelEndMenu.SetActive(true);
            DisplayFinalScore();
            levelWinDisplay.SetActive(true);
        }
    }
    private void DisplayFinalScore(){
        if (GameManager.AddCurrentScore(_levelManager.GetPoints()))
            levelScoreText.text = "NEW HIGH SCORE: ";
        else levelScoreText.text = "Score: ";
        if (_levelManager.GetPoints() > 999999) levelScoreText.text+="\n";
        levelScoreText.text = string.Format("{0}{1:n0}",levelScoreText.text,_levelManager.GetPoints());
    }
}
