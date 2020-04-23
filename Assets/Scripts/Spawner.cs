// https://answers.unity.com/questions/553385/enemy-spawning-system-in-c.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objects;
    //public ObjectPool objects;
    public Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void spawn()
    {
        GameObject spawned = Instantiate(objects);
        spawned.transform.position = spawnPosition;
    }
}
