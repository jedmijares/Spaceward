using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : SpawnableObject
{
    public float lifetime = 3;      // how long until the Projectile despawns?
    private float shootTime;    // time the projectile was shot

    void OnEnable()
    {
        shootTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // disable the bullet after 'lifetime' seconds
        if (Time.time - shootTime >= lifetime)
            creator.Reclaim(this);
    }

    void OnTriggerEnter(Collider other)
    {
        creator.Reclaim(this);
    }
}
