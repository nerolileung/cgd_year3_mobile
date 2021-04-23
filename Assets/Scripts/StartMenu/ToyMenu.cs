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

            Image _image = clone.GetComponent<Image>();
            _image.sprite = toys[i].image;
            if (!GameManager.HasBoughtToy(toys[i].name))
                _image.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            else if (GameManager.GetCurrentToy() != toys[i].name)
                _image.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        }
        pointsDisplay.text = GameManager.GetPoints().ToString();
    }
}