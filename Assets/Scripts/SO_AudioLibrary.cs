using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "AudioLibrary", menuName = "ScriptableAssets/AudioLibrary", order = 1)]
public class SO_AudioLibrary : ScriptableObject, ISerializationCallbackReceiver
{
    [Serializable]
    struct AudioEntry
    {
        public AudioType m_Type;
        public AudioClip[] m_Clips;
    }

    [SerializeField]
    AudioEntry[] m_DictionaryEntry;

	public Dictionary<AudioType, AudioClip[]> m_AudioLibrary;

    public void OnAfterDeserialize()
    {
        m_AudioLibrary = new Dictionary<AudioType, AudioClip[]>();
        foreach(AudioEntry entry in m_DictionaryEntry)
        {
            m_AudioLibrary.Add(entry.m_Type, entry.m_Clips);
        }
    }

    public void OnBeforeSerialize()
    {
    }
}
