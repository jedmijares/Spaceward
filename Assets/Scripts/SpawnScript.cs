// referred to https://catlikecoding.com/unity/tutorials/object-management/

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShapeFactory : ScriptableObject {

	[SerializeField]
	GameObject[] prefabs;

	[SerializeField]
	Material[] materials;

	[SerializeField]
	bool recycle;

	List<GameObject>[] pools;

	public GameObject Get (int shapeId = 0, int materialId = 0) {
		GameObject instance;
		if (recycle) {
			if (pools == null) {
				CreatePools();
			}
			List<GameObject> pool = pools[shapeId];
			int lastIndex = pool.Count - 1;
			if (lastIndex >= 0) {
				instance = pool[lastIndex];
				instance.gameObject.SetActive(true);
				pool.RemoveAt(lastIndex);
			}
			else {
				instance = Instantiate(prefabs[shapeId]);
				//instance.GameObject = shapeId;
			}
		}
		else {
			instance = Instantiate(prefabs[shapeId]);
			//instance.GameObject = shapeId;
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
			// pools[shapeToRecycle.ShapeId].Add(shapeToRecycle);
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