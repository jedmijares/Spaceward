using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int curHp = 50;
    public int maxHp = 50;
    public int scoreToGive = 50;
    public int minShotVolley = 3;
    public int maxShotVolley = 5;

    [Header("Movement")]
    public float moveSpeed = 15;

    private Weapon weapon;
    private Vector3 destination; // next position to move towards
    private Camera cam;
    private bool shooting;
    private int shotVolley;
    private int shotsFired = 0;

    void Start ()
    {
        // get the components
        weapon = GetComponent<Weapon>();
        destination = transform.position;
        cam = Camera.main;
        shooting = false;
        shotVolley = Random.Range(minShotVolley, maxShotVolley);
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

        //// look at the target
        //Vector3 dir = (target.transform.position - transform.position).normalized;
        //float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        //transform.eulerAngles = Vector3.up * angle;
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if (transform.position == destination)
        {
            destination = cam.ScreenToWorldPoint(new Vector3(Random.Range(75, Screen.width - 75), Random.Range(75, Screen.height - 75), 40));
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

        Destroy(gameObject);
    }
}