using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    void OnBecameInvisible(){
        gameObject.SetActive(false);
    }
}