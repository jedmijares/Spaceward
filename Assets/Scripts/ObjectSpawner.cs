// https://catlikecoding.com/unity/tutorials/object-management/

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectSpawner : ScriptableObject
{ 
	[SerializeField]
	SpawnableObject[] prefabs;

	[SerializeField]
	bool recycle = true;

	public int maxObjects = -1;
	private int currentCount = 0;

	List<SpawnableObject>[] pools;

	public SpawnableObject Get (int objectID = 0)
	{
		if ((maxObjects <= 0) | (currentCount < maxObjects))
		{
			currentCount++;
			SpawnableObject instance;
			if (recycle)
			{
				if (pools == null)
				{
					CreatePools();
				}
				List<SpawnableObject> pool = pools[objectID];
				int lastIndex = pool.Count - 1;
				if (lastIndex >= 0)
				{
					instance = pool[lastIndex];
					instance.creator = this;
					instance.gameObject.SetActive(true);
					pool.RemoveAt(lastIndex);
				}
				else
				{
					instance = Instantiate(prefabs[objectID]);
					instance.creator = this;
					instance.ObjectID = objectID;
				}
			}
			else
			{
				instance = Instantiate(prefabs[objectID]);
				instance.creator = this;
				instance.ObjectID = objectID;
			}

			return instance;
		}
		else return null;
	}

	public SpawnableObject GetRandom () {
		return Get(Random.Range(0, prefabs.Length));
	}

	public void Reclaim (SpawnableObject objectToRecycle) 
	{
		currentCount--;
		if (recycle) {
			if (pools == null) {
				CreatePools();
			}
			pools[objectToRecycle.ObjectID].Add(objectToRecycle);
			objectToRecycle.gameObject.SetActive(false);
		}
		else {
			Destroy(objectToRecycle.gameObject);
		}
	}

	void CreatePools () {
		pools = new List<SpawnableObject>[prefabs.Length];
		for (int i = 0; i < pools.Length; i++) {
			pools[i] = new List<SpawnableObject>();
		}
	}
}