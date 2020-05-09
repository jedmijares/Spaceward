// https://catlikecoding.com/unity/tutorials/object-management/

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectSpawner : ScriptableObject {

	[SerializeField]
	GameObject[] prefabs;

	[SerializeField]
	Material[] materials;

	[SerializeField]
	bool recycle = true;

	List<GameObject>[] pools;

	public GameObject Get (int objectID = 0, int materialId = 0) {
		GameObject instance;
		if (recycle) {
			if (pools == null) {
				CreatePools();
			}
			List<GameObject> pool = pools[objectID];
			int lastIndex = pool.Count - 1;
			if (lastIndex >= 0) {
				instance = pool[lastIndex];
				instance.gameObject.SetActive(true);
				pool.RemoveAt(lastIndex);
			}
			else {
				instance = Instantiate(prefabs[objectID]);
				//instance.ObjectID = objectID;
			}
		}
		else {
			instance = Instantiate(prefabs[objectID]);
			//instance.ObjectID = objectID;
		}

		// instance.SetMaterial(materials[materialId], materialId);
		return instance;
	}

	public GameObject GetRandom () {
		return Get(
			Random.Range(0, prefabs.Length),
			Random.Range(0, materials.Length)
		);
	}

	public void Reclaim (GameObject shapeToRecycle) {
		if (recycle) {
			if (pools == null) {
				CreatePools();
			}
			//pools[shapeToRecycle.ObjectID].Add(shapeToRecycle);
			shapeToRecycle.gameObject.SetActive(false);
		}
		else {
			Destroy(shapeToRecycle.gameObject);
		}
	}

	void CreatePools () {
		pools = new List<GameObject>[prefabs.Length];
		for (int i = 0; i < pools.Length; i++) {
			pools[i] = new List<GameObject>();
		}
	}
}