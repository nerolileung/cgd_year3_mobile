using System.Collections.Generic;

public static class GameManager
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
    private static int playerSkinColour;
    private static CLOTHES_COLOUR playerClothesColour;
    private static string currentToy;
    private static List<string> boughtToys;
    private static Dictionary<string, bool> flags;

    static GameManager()
    {
        // initialise
        // todo load values from save if possible; player prefs?
        currentControls = CONTROL_SCHEME.BUTTONS;
        points = 0;

        playerSkinColour = 0;
        playerClothesColour = CLOTHES_COLOUR.DARK_PAPER;

        currentToy = "Teddy";
        boughtToys = new List<string>();
        boughtToys.Add("Teddy");

        flags = new Dictionary<string, bool>();
        flags["PAUSED"] = false;
        flags["TUTORIAL_COMPLETE"] = false;   
    }

    public static CONTROL_SCHEME GetControls(){
        return currentControls;
    }
    public static void SetControls(CONTROL_SCHEME controls){
        currentControls = controls;
    }

    public static int GetPoints(){
        return points;
    }
    public static void AddPoints(int change){
        points += change;
    }
    public static bool SpendPoints(int change){
        if (change > points) return false;
        else {
            points -= change;
            return true;
        }
    }
    public static bool GetFlag(string flag){
        return flags[flag];
    }
    public static void SetFlag(string flag, bool value){
        flags[flag] = value;
    }

    public static int GetSkinColour(){
        return playerSkinColour;
    }
    public static CLOTHES_COLOUR GetClothesColour(){
        return playerClothesColour;
    }
    public static void SetSkinColour(int colour){
        playerSkinColour = colour;
    }
    public static void SetClothesColour(CLOTHES_COLOUR colour){
        playerClothesColour = colour;
    }

    public static bool HasBoughtToy(string id){
        return boughtToys.Contains(id);
    }
    public static void AddToy(string id){
        // avoid duplication
        if (!boughtToys.Contains(id)){
            boughtToys.Add(id);
        }
    }
    public static string GetCurrentToy(){
        return currentToy;
    }
    public static void SetCurrentToy(string id){
        if (boughtToys.Contains(id)){
            currentToy = id;
        }
    }
}