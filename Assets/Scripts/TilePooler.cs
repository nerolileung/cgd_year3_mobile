using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePooler : MonoBehaviour
{
    private GameObject[] pool;

    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = Resources.Load<GameObject>("Tile");
        pool = new GameObject[12*30];
        for (int i = 0; i < pool.Length; i++){
            GameObject clone = Instantiate(prefab,transform);
            clone.SetActive(false);
            pool[i] = clone;
        }
    }
    public GameObject GetObject(){
        for (int i = 0; i < pool.Length; i++){
            if (!pool[i].activeInHierarchy) {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        Debug.Log("No free objects in pool!");
        return null;
    }
}