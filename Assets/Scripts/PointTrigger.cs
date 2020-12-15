using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    public int points;
    private LevelManager _levelManager;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Player"){
            _levelManager.AddScore(points);
            GameObject.Destroy(this.gameObject);
        }
    }
}
