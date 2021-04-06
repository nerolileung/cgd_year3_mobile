using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLooping : MonoBehaviour
{
	AudioSource m_Source;
	public int m_Loops { set; private get; } = 1;

	private void Awake()
	{
		m_Source = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (!m_Source.isPlaying)
		{
			if (m_Loops > 1 || m_Loops == 0)
			{
				m_Source.Play();
				if (m_Loops > 1)
				{
					m_Loops--;
				}
			}
			else
			{
				AudioManager.Instance.onStopAllLoopingAudio -= this.DestroyEarly;
				AudioManager.Instance.onAudioCleanup -= this.DestroyEarly;
				Destroy(this.gameObject);
			}
		}
	}

	public void DestroyEarly(AudioManager manager)
	{
		manager.onStopAllLoopingAudio -= this.DestroyEarly;
		manager.onAudioCleanup -= this.DestroyEarly;
		Destroy(this.gameObject);
	}
}
