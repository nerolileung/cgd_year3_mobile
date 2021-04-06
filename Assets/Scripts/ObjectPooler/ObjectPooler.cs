using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PooledObjectInfo
{
	public GameObject PrefabRef;
	public int PoolSize;
	public bool CanGrow;
	public string ObjectName;
}

public class ObjectPooler : MonoBehaviour
{
	#region Singleton

	public static ObjectPooler Instance { get; private set; }

	public static IEnumerator WaitOnInstance()
	{
		int loops = 0;
		while (Instance == null)
		{
			yield return new WaitForEndOfFrame();
			loops++;
			if (loops >= 30)
			{
				GameObject tempGO = new GameObject("ObjectPooler");
				ObjectPooler pooler = tempGO.AddComponent<ObjectPooler>();
				Instance = pooler;
				break;
			}
		}
	}

	#endregion

	[SerializeField]
	private PooledObjectInfo[]		m_ObjectData;

	private GameObject[]			m_Shelves;
	private List<PooledObject>[]	m_PooledObjects;

	public event Action onPoolerCleanup;

	public void Event_PoolerCleanup()
	{
		onPoolerCleanup?.Invoke();
	}

	private void Awake()
	{
		#region Singleton
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			DestroyImmediate(this.gameObject);
		}
		#endregion

		//create the arrays at the correct length
		int shelfAmount = m_ObjectData.Length;
		m_Shelves = new GameObject[shelfAmount];
		m_PooledObjects = new List<PooledObject>[shelfAmount];

		for(int i = 0; i < shelfAmount; i++)
		{
			//create the GameObject Shelf parent for this type of pooled object;
			GameObject shelf = new GameObject("Shelf: " + m_ObjectData[i].ObjectName);
			shelf.transform.parent = this.transform;
			m_Shelves[i] = shelf;
			//create the list of objects and instantiate up tot he desired amount
			m_PooledObjects[i] = new List<PooledObject>();
			for(int j = 0; j < m_ObjectData[i].PoolSize; j++)
			{
				//Create an instance of the prefab, add a pooledObjectScript
				GameObject tempGO = Instantiate<GameObject>(m_ObjectData[i].PrefabRef, shelf.transform);
				PooledObject pooledRef = tempGO.AddComponent<PooledObject>();
				tempGO.name = m_ObjectData[i].ObjectName;
				tempGO.SetActive(false);
				m_PooledObjects[i].Add(pooledRef);
				pooledRef.Init(i, this);
			}
		}
	}

	public GameObject GetPooledObject(string name)
	{
		int shelfCount = m_Shelves.Length;
		int currentShelf = -1;
		for(int i = 0; i < shelfCount; i++)
		{
			if(m_Shelves[i].name == "Shelf: " + name)
			{
				currentShelf = i;
				break;
			}
		}

		if (currentShelf >= 0)
		{
			int currentPoolCount = m_PooledObjects[currentShelf].Count;
			int firstAvailable = -1;
			for(int i = 0; i < currentPoolCount; i++)
			{
				if (m_PooledObjects[currentShelf][i] != null)
				{
					if(!m_PooledObjects[currentShelf][i].m_Active)
					{
						firstAvailable = i;
						break;
					}
				}
				else
				{
					RegenItem(currentShelf, i);
					firstAvailable = i;
					break;
				}
			}

			if(firstAvailable >= 0)
			{
				PooledObject toReturn = m_PooledObjects[currentShelf][firstAvailable];
				toReturn.m_Active = true;
				onPoolerCleanup += toReturn.RecycleSelf;
				return toReturn.gameObject;
			}

			if(m_ObjectData[currentShelf].CanGrow)
			{
				GameObject tempGO = Instantiate<GameObject>(m_ObjectData[currentShelf].PrefabRef, m_Shelves[currentShelf].transform);
				PooledObject pooledRef = tempGO.AddComponent<PooledObject>();
				tempGO.name = m_ObjectData[currentShelf].ObjectName;
				tempGO.SetActive(false);
				m_PooledObjects[currentShelf].Add(pooledRef);
				pooledRef.Init(currentShelf, this);
				pooledRef.m_Active = true;
				onPoolerCleanup += pooledRef.RecycleSelf;
				return tempGO;
			}
			else
			{
				Debug.LogWarning("No Objects left in the pool by the name of " + name + ". Returning NULL");
			}
		}
		else
		{
			Debug.LogError("No Shelf for objects by the name of " + name);
		}

		return null;
	}

	public void RecycleObject(GameObject obj)
	{
		PooledObject poolRef = obj.GetComponent<PooledObject>();
		if (poolRef != null)
		{
			RecycleObject(poolRef);
		}
		else
		{
			Debug.LogError("Trying to recycle an object called " + obj.name + " that didnt come from the Object Pooler");
		}
	}

	public void RecycleObject(PooledObject poolRef)
	{
		poolRef.transform.parent = m_Shelves[poolRef.m_PoolIndex].transform;
		poolRef.gameObject.SetActive(false);
		poolRef.m_Active = false;
		onPoolerCleanup -= poolRef.RecycleSelf;
	}

	private void RegenItem(int shelfIndex, int slotIndex)
	{
		Debug.Log("Regenning item on shelf " + shelfIndex + " in slot " + slotIndex);
		GameObject tempGO = Instantiate<GameObject>(m_ObjectData[shelfIndex].PrefabRef, m_Shelves[shelfIndex].transform);
		PooledObject pooledRef = tempGO.AddComponent<PooledObject>();
		tempGO.name = m_ObjectData[shelfIndex].ObjectName;
		tempGO.SetActive(false);
		m_PooledObjects[shelfIndex][slotIndex] = pooledRef;
		pooledRef.Init(shelfIndex, this);
	}
}
