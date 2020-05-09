// https://catlikecoding.com/unity/tutorials/object-management/

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySpawner : ScriptableObject {

	[SerializeField]
	Enemy[] prefabs;

	[SerializeField]
	Material[] materials;

	[SerializeField]
	bool recycle;

	List<Enemy>[] pools;

	public Enemy Get (int enemyID = 0, int materialId = 0) {
		Enemy instance;
		if (recycle) {
			if (pools == null) {
				CreatePools();
			}
			List<Enemy> pool = pools[enemyID];
			int lastIndex = pool.Count - 1;
			if (lastIndex >= 0) {
				instance = pool[lastIndex];
				instance.gameObject.SetActive(true);
				pool.RemoveAt(lastIndex);
			}
			else {
				instance = Instantiate(prefabs[enemyID]);
				instance.EnemyID = enemyID;
			}
		}
		else {
			instance = Instantiate(prefabs[enemyID]);
			instance.EnemyID = enemyID;
		}

		// instance.SetMaterial(materials[materialId], materialId);
		return instance;
	}

	public Enemy GetRandom () {
		return Get(
			Random.Range(0, prefabs.Length),
			Random.Range(0, materials.Length)
		);
	}

	public void Reclaim (Enemy shapeToRecycle) {
		if (recycle) {
			if (pools == null) {
				CreatePools();
			}
			pools[shapeToRecycle.EnemyID].Add(shapeToRecycle);
			shapeToRecycle.gameObject.SetActive(false);
		}
		else {
			Destroy(shapeToRecycle.gameObject);
		}
	}

	void CreatePools () {
		pools = new List<Enemy>[prefabs.Length];
		for (int i = 0; i < pools.Length; i++) {
			pools[i] = new List<Enemy>();
		}
	}
}