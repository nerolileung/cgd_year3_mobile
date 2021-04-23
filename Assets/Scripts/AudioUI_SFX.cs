using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioUI_SFX : MonoBehaviour
{
    public void Play(AudioClip sfx){
        AudioManager.Instance.PlaySFX(sfx);
    }
}