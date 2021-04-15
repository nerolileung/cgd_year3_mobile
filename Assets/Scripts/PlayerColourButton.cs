using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColourButton : MonoBehaviour
{
    private static GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void SetSkin(int colour){
        _gameManager.SetSkinColour(colour);
    }
    public void SetClothes(int colour){
        _gameManager.SetClothesColour((GameManager.CLOTHES_COLOUR)colour);
    }
}