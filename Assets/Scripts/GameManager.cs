using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private LevelManager _levelManager;
    
    public enum CONTROL_SCHEME {
        SWIPE, TOUCH, BUTTONS
    }
    private CONTROL_SCHEME currentControls;

    // Start is called before the first frame update
    void Start()
    {
        currentControls = CONTROL_SCHEME.BUTTONS;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int levelIndex){
        // load scene
        SceneManager.LoadScene("Prototype");
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }

    public CONTROL_SCHEME GetControls(){
        return currentControls;
    }
}