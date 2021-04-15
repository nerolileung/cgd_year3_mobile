using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum CONTROL_SCHEME {
        SWIPE, TOUCH, BUTTONS
    }
    public enum CLOTHES_COLOUR {
        DARK_PAPER = 0,
        DARK_GREEN,
        DARK_RED,
        DARK_BLUE,
        DARK_PURPLE,
        LIGHT_INK,
        LIGHT_GREEN,
        LIGHT_RED,
        LIGHT_BLUE,
        LIGHT_PURPLE
    }
    private static CONTROL_SCHEME currentControls;
    private static int points;
    // private static Toy currentToy;
    private static int playerSkinColour;
    private static CLOTHES_COLOUR playerClothesColour;
    private static Dictionary<string, bool> flags;

    // Start is called before the first frame update
    void Start()
    {
        // initialise
        // todo load values from save if possible
        currentControls = CONTROL_SCHEME.BUTTONS;
        points = 0;
        // todo current toy
        flags = new Dictionary<string, bool>();
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

    public int GetSkinColour(){
        return playerSkinColour;
    }
    public CLOTHES_COLOUR GetClothesColour(){
        return playerClothesColour;
    }
    public void SetSkinColour(int colour){
        if (colour < 0 || colour > 9){
            Debug.Log("incorrect skin index given: "+colour);
        }
        playerSkinColour = colour;
    }
    public void SetClothesColour(CLOTHES_COLOUR colour){
        playerClothesColour = colour;
    }
}