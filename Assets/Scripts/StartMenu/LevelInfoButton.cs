using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoButton : MonoBehaviour
{
    private static GameObject subMenu;
    private static Text subMenuTitle;
    private static Text[] subMenuScores;
    private static Text subMenuHighscore;
    private static Image subMenuImage;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Image cover;
    [SerializeField]
    private Image icon;
    private LevelInfo info;
    private List<int> scores;

    public static void InitClass(GameObject sMenu, Text sTitle, Text[] sScores, Text sHiScore, Image sImage){
        if (subMenu == null){
            subMenu = sMenu;
            subMenuTitle = sTitle;
            subMenuScores = sScores;
            subMenuHighscore = sHiScore;
            subMenuImage = sImage;
        }
    }
    public void Init(LevelInfo _info, List<int> _scores){
        info = _info;
        title.text = info.title;
        cover.color = info.tint;
        icon.sprite = info.icon;
        scores = _scores;
    }
    public void DisplayInfo(){
        subMenu.SetActive(true);
        subMenuTitle.text = info.title;
        subMenuImage.sprite = info.icon;
        if (scores.Count == 0){
            subMenuHighscore.text = "Highest Score: 0";
        }
        for (int i = 0; i < subMenuScores.Length; i++){
            // has scores to fill in
            if (i < scores.Count){
                subMenuScores[i].text = string.Format("{0}. {1}",i+1,scores[i]);
                // high score
                if (i == 0){
                    // line break if more than 6 digits
                    if (scores[i] > 999999)
                        subMenuHighscore.text = string.Format("Highest Score:\n{0:n0}",scores[i]);
                    else subMenuHighscore.text = string.Format("Highest Score: {0:n0}",scores[i]);
                }
            }
            // no more scores
            else {
                subMenuScores[i].text = "";
            }
        }
        PlayButtonLevel.SetLevel(info);
    }
}
