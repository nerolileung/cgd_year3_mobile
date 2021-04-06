using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
	[HideInInspector]
	public int m_PoolIndex { get;  private set; }
	public bool m_Active;
	ObjectPooler m_PoolRef;

	public void Init(int shelfNum, ObjectPooler poolRef)
	{
		m_PoolRef = poolRef;
		m_PoolIndex = shelfNum;
		m_Active = false;
	}

	public void RecycleSelf()
	{
		m_PoolRef.RecycleObject(this);
	}

	private void OnDestroy()
	{
		m_PoolRef.onPoolerCleanup -= this.RecycleSelf;
	}
}
