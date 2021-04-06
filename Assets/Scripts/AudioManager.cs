using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType
{
	GUNSHOT,
	FOOTSTEP
}

public class AudioManager : MonoBehaviour
{
	#region singleton
	public static AudioManager Instance { get; private set; }
	public static IEnumerator WaitOnInstance()
	{
		int loops = 0;
		while(Instance == null)
		{
			yield return new WaitForEndOfFrame();
			loops++;
			if(loops >= 30)
			{
				GameObject tempGO = new GameObject("AudioManager");
				AudioManager manager = tempGO.AddComponent<AudioManager>();
				Instance = manager;
				break;
			}
		}
	}
	#endregion

	[SerializeField]
	private SO_AudioLibrary m_AudioLibrary;

	[SerializeField]
	private AudioMixer m_Mixer;

	#region CleanupEvent
	public event Action<AudioManager> onAudioCleanup;
	public void Event_AudioCleanup()
	{
		onAudioCleanup?.Invoke(this);
	}
	#endregion
	#region StopLoopingEvent
	public event Action<AudioManager> onStopAllLoopingAudio;
	public void Event_StopAllLoopingAudio()
	{
		onStopAllLoopingAudio?.Invoke(this);
	}
	#endregion

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			DestroyImmediate(this.gameObject);
		}

		if (m_AudioLibrary == null)
		{
			m_AudioLibrary = Resources.Load<SO_AudioLibrary>("AudioLibrary");
		}
	}

	#region OneShot
	/// <summary>
	/// Spawns an object to play a sound with various properties
	/// </summary>
	/// <param name="type">clip type to be selected</param>
	/// <param name="group">audio group to play through</param>
	public GameObject PlayAudio(AudioType type, string group)
	{
		GameObject tempGO = new GameObject("AudioOneShot");
		AudioSource source = tempGO.AddComponent<AudioSource>();
		source.outputAudioMixerGroup = m_Mixer.FindMatchingGroups(group)[0];
		source.clip = GetClipByType(type);
		AudioOneShot scriptRef = tempGO.AddComponent<AudioOneShot>();
		onAudioCleanup += scriptRef.DestroyEarly;
		source.Play();
		return tempGO;
	}/// <summary>
	 /// Spawns an object to play a sound with various properties
	 /// </summary>
	 /// <param name="type">clip type to be selected</param>
	 /// <param name="group">audio group to play through</param>
	 /// <param name="position">the world position of the sound</param>
	public GameObject PlayAudio(AudioType type, string group, Vector3 position)
	{
		GameObject tempGO = PlayAudio(type, group);
		tempGO.transform.position = position;
		return tempGO;
	}
	#endregion

	#region Looping
	/// <summary>
	/// Spawns an object to play a sound with various properties
	/// </summary>
	/// <param name="type">clip type to be selected</param>
	/// <param name="group">audio group to play through</param>
	/// <param name="loops">0 means infinite loop</param>
	public GameObject PlayAudio(AudioType type, string group, int loops = 1)
	{
		GameObject tempGO = new GameObject("AudioLooping");
		AudioSource source = tempGO.AddComponent<AudioSource>();
		source.outputAudioMixerGroup = m_Mixer.FindMatchingGroups(group)[0];
		source.clip = GetClipByType(type);
		AudioLooping scriptRef = tempGO.AddComponent<AudioLooping>();
		scriptRef.m_Loops = Mathf.Max(loops, 0);
		onStopAllLoopingAudio += scriptRef.DestroyEarly;
		onAudioCleanup += scriptRef.DestroyEarly;
		source.Play();
		return tempGO;
	}
	/// <summary>
	/// Spawns an object to play a sound with various properties
	/// </summary>
	/// <param name="type">clip type to be selected</param>
	/// <param name="group">audio group to play through</param>
	/// <param name="position">the world position of the sound</param>
	/// <param name="loops">0 means infinite loop</param>
	public GameObject PlayAudio(AudioType type, string group, Vector3 position, int loops = 1)
	{
		GameObject tempGO = PlayAudio(type, group, loops);
		tempGO.transform.position = position;
		return tempGO;
	}
	#endregion

	private AudioClip GetClipByType(AudioType type)
	{
		float rand = UnityEngine.Random.value * 0.99f;
		int index = 0;
		int clipCount = m_AudioLibrary.m_AudioLibrary[type].Length;

		switch (type)
		{
			case AudioType.GUNSHOT:
				if (clipCount > 1)
				{
					index = Mathf.FloorToInt(rand * (clipCount + 1));
				}
				break;
			case AudioType.FOOTSTEP:
				//logic in here for selecting the correct index
				break;
			default:
				break;
		}

		return m_AudioLibrary.m_AudioLibrary[type][index];
	}
}

public static class AudioGroups
{
	public const string Music = "Music";
	public const string SFX = "SFX";
}