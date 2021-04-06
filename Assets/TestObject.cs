using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
	private void Start()
	{
		AudioManager.Instance.PlayAudio(AudioType.GUNSHOT, AudioGroups.SFX, new Vector3(1, 0, 1), 6);
	}
}