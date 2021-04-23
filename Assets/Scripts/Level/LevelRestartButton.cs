using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelRestartButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(()=>SceneManager.LoadScene("Level"));
    }
}
