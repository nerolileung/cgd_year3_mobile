using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOneShot : MonoBehaviour
{
	AudioSource m_Source;
	private void Awake()
	{
		m_Source = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (!m_Source.isPlaying)
		{
			AudioManager.Instance.onAudioCleanup -= this.DestroyEarly;
			Destroy(this.gameObject);
		}
	}

	public void DestroyEarly(AudioManager manager)
	{
		manager.onAudioCleanup -= this.DestroyEarly;
		Destroy(this.gameObject);
	}
}
