using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdButton : MonoBehaviour
{
    [SerializeField]
    private LevelManager _levelManager;
    [SerializeField]
    private GameObject adWindow;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => adWindow.SetActive(true));
        button.onClick.AddListener(() => timer = 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (adWindow.activeInHierarchy){
            timer -= Time.deltaTime;
            if (timer < 0){
                // no more ad
                adWindow.SetActive(false);
                GetComponent<Button>().interactable = false;

                int basePoints = _levelManager.GetPoints();
                float bonusPoints = basePoints*0.05f;
                // round to 10
                bonusPoints -= bonusPoints % 10;
                // minimum
                bonusPoints += 10;

                int finalBonus = Mathf.FloorToInt(bonusPoints);
                GameManager.AddPoints(finalBonus);

                GetComponentInChildren<Text>().text = string.Format("+{0} points!",finalBonus);
            }
        }
    }
}
