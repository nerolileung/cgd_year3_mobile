using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private Text textField;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Slider bgmSlider;

    public void ChangeVolume(float volume){
        switch (gameObject.name){
            case "Main Slider":
            AudioManager.Instance.SetTotalVolume(volume);
            // calculate percentage and display it
            float percentage = volume/1.0001f;
            string text = Mathf.RoundToInt(percentage * 100).ToString();
            text += "%";
            textField.text = text;
            break;
            case "SFX Slider":
            AudioManager.Instance.SetSFXVolume(volume);
            break;
            case "BGM Slider":
            AudioManager.Instance.SetBGMVolume(volume);
            break;
            default:
            Debug.Log("unknown audio slider! name: "+gameObject.name);
            break;
        }
    }
}
