using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;          // damage dealt to the target

    public GameObject hitParticle;

    void OnTriggerEnter(Collider other)
    {
        //did we hit the player ?
        if (other.CompareTag("Player"))
            other.GetComponent<Player>().TakeDamage(damage);
        else if (other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().TakeDamage(damage);

        //// create the hit particle
        //GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);
        //Destroy(obj, 0.5f);
    }
}