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
        // todo
    }
}