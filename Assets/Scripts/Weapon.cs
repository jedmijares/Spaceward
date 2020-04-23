using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ObjectPool bulletPool;
    //public GameObject bulletPrefab;
    public Transform muzzle;            // spawn pos for the bullet

    //public int curAmmo;                 // current amount of ammo
    //public int maxAmmo;                 // maximum amount of ammo we can get
    //public bool infiniteAmmo;           // do we have infinite ammo?

    public float bulletSpeed;           // initial velocity of the bullet

    public float shootRate;             // min time between shots
    private float lastShootTime;        // last time we shot a bullet
    //private bool isPlayer;              // are we the player's weapon?

    private Camera cam;

    //public AudioClip shootSfx;
    //private AudioSource audioSource;

    void Awake ()
    {
        cam = Camera.main;

        // are we attached to the player?
        //if (GetComponent<Player>())
        //    isPlayer = true;

        //audioSource = GetComponent<AudioSource>();
    }

    // can we shoot a bullet?
    public bool CanShoot ()
    {
        if(Time.time - lastShootTime >= shootRate)
        {
            //if(curAmmo > 0 || infiniteAmmo == true)
                return true;
        }

        return false;
    }

    // called when we want to shoot a bullet
    public void Shoot ()
    {
        lastShootTime = Time.time;
        //curAmmo--;

        //if (isPlayer)
        //    GameUI.instance.UpdateAmmoText(curAmmo, maxAmmo);

        //audioSource.PlayOneShot(shootSfx);

        GameObject bullet = bulletPool.GetObject();

        bullet.transform.position = muzzle.position;

        // https://docs.unity3d.com/ScriptReference/RaycastHit-point.html
        RaycastHit hitInfo;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 dir = (hitInfo.point - muzzle.position).normalized;

            //bullet.transform.rotation = Quaternion.Euler(dir.x, dir.y, dir.z);//dir;// Vector3.forward * angle;
            bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
        }
    }
}