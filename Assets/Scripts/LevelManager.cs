using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private enum LEVEL_STATE {
        PLAYING,
        WIN,
        LOSE
    }
    private LevelInfo level;
    private char[][] levelMap;
    [SerializeField]
    private GameObject _mapBookend;
    [SerializeField]
    private TilePooler _pooler;
    [SerializeField]
    private GameObject _camera;
    private int newTileX;
    private int rightestTileX;
    private int mapLength;
    private Toy _toy;
    private LEVEL_STATE state;
    private int points;
    public bool pointsChanged;

    // Start is called before the first frame update
    void Start()
    {
        level = GameManager.GetCurrentLevel();

        // load bgm
        if (level.bgm != null)
            AudioManager.Instance.PlayBGM(level.bgm);

        // load map data
        mapLength = 0;
        string[] separators = {"\r\n", "\r", "\n"};
        string[] lines = level.map.text.Split(separators,System.StringSplitOptions.None);
        levelMap = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++){
            levelMap[i] = lines[i].ToCharArray();
            if (mapLength < lines[i].Length)
                mapLength = lines[i].Length;
        }
        mapLength -= 1;

        // find distance between camera (player) and right edge of screen
        Camera cam = _camera.GetComponent<Camera>();
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,cam.pixelHeight,0f));
        newTileX = Mathf.CeilToInt(point.x);
        rightestTileX = newTileX;
        
        // initialise map tiles
        for (int i = 0; i < levelMap.Length; i++){
            for (int j = 0; j <= newTileX; j++){
                if (levelMap[i][j] != ' '){
                    GameObject tile = _pooler.GetObject();
                    tile.transform.localPosition = new Vector3(j,-i,0f);
                    if (levelMap[i][j] == 'x'){
                        tile.tag = "Danger";
                    }
                }
            }
        }

        state = LEVEL_STATE.PLAYING;
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GetFlag(GameManager.GAME_FLAGS.PAUSED)){
            switch (state){
                case LEVEL_STATE.PLAYING:
                if (_camera.transform.position.x < mapLength){
                    // move player
                    // todo float currentSpeed = Mathf.Lerp(level.startSpeed*_toy.GetSpeedModifier(),level.endSpeed,_camera.transform.position.x/mapLength);
                    float currentSpeed = Mathf.Lerp(level.startSpeed,level.endSpeed,_camera.transform.position.x/mapLength);
                    _camera.transform.Translate(currentSpeed * Time.deltaTime,0,0);

                    // end of visible tiles reached
                    if (rightestTileX - _camera.transform.position.x < newTileX){
                        // there are map tiles left
                        if (rightestTileX < mapLength){
                            UpdateMap();
                            UpdatePoints();
                        }
                        // show bookend
                        else if (!_mapBookend.activeInHierarchy) {
                            _mapBookend.SetActive(true);
                            _mapBookend.transform.localPosition = new Vector3(rightestTileX+8,-4f,0f);
                        }
                    }
                }
                else if (!IsLevelComplete()){
                    state = LEVEL_STATE.WIN;
                    GameManager.AddPoints(points);
                }
                break;
                case LEVEL_STATE.WIN:
                if (level.title == "Tutorial"){
                        GameManager.SetFlag(GameManager.GAME_FLAGS.TUTORIAL_COMPLETE, true);
                    }
                break;
            }
        }
    }

    private void UpdateMap(){
        rightestTileX += 1;
        for (int i = 0; i < levelMap.Length; i++){
            if (levelMap[i][rightestTileX] != ' '){
                GameObject tile = _pooler.GetObject();
                tile.transform.localPosition = new Vector3(rightestTileX,-i,0f);
                if (levelMap[i][rightestTileX] == 'x') {
                    tile.tag = "Danger";
                }
            }
        }
    }
    private void UpdatePoints(){
        points += 10;
        pointsChanged = true;
        // todo _toy.OnPointGain();
    }
    public int GetPoints(){
        return points;
    }

    public bool IsLevelComplete(){
        return state != LEVEL_STATE.PLAYING;
    }
    public void SetLevelLost(){
        state = LEVEL_STATE.LOSE;
        GameManager.AddPoints(points);
    }

    public void SetToy(Toy value){
        _toy = value;
    }
}