using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColourButton : MonoBehaviour
{
    private static Image playerImage;
    private static Sprite[,] images;

    // Start is called before the first frame update
    void Start()
    {
        if (playerImage == null)
            playerImage = GameObject.Find("Character Sprite").GetComponent<Image>();
        if (images == null){
            images = new Sprite[10,10];
            for (int i = 0; i < 10; i++){
                for (int j = 0; j < 10; j++){
                    Sprite[] sprites = Resources.LoadAll<Sprite>("player_"+i+j);
                    images[i,j] = sprites[2];
                }
            }
        }
    }

    public void SetSkin(int colour){
        GameManager.SetSkinColour(colour);
        UpdateImage();
    }
    public void SetClothes(int colour){
        GameManager.SetClothesColour(colour);
        UpdateImage();
    }
    private void UpdateImage(){
        int skin = GameManager.GetSkinColour();
        int clothes = (int)GameManager.GetClothesColour();
        playerImage.sprite = images[skin,clothes];
    }
}