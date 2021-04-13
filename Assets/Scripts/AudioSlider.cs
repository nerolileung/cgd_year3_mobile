using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private static AudioManager _audioManager;
    [SerializeField]
    private Text textField;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Slider bgmSlider;

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void ChangeVolume(float volume){
        switch (gameObject.name){
            case "Main Slider":
            _audioManager.SetTotalVolume(volume);
            // calculate percentage and display it
            float percentage = volume/1.0001f;
            string text = Mathf.RoundToInt(percentage * 100).ToString();
            text += "%";
            textField.text = text;
            break;
            case "SFX Slider":
            _audioManager.SetSFXVolume(volume);
            break;
            case "BGM Slider":
            _audioManager.SetBGMVolume(volume);
            break;
            default:
            Debug.Log("unknown audio slider! name: "+gameObject.name);
            break;
        }
    }
}
