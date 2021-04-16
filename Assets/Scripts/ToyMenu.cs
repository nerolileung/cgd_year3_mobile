using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyMenu : MonoBehaviour
{
    [SerializeField]
    GameObject subMenu;
    [SerializeField]
    private Text[] subMenuTexts = new Text[4];
    [SerializeField]
    private Image subMenuImage;
    [SerializeField]
    private Text pointsDisplay;

    void Awake(){
        GameObject prefab = Resources.Load<GameObject>("ToyInfoButton");
        ToyInfo[] toys = Resources.LoadAll<ToyInfo>("ToyInfo");

        ToyInfoButton.InitClass(subMenuTexts[0],subMenuTexts[1],subMenuTexts[2],subMenuTexts[3],subMenuImage,subMenu);

        for (int i = 0; i < toys.Length; i++){
            GameObject clone = Instantiate(prefab,transform);
            clone.name = toys[i].name;
            clone.GetComponent<ToyInfoButton>().Init(toys[i]);
        }
        pointsDisplay.text = GameManager.GetPoints().ToString();
    }
}