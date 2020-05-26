using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ObjectSpawner bulletSpawner;
    public Transform muzzle;            // spawn pos for the bullet
    public int bulletIndex = 0;

    public float bulletSpeed;           // initial velocity of the bullet

    public float shootRate;             // min time between shots
    private float lastShootTime;        // last time we shot a bullet
    private bool isPlayer = false;              // are we the player's weapon?
    private GameObject target;

    private Camera cam;

    //public AudioClip shootSfx;
    //private AudioSource audioSource;

    void Awake ()
    {
        // are we attached to the player?
        if (GetComponent<Player>())
        {
            cam = Camera.main;
            isPlayer = true;
        }
        else
        {
            target = FindObjectOfType<Player>().gameObject;
            isPlayer = false;
        }

        //audioSource = GetComponent<AudioSource>();
    }

    // can we shoot a bullet?
    public bool CanShoot ()
    {
        if(Time.time - lastShootTime >= shootRate)
        {
                return true;
        }
        return false;
    }

    // called when we want to shoot a bullet
    public void Shoot ()
    {
        lastShootTime = Time.time;

        //audioSource.PlayOneShot(shootSfx);

        if(isPlayer)
        {
            // https://docs.unity3d.com/ScriptReference/RaycastHit-point.html
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if(hitInfo.point.z < muzzle.position.z) // if the hit was closer to the camera than the muzzle
                {
                    return; // do not fire
                }
                SpawnableObject bullet = bulletSpawner.Get(bulletIndex);
                bullet.transform.position = muzzle.position;
                Vector3 dir = (hitInfo.point - muzzle.position).normalized;
                bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
            }
            else // if the raycast misses any object
            {
                Vector3 aimed = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 40));
                SpawnableObject bullet = bulletSpawner.Get(bulletIndex);
                bullet.transform.position = muzzle.position;
                Vector3 dir = (aimed - muzzle.position).normalized;
                bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
            }
        }
        else // if an enemy
        {
            SpawnableObject bullet = bulletSpawner.Get(bulletIndex);
            bullet.transform.position = muzzle.position;
            Vector3 dir = (target.transform.position - muzzle.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
        }
    }
}