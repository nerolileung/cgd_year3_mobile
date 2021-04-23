using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private static AudioMixer mixer;
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioSource bgmSource;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { 
			DestroyImmediate(this.gameObject); 
		}

        if (mixer == null)
            mixer = Resources.Load<AudioMixer>("AudioMixer");

        SceneManager.LoadScene(1);
    }

    // todo lower volume when game is paused

    public void SetSFXVolume(float volume){
        mixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
    }
    public void SetBGMVolume(float volume){
        mixer.SetFloat("BGMVol", Mathf.Log10(volume) * 20);
    }
    public void SetTotalVolume(float volume){
        mixer.SetFloat("MasterVol", Mathf.Log10(volume) * 20);
    }

    public void PlaySFX(AudioClip sound){
        sfxSource.PlayOneShot(sound);
    }
    public void PlayBGM(AudioClip music){
        if (bgmSource.clip == music) return;
        else {
            bgmSource.Stop();
            bgmSource.clip = music;
            bgmSource.Play();
            bgmSource.loop = true;
        }
    }
}
