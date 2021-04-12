using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum CONTROL_SCHEME {
        SWIPE, TOUCH, BUTTONS
    }
    private CONTROL_SCHEME currentControls;
    private int points;
    // private Toy currentToy;
    private Dictionary<string, bool> flags;

    // Start is called before the first frame update
    void Start()
    {
        // initialise
        // todo load values from save if possible
        currentControls = CONTROL_SCHEME.BUTTONS;
        points = 0;
        // todo current toy
        flags["PAUSED"] = false;
        flags["TUTORIAL_COMPLETE"] = false;

        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public CONTROL_SCHEME GetControls(){
        return currentControls;
    }
    public void SetControls(CONTROL_SCHEME controls){
        currentControls = controls;
    }

    public int GetPoints(){
        return points;
    }
    public void AddPoints(int change){
        points += change;
    }
    public bool SpendPoints(int change){
        if (change > points) return false;
        else {
            change -= points;
            return true;
        }
    }

    // get set toy
    
    public bool GetFlag(string flag){
        return flags[flag];
    }
    public void SetFlag(string flag, bool value){
        flags[flag] = value;
    }
}