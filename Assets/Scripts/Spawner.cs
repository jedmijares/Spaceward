// https://answers.unity.com/questions/553385/enemy-spawning-system-in-c.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objects;
    //public ObjectPool objects;
    public Vector3 spawnPosition;
    public float SpawnTime = 5.0f;
    public float InitialTime;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", InitialTime, SpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void Spawn()
    {
        GameObject spawned = Instantiate(objects);
        spawned.transform.position = spawnPosition;
    }
}
