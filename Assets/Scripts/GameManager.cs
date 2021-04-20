using System.Collections.Generic;

public static class GameManager
{
    public enum CONTROL_SCHEME {
        SWIPE, TOUCH, BUTTONS
    }
    public enum GAME_FLAGS{
        PAUSED,
        TUTORIAL_COMPLETE,
        CONTROLS_CHANGED
    }
    private static CONTROL_SCHEME currentControls;
    private static int points;
    private static int playerSkinColour;
    private static int playerClothesColour;
    private static string currentToy;
    private static List<string> boughtToys;
    private static Dictionary<GAME_FLAGS, bool> flags;
    private static LevelInfo currentLevel;

    static GameManager()
    {
        // initialise
        // todo load values from save if possible; player prefs?
        currentControls = CONTROL_SCHEME.BUTTONS;
        points = 0;

        playerSkinColour = 0;
        playerClothesColour = 0;

        currentToy = "Teddy";
        boughtToys = new List<string>();
        boughtToys.Add("Teddy");

        flags = new Dictionary<GAME_FLAGS, bool>();
        flags[GAME_FLAGS.PAUSED] = false;
        flags[GAME_FLAGS.TUTORIAL_COMPLETE] = false;
        flags[GAME_FLAGS.CONTROLS_CHANGED] = true;

        currentLevel = null;
    }
    #region controls
    public static CONTROL_SCHEME GetControls(){
        return currentControls;
    }
    public static void SetControls(CONTROL_SCHEME controls){
        currentControls = controls;
        SetFlag(GAME_FLAGS.CONTROLS_CHANGED, true);
    }
    #endregion
    #region points
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
    #endregion
    #region flags
    public static bool GetFlag(GAME_FLAGS flag){
        return flags[flag];
    }
    public static void SetFlag(GAME_FLAGS flag, bool value){
        flags[flag] = value;
    }
    #endregion
    #region player colour
    public static int GetSkinColour(){
        return playerSkinColour;
    }
    public static int GetClothesColour(){
        return playerClothesColour;
    }
    public static void SetSkinColour(int colour){
        playerSkinColour = colour;
    }
    public static void SetClothesColour(int colour){
        playerClothesColour = colour;
    }
    #endregion
    #region toys
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
    public static int GetToyPrice(){
        double price = System.Math.Pow(15,boughtToys.Count);
        price *= 100;
        return (int)price;
    }
    #endregion
    #region level
    public static LevelInfo GetCurrentLevel(){
        return currentLevel;
    }
    public static void SetCurrentLevel(LevelInfo level){
        currentLevel = level;
    }
    #endregion
}