using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyInfoButton : MonoBehaviour
{
    private ToyInfo info;
    private static Text nameText;
    private static Text descText;
    private static Text priceText;
    private static Text buttonText;
    private static Image imageDisplay;
    private static GameObject subMenu;
    public static void InitClass(Text name, Text desc, Text price, Text button, Image img, GameObject _subMenu){
        if (nameText == null){
            nameText = name;
            descText = desc;
            priceText = price;
            buttonText = button;
            imageDisplay = img;
            subMenu = _subMenu;
        }
    }
    public void Init(ToyInfo _info){
        info = _info;
    }
    public void DisplayInfo(){
        // todo sfx
        subMenu.SetActive(true);
        nameText.text = info.displayName;
        descText.text = info.description;
        if (GameManager.HasBoughtToy(gameObject.name)){
            priceText.text = "Already unlocked!";
            if (GameManager.GetCurrentToy() == gameObject.name)
                buttonText.text = "Equipped";
            else
                buttonText.text = "Equip";
        }
        else{
            priceText.text = "Price: " + info.price.ToString() + " points";
            buttonText.text = "Buy";
        }
        imageDisplay.sprite = info.image;
        buttonText.GetComponentInParent<ToyBuyButton>().SetToy(info);
    }
}