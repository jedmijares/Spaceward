using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [Header("Stats")]
    public int minShotVolley = 3;
    public int maxShotVolley = 5;

    private Weapon weapon;
    //private Vector3 destination; // next position to move towards
    private bool shooting;
    private int shotVolley;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        weapon = GetComponent<Weapon>();
        destination = cam.ScreenToWorldPoint(new Vector3(Random.Range(screenEdgeOffset, Screen.width - screenEdgeOffset), Random.Range(screenEdgeOffset, Screen.height - screenEdgeOffset), distanceFromCamera));
    }

    private void OnEnable()
    {
        //curHp = maxHp;
        curHp = maxHp;
        shooting = false;
        shotVolley = Random.Range(minShotVolley, maxShotVolley + 1);
    }

    // Update is called once per frame
    void Update()
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
}
