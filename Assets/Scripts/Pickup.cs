using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : SpawnableObject
{
    public GameObject hitParticle;
    Vector3 destination;
    public float moveSpeed = 10;

    private void OnEnable()
    {
        destination = new Vector3(0, 0, 0); // Camera.main.WorldToScreenPoint(transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.instance.TakeDamage(-10);
            GameUI.instance.UpdateHealthBar(Player.instance.curHp, Player.instance.maxHp);
            Destroy(gameObject);
        }
            

        //// create the hit particle
        //GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);
        //Destroy(obj, 0.5f);
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if (transform.position == destination)
        {
            Destroy(gameObject);
        }
    }
}