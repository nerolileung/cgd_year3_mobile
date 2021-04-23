using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsButton : MonoBehaviour
{
    public void Click(int controls){
        GameManager.SetControls((GameManager.CONTROL_SCHEME)controls);
    }
}