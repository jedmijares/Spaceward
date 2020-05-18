using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpawnableObject
{
    [Header("Stats")]
    public int curHp;
    public int maxHp = 50;
    public int scoreToGive = 50;
    public int minShotVolley = 3;
    public int maxShotVolley = 5;

    [Header("Movement")]
    public float moveSpeed = 15;
    public int screenEdgeOffset = 75;
    public int distanceFromCamera = 40;

    private Weapon weapon;
    private Vector3 destination; // next position to move towards
    private Camera cam;
    private bool shooting;
    private int shotVolley;


    void Start ()
    {
        // get the components
        weapon = GetComponent<Weapon>();
        cam = Camera.main;
        destination = cam.ScreenToWorldPoint(new Vector3(Random.Range(screenEdgeOffset, Screen.width - screenEdgeOffset), Random.Range(screenEdgeOffset, Screen.height - screenEdgeOffset), distanceFromCamera));
    }

    private void OnEnable()
    {
        curHp = maxHp;
        shooting = false;
        shotVolley = Random.Range(minShotVolley, maxShotVolley + 1);
    }

    void Update ()
    {
        if (shooting)
        {
            if (weapon.CanShoot())
            {
                weapon.Shoot();
                shotVolley--;
            }
            if (shotVolley == 0)
            {
                shooting = false;
                shotVolley = Random.Range(minShotVolley, maxShotVolley);
            }
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if (transform.position == destination)
        {
            destination = cam.ScreenToWorldPoint(new Vector3(Random.Range(screenEdgeOffset, Screen.width - screenEdgeOffset), Random.Range(screenEdgeOffset, Screen.height - screenEdgeOffset), distanceFromCamera));
            shooting = true;
        }
    }

    public void TakeDamage (int damage)
    {
        curHp -= damage;

        if(curHp <= 0)
            Die();
    }

    void Die ()
    {
        GameManager.instance.AddScore(scoreToGive);
        creator.Reclaim(this);
    }
}