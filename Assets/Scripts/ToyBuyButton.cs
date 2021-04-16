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
    private ToyInfo currentToy;
    public void Click(){
        // todo sfx - toy noise or "error"
        switch (buttonText.text){
            case "Buy":
                if (GameManager.SpendPoints(currentToy.price)){
                    GameManager.AddToy(currentToy.name);
                    buttonText.text = "Equip";
                    priceText.text = "Already unlocked!";
                    pointsDisplay.text = GameManager.GetPoints().ToString();
                }
                else {
                    // error noise
                }
            break;
            case "Equip":
                GameManager.SetCurrentToy(currentToy.name);
                buttonText.text = "Equipped";
                // todo change toy sprite colour to indicate state
            break;
            case "Equipped":
                return;
            default:
            Debug.Log("Unknown case: "+buttonText.text);
            break;
        }
    }
    public void SetToy(ToyInfo toy){
        currentToy = toy;
    }
}