using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMainMenu : MonoBehaviour
{
    void Awake(){
        AudioManager.Instance.PlayBGM(Resources.Load<AudioClip>("BGM_Main"));
    }
}
