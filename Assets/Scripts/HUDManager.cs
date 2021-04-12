using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private GameObject _camera;
    private float cameraOffset;
    private Player _player;
    private GameManager _gameManager;

    private LevelInfo _info;
    private float currentSpeed;
    private int points;

    [SerializeField]
    private GameObject buttons;
    [SerializeField]
    private GameObject pointsDisplay;
    [SerializeField]
    private GameObject levelEndDisplay;
    private Text levelEndText;
    

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindWithTag("MainCamera");
        cameraOffset = _camera.transform.position.x; // initial x pos

        _player = _camera.GetComponentInChildren<Player>();

        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (_gameManager.GetControls()==GameManager.CONTROL_SCHEME.BUTTONS)
            buttons.SetActive(true);
        else buttons.SetActive(false);

        _info = Resources.Load<LevelInfo>("Levels/PInfo"); //temp
        currentSpeed = _info.startSpeed;

        levelEndText = levelEndDisplay.GetComponentInChildren<Text>();
        levelEndDisplay.SetActive(false);

        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float offsetPosition = _camera.transform.position.x+cameraOffset;
        if (offsetPosition >= _info.levelLength){
            levelEndDisplay.SetActive(true);
            levelEndText.text = "Level Complete!";
        }
        else if (_player.IsAlive()){
            // move camera and therefore player
            currentSpeed = Mathf.Lerp(_info.startSpeed,_info.endSpeed,offsetPosition/_info.levelLength);
            _camera.transform.Translate(currentSpeed * Time.deltaTime, 0f, 0f);
        }
        else {
            levelEndDisplay.SetActive(true);
            levelEndText.text = "Level Incomplete...";
        }
    }
    public void SetInfo(LevelInfo info){
        _info = info;
        currentSpeed = _info.startSpeed;
    }
    // for button controls
    public void PlayerJump(){
        _player.Jump();
    }
    public void PlayerSlide(){
        _player.Slide();
    }
    public void AddScore(int score){
        points += score;
        pointsDisplay.GetComponent<Text>().text = points.ToString();
    }
}