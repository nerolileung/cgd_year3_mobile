using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ToyInfo", order = 1)]
public class ToyInfo : ScriptableObject
{
    public string displayName;
    [TextArea]
    public string description;
    public int price;
    public Sprite image;
}