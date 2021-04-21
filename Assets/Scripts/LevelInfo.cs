using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Data", menuName = "LevelInfo", order = 1)]
public class LevelInfo : ScriptableObject
{
    public string title;
    //public Sprite icon;
    public float startSpeed;
    public float endSpeed;
    public TextAsset map;
    public Sprite[] spritesheet;
    public AudioClip bgm;
}