using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject subMenu;
    [SerializeField]
    private Text subMenuTitle;
    [SerializeField]
    private Text[] subMenuScores;
    [SerializeField]
    private Text subMenuHighscore;
    [SerializeField]
    private Image subMenuImage;

    void Awake(){
        GameObject prefab = Resources.Load<GameObject>("LevelInfoButton");
        LevelInfo[] levels = Resources.LoadAll<LevelInfo>("LevelInfo");
        LevelInfoButton.InitClass(subMenu,subMenuTitle,subMenuScores,subMenuHighscore,subMenuImage);

        // real levels are just numbered, so tutorial should be at the end
        LevelInfoButton tutorialButton = Instantiate(prefab,transform).GetComponent<LevelInfoButton>();
        tutorialButton.Init(levels[levels.Length-1],GameManager.GetScores(levels[levels.Length-1].name));

        for (int i = 0; i < levels.Length-1; i++){
            LevelInfoButton button = Instantiate(prefab,transform).GetComponent<LevelInfoButton>();
            button.Init(levels[i],GameManager.GetScores(levels[i].name));
        }
    }
}
