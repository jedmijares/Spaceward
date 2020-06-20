using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : SpawnableObject
{
    public GameObject hitParticle;
    Vector3 destination;
    public float moveSpeed = 15;
    static Bounds targetBox = new Bounds(new Vector3(0, 0, 0), new Vector3(1, 1, 0));
    public const float homingDistance = 2.5F;

    private void OnEnable()
    {
        destination = targetBox.ClosestPoint(transform.position); // new Vector3(0, 0, 0); // Camera.main.WorldToScreenPoint(transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.instance.TakeDamage(-4);
            //GameUI.instance.UpdateHealthBar(Player.instance.curHp, Player.instance.maxHp);
            //Destroy(gameObject);
            creator.Reclaim(this);
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
        if(Vector3.Distance(transform.position, Player.instance.transform.position) < homingDistance)
        {
            destination = Player.instance.transform.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if (transform.position == destination)
        {
            creator.Reclaim(this);
        }
    }
}