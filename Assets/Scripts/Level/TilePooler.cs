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
    public GameObject FindObject(int x, int y){
        for (int i = 0; i < pool.Length; i++){
            if (pool[i].activeInHierarchy && pool[i].transform.localPosition.x == x && pool[i].transform.localPosition.y == y) {
                return pool[i];
            }
        }
        //Debug.Log(string.Format("No active objects with coordinates {0} {1}!",x,y));
        return null;
    }
}