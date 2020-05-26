using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammingEnemy : Enemy
{
    enum State
    {
        INITIALIZING,
        ATTACKING
    }
    State state;
    public int attackingSpeed = 15;
    public int damage = 10;

    private void OnEnable()
    {
        this.transform.position = GameManager.instance.getPosOffscreen(GameManager.instance.OffscreenOffset, GameManager.instance.ZPosition);
        destination = new Vector3(0, 0, GameManager.instance.ZPosition);
        state = State.INITIALIZING;
        curHp = maxHp;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            creator.Reclaim(this);
        }
            

        //// create the hit particle
        //GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);
        //Destroy(obj, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.INITIALIZING:
                Move(moveSpeed);
                if(transform.position == destination)
                {
                    state = State.ATTACKING;
                }
                break;
            case State.ATTACKING:
                Move(attackingSpeed);
                destination = Player.instance.transform.position;
                if (transform.position == destination)
                {
                    creator.Reclaim(this);
                }
                break;
        }
    }

    void Move(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
}
