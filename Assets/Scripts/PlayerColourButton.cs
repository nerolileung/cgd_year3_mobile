using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColourButton : MonoBehaviour
{
    private static GameManager _gameManager;
    private static Image playerImage;
    private static Sprite[,] images;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (playerImage == null)
            playerImage = GameObject.Find("Character Sprite").GetComponent<Image>();
        if (images == null){
            images = new Sprite[10,10];
            // todo
            for (int i = 0; i < 1; i++){
                for (int j = 0; j < 10; j++){
                    Sprite[] sprites = Resources.LoadAll<Sprite>("player_"+i+j);
                    images[i,j] = sprites[2];
                }
            }
        }
    }

    public void SetSkin(int colour){
        _gameManager.SetSkinColour(colour);
        UpdateImage();
    }
    public void SetClothes(int colour){
        _gameManager.SetClothesColour((GameManager.CLOTHES_COLOUR)colour);
        UpdateImage();
    }
    private void UpdateImage(){
        int skin = _gameManager.GetSkinColour();
        int clothes = (int)_gameManager.GetClothesColour();
        playerImage.sprite = images[skin,clothes];
    }
}