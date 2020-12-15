using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Data", menuName = "LevelInfo", order = 1)]
public class LevelInfo : ScriptableObject
{
    public string title;
    //public Sprite image;
    public float startSpeed;
    public float endSpeed;
    // length in units/100px
    public int levelLength;
}