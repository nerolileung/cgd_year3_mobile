using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyBuyButton : MonoBehaviour
{
    [SerializeField]
    private Text priceText;
    [SerializeField]
    private Text buttonText;
    [SerializeField]
    private Text pointsDisplay;
    private string currentToy;
    private Image currentToyButton;
    public void Click(){
        // todo sfx - toy noise or "error"
        switch (buttonText.text){
            case "Buy":
                if (GameManager.SpendPoints(GameManager.GetToyPrice())){
                    GameManager.AddToy(currentToy);
                    buttonText.text = "Equip";
                    priceText.text = "Already unlocked!";
                    pointsDisplay.text = GameManager.GetPoints().ToString();
                    currentToyButton.color = new Color(0.6f, 0.6f, 0.6f, 1f);
                }
                else {
                    // error noise
                }
            break;
            case "Equip":
                GameManager.SetCurrentToy(currentToy);
                buttonText.text = "Equipped";
                currentToyButton.color = Color.white;
            break;
            case "Equipped":
                return;
            default:
            Debug.Log("Unknown case: "+buttonText.text);
            break;
        }
    }
    public void SetToy(string toy){
        currentToy = toy;
    }
    public void SetToyImage(Image button){
        currentToyButton = button;
    }
}