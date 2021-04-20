using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColourMenu : MonoBehaviour
{
    void Awake(){
        Image _image = GetComponent<Image>();
        Sprite[] spritesheet = Resources.LoadAll<Sprite>("player_"+GameManager.GetSkinColour()+GameManager.GetClothesColour());
        _image.sprite = spritesheet[2];
    }
}