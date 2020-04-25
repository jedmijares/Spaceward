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
    public float OffscreenOffset = 10;
    public float ZPosition = 40;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", InitialTime, SpawnTime);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void Spawn()
    {
        GameObject spawned = Instantiate(objects);
        spawned.transform.position = getPosOffscreen(OffscreenOffset,ZPosition);
    }

    // at the given z value, generate a Vector3 representing a random position a distance offset offscreen
    Vector3 getPosOffscreen(float offset, float z)
    {
        Vector3 position = Vector3.zero;
        position.z = z;
        // Vector3 destination = cam.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), z));
        if((Random.value > 0.5f))
        {
            position.y = Random.Range(0, cam.pixelHeight);
            if ((Random.value > 0.5f))
            {
                position.x = cam.pixelWidth + offset;
            }
            else
            {
                position.x = 0 - offset;
            }
        }
        else
        {
            position.x = Random.Range(0, cam.pixelWidth);
            if ((Random.value > 0.5f))
            {
                position.y = cam.pixelHeight + offset;
            }
            else
            {
                position.y = 0 - offset;
            }
        }
        return cam.ScreenToWorldPoint(position);
    }
}
